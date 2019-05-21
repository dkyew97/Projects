package Modal;

import Controller.LibraryInterface;
import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.sql.Connection;
import java.sql.DriverManager;

public class ConnectionClass {
    public static ConnectionClass instance = new ConnectionClass();
    private ConnectionClass(){}
    public static ConnectionClass getInstance(){
        return instance;
    }
    public LibraryInterface getServerCon() throws NotBoundException, MalformedURLException, RemoteException{
        LibraryInterface object = (LibraryInterface) Naming.lookup("rmi://localhost:1085/test");
        return object;
    }
    static Connection con;
    public Connection Dbconnect() {
        System.out.println("Connecting to Database...");
        try {
            con = DriverManager.getConnection("jdbc:sqlite:C:\\\\Users\\\\Darryl Yew\\\\OneDrive - Asia Pacific University\\\\Degree Assignment\\\\Distributed Computing\\\\APULibrary\\\\LibraryUser.db");
            System.out.println("Database Connected Successfully...");
        }
        catch (Exception e) {
            System.out.println("Failed to Connect. Error= " +e);
        }
        return con;
    }
}
