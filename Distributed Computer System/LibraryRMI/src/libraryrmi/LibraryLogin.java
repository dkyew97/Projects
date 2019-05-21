/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package libraryrmi;

import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.util.Scanner;

/**
 *
 * @author Darryl Yew
 */
public class LibraryLogin {
    public void login()throws MalformedURLException,NotBoundException,RemoteException{
        LibraryInterface object = (LibraryInterface) Naming.lookup("rmi://localhost:1055/test");
        String userName,password;
        Scanner sc = new Scanner(System.in);
        

        System.out.println("WELCOME TO APU LIBRARY SYSTEM");
        System.out.print("Username: ");
        userName = sc.next();
        System.out.print("Password: ");
        password = sc.next();
            
            if(object.validateLogin(userName,password)){
                System.out.println("Success");
                LibraryMenu lm = new LibraryMenu();
                lm.menu();
            }
            else{
                System.out.println("Failed");
                login();
            }
            

    }
    
}
