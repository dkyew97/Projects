package com.company;


import java.io.*;
import java.util.Scanner;

public class PurchaseOrder implements Interface {
    Scanner SC = new Scanner(System.in);
    PurchaseManager PR = new PurchaseManager();
    String PRID;
    String PMID;
    String POID;


    public void POMenu() throws IOException {
        int a;
        System.out.println("1. Create Purchase Order:");
        System.out.println("2. View Order");
        System.out.print("Selection");
        a = SC.nextInt();
        switch(a){
            case 1:Add();break;
            case 2: View();break;
        }
    }
    @Override
    public void Add() throws IOException {
        int choice;

        System.out.println("Enter Purchase Requisition ID:");
        String A = SC.next();
        if (Validator(A) == true) {
            PRID = A;
        } else {
            System.out.println("Incorrect. Try Again");
            Add();
        }

        System.out.println("1. Accept");
        System.out.println("2. Reject");
        choice = SC.nextInt();
        switch (choice) {
            case 1:
                System.out.println("PO" + MaxLimit());
                POID = "PO" + MaxLimit();
                BufferedWriter BW = new BufferedWriter(new FileWriter("PO.txt", true));
                BW.write(PMID + ":" + PRID + ":" + POID);
                BW.flush();
                BW.newLine();
                BW.close();
                Edit();
                break;
            case 2:
                RejectPR();
                break;
        }

    }

    @Override
    public void Edit() throws IOException {
        String Result;
        String itemID = null;
        String[] Array = new String[5];
        File tempDB = new File("temp.txt");
        File db = new File("PR.txt");
        BufferedReader BR = new BufferedReader(new FileReader(db));
        BufferedWriter BW = new BufferedWriter(new FileWriter(tempDB));


        while ((Result = BR.readLine()) != null) {
            if (Result.contains(PRID)) {
                Array = Result.split(":");
                String ID = Array[0];
                String abc = Array[1];
                String SPID = Array[2];
                String Datt = Array[3];
                String qan = Array[4];
                String PRID = Array[5];

                Result = Result.replace(PRID, "Approved");
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


    @Override
    public void View() throws IOException {
        BufferedReader BR = new BufferedReader(new FileReader("PO.txt"));
        String Result;
        System.out.println("------------------------------------------------------------- ");
        System.out.println("User\t\tPR ID\t\tPurchase Order ID 		         ");
        System.out.println("------------------------------------------------------------- ");
        while ((Result = BR.readLine()) != null) {
            for (String w : Result.split(":")) {
                System.out.print(w + "\t\t\t");
            }
            System.out.println("\n");
        }
        BR.close();
    }

    @Override
    public void Delete() throws IOException {
        String SearchID, Result;
        File tempDB = new File("Tempt.txt");
        File Ori = new File("PO.txt");

        BufferedReader BR = new BufferedReader(new FileReader(Ori));
        BufferedWriter BW = new BufferedWriter(new FileWriter(tempDB));

        System.out.print("Enter Purchase Order ID:");
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

    public boolean Validator(String A) throws IOException {
        BufferedReader BR = new BufferedReader(new FileReader("PR.txt"));
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

    public int MaxLimit() throws IOException {
        BufferedReader reader = new BufferedReader(new FileReader("PO.txt"));
        int lines = 0;
        while (reader.readLine() != null) lines++;
        reader.close();
        return lines + 1;
    }

    public void RejectPR() throws IOException {
        String Result;
        String itemID = null;
        String[] Array = new String[5];
        File tempDB = new File("temp.txt");
        File db = new File("PR.txt");
        BufferedReader BR = new BufferedReader(new FileReader(db));
        BufferedWriter BW = new BufferedWriter(new FileWriter(tempDB));

        while ((Result = BR.readLine()) != null) {
            if (Result.contains(PRID)) {
                Array = Result.split(":");
                String ID = Array[0];
                String abc = Array[1];
                String SPID = Array[2];
                String Datt = Array[3];
                String qan = Array[4];
                String PRID = Array[5];

                Result = Result.replace(PRID, "Reject");
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

}