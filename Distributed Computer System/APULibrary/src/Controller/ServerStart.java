/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Controller;

import java.rmi.*;
import java.rmi.registry.*;


/**
 *
 * @author Darryl Yew Wei Kit
 */
public class ServerStart {
    public static void main(String[] args) throws RemoteException{
        System.out.println("Initializing Server..");
        try{
                Registry reg = LocateRegistry.createRegistry(1085);
                reg.rebind("test",new LibraryServer());
                System.out.println("Server is Up and Running...");
        }
        catch(Exception E){
            System.out.println("Error Running Server... Error Code: " + E);
        }
    }
}
