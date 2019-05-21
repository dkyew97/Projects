/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package View;


import Modal.ConnectionClass;
import Modal.LibraryBook;
import Controller.LibraryInterface;
import Modal.TransactionObject;
import java.net.MalformedURLException;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.util.List;
import java.util.Scanner;


/**
 *
 * @author Darryl Yew
 */
public class LibraryMenu {
    ConnectionClass cc = ConnectionClass.getInstance();
    LibraryInterface object;
    private static String HeadTail = "=============================================";
    
    public LibraryMenu() throws NotBoundException, MalformedURLException, RemoteException {
        this.object = cc.getServerCon();
    }
    public void LibraryFirst() throws NotBoundException, MalformedURLException, RemoteException{
        int sel;
        Scanner sc = new Scanner(System.in);
        System.out.println(HeadTail+HeadTail);
        System.out.println("WELCOME TO APU LIBRARY!!!");
        System.out.println("First Time User?? Choose 1 to Register");
        System.out.println("Registered?? Choose 2 to Login");
        System.out.print("SELECTION: ");
        sel = sc.nextInt();
        switch(sel){
            case 1: Registration("Student");break;
            case 2: Login();break;
        }
    }
    public void Registration(String lvlauth) throws NotBoundException, MalformedURLException, RemoteException{
        Scanner sc = new Scanner(System.in);
        
        String TP;
        String userName;
        String passCode;
        String email;
        System.out.println(HeadTail+HeadTail);
        System.out.println("REGISTRATION!!");
        System.out.print("TP Number: ");
        TP = sc.nextLine();
        System.out.print("Username: ");
        userName = sc.nextLine();
        System.out.print("Password: ");
        passCode = sc.nextLine();
        System.out.print("Email: ");
        email = sc.next();
        
        if(object.makeRegistration(TP ,userName, passCode.hashCode(), email,lvlauth)){
            System.out.println("Registration is Successful");
            Login();
        }
        else{
            System.out.println("Registration Failed!");
            Registration(lvlauth);
        }
    }
    
    public void MainMenu() throws NotBoundException, MalformedURLException, RemoteException{
        int sel;

        Scanner sc = new Scanner(System.in);
        System.out.println(HeadTail+HeadTail);
        System.out.println("Welcome " + object.getUserName());
        System.out.println("1. Borrow Book");
        System.out.println("2. Report");
        System.out.println("3. List All DCOMS Book");
        System.out.println("4. Log Out");
        System.out.print("SELECTION: ");
        sel = sc.nextInt();
        switch(sel){
            case 1: borrowBook();break;
            case 2: viewReport();break;
            case 3: BookSelect();break;
        }
    }
    public void Login() throws RemoteException, NotBoundException, MalformedURLException{
Scanner sc = new Scanner(System.in);
        String user;
        String pw;
        System.out.println(HeadTail+HeadTail);
        System.out.println("LOGIN!!");
        System.out.print("Username: ");
        user = sc.nextLine();
        System.out.print("Password: ");
        pw = sc.next();
        String auth = object.validateLogin(user, pw.hashCode());
        switch (auth){
            case "Student":object.setUserName(user);MainMenu();break;
            case "Librarian":object.setUserName(user);LibMenu();break;
            default:System.out.println("Invalid Credentials!! Try Again");
            Login();
            break;
        }
        
    }
    public void BookSelect() throws NotBoundException, MalformedURLException, RemoteException{
        System.out.println(HeadTail+HeadTail);
        List <LibraryBook> rs =object.retrieveBook();
        int a = 1;
        String format = "%-3s %-40s %-30s %-30s \n";
        System.out.printf(format,"", "TITLE","Author","Publisher");
        for(int i = 0; i < rs.size();i++){
            System.out.printf(format,a+".",rs.get(i).getA(),rs.get(i).getB(),rs.get(i).getC());
            a++;
        }
        System.out.println(HeadTail+HeadTail);
        MainMenu();
    }
    public void borrowBook()throws NotBoundException, MalformedURLException, RemoteException{
        System.out.println(HeadTail+HeadTail);
        Scanner sc = new Scanner(System.in);
        int sel;
        int days;
        List <LibraryBook> rs =object.retrieveBook();
        int a = 1;
        String format = "%-3s %-40s %-30s %-30s \n";
        System.out.printf(format,"", "TITLE","Author","Publisher");
        for(int i = 0; i < rs.size();i++){
            System.out.printf(format,a+".",rs.get(i).getA(),rs.get(i).getB(),rs.get(i).getC());
            a++;
        }
        System.out.print("Which Book would you like to borrow? Enter the number: ");
        sel = sc.nextInt();
        System.out.print("Duration of Lend: ");
        days = sc.nextInt();
        if(object.makeLendTransaction(object.getUserName(),rs.get(sel-1).getA(),days)){
            System.out.println("Borrowed Successfully");
        }
        else{
            System.out.println("Problem Occured. Try Again");
        }
        MainMenu();
    }
    public void viewReport() throws NotBoundException, MalformedURLException, RemoteException{
        System.out.println(HeadTail+HeadTail+HeadTail);
        List <TransactionObject> rs =object.retrieveReport(object.getUserName());
        int a = 1;
        String format = "%-3s %-20s %-40s %-10s %-30s\n";
        System.out.printf(format,"", "Username","Book Borrowed","Duration","Date Lend");
        for(int i = 0; i < rs.size();i++){
            System.out.printf(format,a+".",rs.get(i).getUsername(),rs.get(i).getBooktitle(),rs.get(i).getDuration()+ " Days",rs.get(i).getD());
            a++;
        }
        System.out.println(HeadTail+HeadTail+HeadTail);
        MainMenu();
    }
    public void LibMenu() throws RemoteException, NotBoundException, MalformedURLException{
        System.out.println(HeadTail+HeadTail);
        Scanner sc = new Scanner(System.in);
        int sel;
        System.out.println("Welcome "+ object.getUserName());
        System.out.println("1. Add Book");
        System.out.println("2. View All Report");
        System.out.println("3. Add new Librarian");
        System.out.println("4. Log Out");
        System.out.print("SELECTION: ");
        sel = sc.nextInt();
        switch(sel){
            case 1:addBook();break;
            case 2: viewAllReport();break;
            case 3: Registration("Librarian");break;
            case 4: System.exit(0);
        }
    }
    public void addBook() throws NotBoundException, MalformedURLException, RemoteException{
        System.out.println(HeadTail+HeadTail);
        Scanner sc = new Scanner(System.in);
        String book, author, pub;
        System.out.print("Book Title: ");
        book = sc.nextLine();
        System.out.print("Author: ");
        author = sc.nextLine();
        System.out.print("Publisher: ");
        pub = sc.nextLine();
        if(object.addBook(book, author, pub)){
            System.out.println("Book Added Successfully");
        }
        else{
            System.out.println("Problem Occured. Try Again!");
        }
        LibMenu();
    }
    public void viewAllReport() throws RemoteException, NotBoundException, MalformedURLException{
        System.out.println(HeadTail+HeadTail+HeadTail);
        List <TransactionObject> rs =object.retrieveReport();
        int a = 1;
        String format = "%-3s %-20s %-40s %-10s %-30s\n";
        System.out.printf(format,"", "Username","Book Borrowed","Duration","Date Lend");
        for(int i = 0; i < rs.size();i++){
            System.out.printf(format,a+".",rs.get(i).getUsername(),rs.get(i).getBooktitle(),rs.get(i).getDuration() + " Days",rs.get(i).getD());
            a++;
        }
        System.out.println(HeadTail+HeadTail+HeadTail);
        LibMenu();
    }
}
