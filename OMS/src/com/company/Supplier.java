package com.company;

import java.io.*;
import java.util.Scanner;

public class Supplier implements Interface {
    String SupplierID;
    String SupplierName;
    Scanner SC = new Scanner(System.in);

    public void SupplierMenu() throws IOException {
        int Selection;
        String Choice = "Supplier";
        System.out.println(Choice + " Entry");
        System.out.println("1.Add " + Choice + "");
        System.out.println("2.Edit " + Choice + ":");
        System.out.println("3.View All " + Choice + ":");
        System.out.println("4.Delete " + Choice + ";");
        System.out.print("Selection:");
        Selection = SC.nextInt();
        Selection(Selection);
    }

    public void Selection(int Input) throws IOException {
        switch (Input) {
            case 1:
                this.Add();
                break;
            case 2:
                this.Edit();
                break;
            case 3:
                this.View();
                break;
            case 4:
                this.Delete();
                break;
            default:
                System.out.println("Wrong Input. Try again");
                SupplierMenu();
                break;
        }
    }

    @Override
    public void Add() throws IOException {

        System.out.print("Supplier ID:SP" + MaxLimit());
        SupplierID = "SP" + MaxLimit();
        System.out.print("Enter Supplier Name:");
        SupplierName = SC.next();

        BufferedWriter BW = new BufferedWriter(new FileWriter("Supplier.txt", true));
        BW.write(SupplierID + ":" + SupplierName);
        BW.flush();
        BW.newLine();
        BW.close();
    }

    @Override
    public void Edit() throws IOException {
        String Result;
        String itemID = null;
        String temp;
        String nn;
        String[] Array = new String[5];
        File tempDB = new File("temp.txt");
        File db = new File("Supplier.txt");
        BufferedReader BR = new BufferedReader(new FileReader(db));
        BufferedWriter BW = new BufferedWriter(new FileWriter(tempDB));

        System.out.print("Enter Supplier ID:");
        temp = SC.next();

        if (Validator(temp) == true) {
            itemID = temp;
        } else {
            System.out.println("Incorrect. Try again");
            Edit();
        }

        System.out.println("New Supplier Name:");
        nn = SC.next();
        while ((Result = BR.readLine()) != null && itemID != null) {
            if (Result.contains(itemID)) {
                Array = Result.split(":");
                String ID = Array[0];
                String abc = Array[1];

                Result = Result.replace(abc, nn);
                BW.write(Result);

                BW.flush();
                BW.newLine();

            }
            else{
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

        BufferedReader BR = new BufferedReader(new FileReader("Supplier.txt"));
        String Result;
        System.out.println("------------------------------------------------------------- ");
        System.out.println("ID\t\t\tName 		         ");
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
        File Ori = new File("Supplier.txt");

        BufferedReader BR = new BufferedReader(new FileReader(Ori));
        BufferedWriter BW = new BufferedWriter(new FileWriter(tempDB));

        System.out.print("Enter Supplier ID:");
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

    public int MaxLimit() throws IOException {
        BufferedReader reader = new BufferedReader(new FileReader("Supplier.txt"));
        int lines = 0;
        while (reader.readLine() != null) lines++;
        reader.close();
        return lines + 1;
    }
    public boolean Validator(String A) throws IOException {
        BufferedReader BR = new BufferedReader(new FileReader("Supplier.txt"));
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
}
