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
public class LibraryRegistration {
    public void regis()throws MalformedURLException,NotBoundException,RemoteException{
        LibraryInterface object = (LibraryInterface) Naming.lookup("rmi://localhost:1055/test");
        String uname,pw,email;
        Scanner sc = new Scanner(System.in);
        System.out.println("REGISTRATION!");
        System.out.print("Username:");
        uname = sc.next();
        System.out.print("Password: ");
        pw = sc.next();
        System.out.print("Email");
        email = sc.next();
        String sample = object.makeRegistration(uname, pw);
        System.out.println(sample);
    }
}
