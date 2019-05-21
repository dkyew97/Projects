/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Controller;

import java.rmi.*;
import java.util.List;

/**
 *
 * @author Darryl Yew Wei Kit
 */
public interface LibraryInterface extends Remote{
    public String getUserName() throws RemoteException;
    public void setUserName(String x) throws RemoteException;
    public Boolean makeRegistration(String TP, String username, int password, String email,String lvl) throws RemoteException;
    public String validateLogin(String username, int password) throws RemoteException;
    public List retrieveBook() throws RemoteException;
    public Boolean makeLendTransaction(String userName, String booktitle, int duration) throws RemoteException;
    public List retrieveReport(String x) throws RemoteException;
    public List retrieveReport() throws RemoteException;
    public Boolean addBook(String book, String auth, String pub) throws RemoteException;
}
