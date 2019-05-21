/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package libraryrmi;

import java.rmi.*;

/**
 *
 * @author Darryl Yew Wei Kit
 */
public interface LibraryInterface extends Remote{
    public Boolean validateLogin(String name, String pw)throws RemoteException;
    public String makeRegistration(String userName, String Password) throws RemoteException;
    public String getBook()throws RemoteException;
    public String returnBook()throws RemoteException;
}
