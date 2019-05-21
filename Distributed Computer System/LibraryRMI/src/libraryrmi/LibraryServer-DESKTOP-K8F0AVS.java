/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package libraryrmi;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.Scanner;

/**
 *
 * @author Darryl Yew Wei Kit
 */
public class LibraryServer extends UnicastRemoteObject implements LibraryInterface{
    String userName;
    String password;
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

    @Override
    public String getBook() throws RemoteException {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public String returnBook() throws RemoteException {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
}

