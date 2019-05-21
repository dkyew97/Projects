/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package libraryrmi;

import java.net.MalformedURLException;
import java.rmi.*;
import java.util.Scanner;
/**
 *
 * @author Darryl Yew Wei Kit
 */
public class LibraryClient {
    public static void main(String[] args) throws MalformedURLException, NotBoundException, RemoteException {
        Scanner sc = new Scanner(System.in);
        int selection;
        LibraryMenu ll = new LibraryMenu();
        ll.firstMenu();
        selection = sc.nextInt();
        switch (selection){
            case 1:LibraryLogin login = new LibraryLogin();login.login();break;
            case 2:LibraryRegistration regis = new LibraryRegistration();regis.regis();break;
            default:System.out.println("Invalid Input");
        }
    }
}
