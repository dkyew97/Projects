/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Controller;

import Modal.ConnectionClass;
import Modal.TransactionObject;
import Modal.LibraryBook;
import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.List;
/**
 *
 * @author Darryl Yew Wei Kit
 */
public class LibraryServer extends UnicastRemoteObject implements LibraryInterface{
    String userName;
    String password;
    ConnectionClass cc = ConnectionClass.getInstance();
    Connection con = cc.Dbconnect();

    @Override
    public String getUserName() {
        return userName;
    }

    @Override
    public void setUserName(String userName) {
        this.userName = userName;
    }
  
     
   
     public LibraryServer()throws RemoteException{
            super();
        }

    @Override
    public Boolean makeRegistration(String tp, String u, int pw, String em, String lvlauth) throws RemoteException {

        String query = "INSERT INTO LIBRARYUSER (tpnumber, userID, userPassword, userEmail, lvlauth) values (?,?,?,?,?)";
        try{
            PreparedStatement st = con.prepareStatement(query);
            st.setString(1, tp);
            st.setString(2,u);
            st.setInt(3, pw);
            st.setString(4,em);
            st.setString(5,lvlauth);
            st.executeUpdate();
            st.close();
            return true;
        }
        catch(Exception e){
            return false ;
        }
    }

    @Override
    public String validateLogin(String username, int password) throws RemoteException {

        String query = "SELECT userID, userPassword, lvlauth from libraryuser where userID = ?";
        try{
            PreparedStatement st = con.prepareStatement(query);
            st.setString(1, username);
            ResultSet rs = st.executeQuery();
            while(rs.next()){
                if(rs.getInt("userPassword")==(password) && rs.getString("userID").equals(username)){
                    return rs.getString("lvlauth");
                }
            }
        }
        catch (Exception e){
            System.out.println(e);
        }
        return "";
    }

    @Override
    public List retrieveBook() throws RemoteException {

        List <LibraryBook> li = new ArrayList();
        String query = "SELECT * from librarybook";
        try{
            PreparedStatement st = con.prepareStatement(query);
            ResultSet rs = st.executeQuery();
            while(rs.next()){
                LibraryBook s = new LibraryBook();
                s.setA(rs.getString("title"));
                s.setB(rs.getString("bookauthor"));
                s.setC(rs.getString("bookpublisher"));
                li.add(s);
            }
            return li;
        }
        catch(Exception e){
            System.out.println(e + "abc123");
        }
        return null;
    }

    @Override
    public Boolean makeLendTransaction(String userName, String booktitle, int duration) throws RemoteException {

        String query = "INSERT INTO transactionlend (booktitle, userid, duration, datelend) values (?,?,?,?)";
        Timestamp date = new Timestamp(new java.util.Date().getTime());
        try{
            PreparedStatement st = con.prepareStatement(query);
            st.setString(1, booktitle);
            st.setString(2,userName);
            st.setInt(3, duration);
            st.setTimestamp(4, date);
            st.executeUpdate();
            st.close();
            return true;
        }
        catch(Exception e){
            return false;
        }
    }

    @Override
    public List retrieveReport(String x) throws RemoteException {
        List <TransactionObject> li = new ArrayList();
        
        String query = "SELECT * from Transactionlend where userID = ?";
        try{
            PreparedStatement st = con.prepareStatement(query);
            st.setString(1, x);
            ResultSet rs = st.executeQuery();
            while(rs.next()){
                TransactionObject s = new TransactionObject();
                s.setBooktitle(rs.getString("booktitle"));
                s.setUsername(rs.getString("userid"));
                s.setDuration(rs.getInt("duration"));
                s.setD(rs.getTimestamp("datelend"));
                li.add(s);
            }
            return li;
        }
        catch(Exception e){
            System.out.println(e + "abc123");
        }
        return null;
    }

    @Override
    public Boolean addBook(String book, String auth, String pub) throws RemoteException {
        String query = "INSERT INTO librarybook (title, bookauthor, bookpublisher) values (?,?,?)";
        try{
            PreparedStatement st = con.prepareStatement(query);
            st.setString(1, book);
            st.setString(2,auth);
            st.setString(3, pub);
            st.executeUpdate();
            st.close();
            return true;
        }
        catch(Exception e){
            return false ;
        }
        
    }

    @Override
    public List retrieveReport() throws RemoteException {
        List <TransactionObject> li = new ArrayList();
        
        String query = "SELECT * from Transactionlend";
        try{
            PreparedStatement st = con.prepareStatement(query);
            ResultSet rs = st.executeQuery();
            while(rs.next()){
                TransactionObject s = new TransactionObject();
                s.setBooktitle(rs.getString("booktitle"));
                s.setUsername(rs.getString("userid"));
                s.setDuration(rs.getInt("duration"));
                s.setD(rs.getTimestamp("datelend"));
                li.add(s);
            }
            return li;
        }
        catch(Exception e){
            System.out.println(e);
        }
        return null;
    }
    
}

