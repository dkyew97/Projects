/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package View;

import View.LibraryMenu;
import java.net.MalformedURLException;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;

/**
 *
 * @author Darryl Yew
 */
public class Main {
    public static void main(String[] args) throws NotBoundException, MalformedURLException, RemoteException {
       LibraryMenu lm = new LibraryMenu();
       lm.LibraryFirst();
    }
}
