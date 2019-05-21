/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package libraryrmi;

import java.rmi.*;
import java.rmi.registry.*;


/**
 *
 * @author Darryl Yew Wei Kit
 */
public class LibraryServerGeneration {
    public static void main(String[] args) throws RemoteException{
        System.out.println("Server Starting..");
        try{
                Registry reg = LocateRegistry.createRegistry(1055);
                reg.rebind("test",new LibraryServer());
                System.out.println("Server Running...");
        }
        catch(Exception E){
            System.out.println("Error Running Server... Error Code: " + E);
        }
    }
}
