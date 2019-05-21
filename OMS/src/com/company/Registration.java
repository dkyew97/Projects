package com.company;

import java.io.*;
import java.util.Scanner;

public class Registration {
    Scanner SC = new Scanner(System.in);

    public void RGMenu() throws IOException {
        int A;
        System.out.println("1. Register");
        System.out.println("2. Edit User");
        System.out.println("3. View All User");
        System.out.println("4. Delete User");
        System.out.println("5. Quit");
        System.out.print("Selection:");
        A = SC.nextInt();
        switch(A){
            case 1:RGType();
            break;
            case 2:EditUser();
            break;
            case 3: ViewAll();
            break;
            case 4:Remove();
            break;
        }
    }

    public void RGType() throws IOException {
        int A;
        System.out.println("1. Sales Manager");
        System.out.println("2. Purchase Manager");
        System.out.println("3. Administrator");
        System.out.println("4. Quit");
        System.out.print("Selection:");
        A = SC.nextInt();
        switch(A){
            case 1:AddUser("SalesManager");
            break;
            case 2: AddUser("PurchaseManager");
            break;
            case 3:AddUser("Administrator");
        }
    }

    public void AddUser(String A) throws IOException {
        String ID = "", PW, temp;
        System.out.print("Enter Preferred ID:");
        temp = SC.next();
        if(itemValidator(temp) == true){
            System.out.println("Username Registered. Choose another");
            AddUser(A);

        }
        else{
            ID = temp;
        }
        System.out.println("Enter Password:");
        PW = SC.next();


        BufferedWriter BW = new BufferedWriter(new FileWriter("User.txt", true));
        BW.write(ID + ":" + PW.hashCode() + ":" + A);
        BW.flush();
        BW.newLine();
        BW.close();

    }

    public void EditUser() throws IOException {
        String Result;
        String itemID = null;
        String temp;
        String nn;
        String[] Array = new String[5];
        File tempDB = new File("temp.txt");
        File db = new File("User.txt");
        BufferedReader BR = new BufferedReader(new FileReader(db));
        BufferedWriter BW = new BufferedWriter(new FileWriter(tempDB));

        System.out.print("Enter User ID:");
        temp = SC.next();

        if (itemValidator(temp) == true) {
            itemID = temp;
        } else {
            System.out.println("Incorrect. Try again");
            EditUser();
        }

        System.out.println("New Password:");
        nn = SC.next();
        while ((Result = BR.readLine()) != null && itemID != null) {
            if (Result.contains(itemID)) {
                Array = Result.split(":");
                String ID = Array[0];
                String abc = Array[1];
                String SPID = Array[2];

                Result = Result.replace(abc, nn);
                BW.write(Result);
                BW.flush();
                BW.newLine();

            } else {
                BW.write(Result);
                BW.flush();
                BW.newLine();

            }
        }
        BW.close();
        BR.close();
        db.delete();
        tempDB.renameTo(db);
    }
    public void Remove() throws IOException {
        String SearchID, Result;
        File tempDB = new File("Tempt.txt");
        File Ori = new File("User.txt");

        BufferedReader BR = new BufferedReader(new FileReader(Ori));
        BufferedWriter BW = new BufferedWriter(new FileWriter(tempDB));

        System.out.print("Enter Item ID:");
        SearchID = SC.next();


        while ((Result = BR.readLine()) != null) {
            if (Result.contains(SearchID))
                continue;
            BW.write(Result);
            BW.flush();
            BW.newLine();
        }

        BR.close();
        BW.close();
        Ori.delete();
        tempDB.renameTo(Ori);
    }
    public void ViewAll() throws IOException {
        BufferedReader BR = new BufferedReader(new FileReader("User.txt"));
        String Result;
        while ((Result = BR.readLine()) != null) {
            for (String w : Result.split(":")) {
                System.out.print(w + "\t");
            }
            System.out.println("\n");
        }
        BR.close();
    }
    public boolean itemValidator(String A) throws IOException {
        BufferedReader BR = new BufferedReader(new FileReader("User.txt"));
        String Result;
        while ((Result = BR.readLine()) != null) {
            if (Result.contains(A)) {
                BR.close();
                return true;
            }
        }
        BR.close();
        return false;
    }
    public String Login(String A, String B) throws IOException {
        String[] C = new String[2];
        BufferedReader BR = new BufferedReader(new FileReader("User.txt"));
        String Result;
        while ((Result = BR.readLine()) != null) {
                C = Result.split(":");
                String ID = C[0];
                String PW = C[1];
                String POS = C[2];
            if (ID.equals(A) && PW.equals(B)) {
                BR.close();
                return POS;
            }
        }
        BR.close();
        return "";
    }
}


