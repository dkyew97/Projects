/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package libraryrmi;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.sql.Connection;
import java.sql.DriverManager;
import java.util.Scanner;

/**
 *
 * @author Darryl Yew Wei Kit
 */
public class LibraryServer extends UnicastRemoteObject implements LibraryInterface{
    String userName;
    String password;
    static Connection con;
     public LibraryServer()throws RemoteException{
            super();
        }

    @Override
    public Boolean validateLogin(String name, String pw) throws RemoteException {
        return name.equals("Darryl") && pw.equals("8929");
    }

    @Override
    public String makeRegistration(String userName, String Password) throws RemoteException {
        return userName + " " + Password;
    }
    public static Connection connectDB(){
        try {
            
            Class.forName("org.apache.derby.jdbc.ClientDriver");
            con = DriverManager.getConnection("jdbc:derby://localhost:1527/Library", "", "");
            System.out.println("connection succeful");
            
        }
        catch (Exception e) {
            System.out.println("cannot connect");
        }
        
        return con;
    }
}

