package com.company;

import java.io.*;
import java.util.Scanner;

public class Item implements Interface{


    String ItemID;
    String SupplierID;
    String ItemName;

    Scanner SC = new Scanner(System.in);


    public void ItemMenu() throws IOException {

        String Choice = "Item";
        System.out.println(Choice + " Entry");
        System.out.println("1.Add " + Choice + "");
        System.out.println("2.Edit " + Choice + ":");
        System.out.println("3.View All " + Choice + ":");
        System.out.println("4.Delete " + Choice + ";");
        System.out.print("Selection:");
        int Selection = SC.nextInt();
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
                ItemMenu();
                break;
        }
    }

    @Override
    public void Add() throws IOException {

        String quan;
        System.out.print("Enter Supplier ID:");
        String Temp;
        Temp = SC.next();
        if (Validator(Temp) == true) {
            SupplierID = Temp;
        } else {
            System.out.println("Invalid Supplier. Try Again");
            this.Add();
        }
        System.out.println("Item ID:TP" + MaxLimit());
        System.out.print("Item Name:");
        ItemName = SC.next();
        ItemID = "TP" + MaxLimit();
        System.out.print("Enter Quantity:");
        quan = SC.next();

        BufferedWriter BW = new BufferedWriter(new FileWriter("Item.txt", true));
        BW.write(ItemID + ":" + ItemName + ":" + SupplierID + ":" + quan);
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
        File db = new File("Item.txt");
        BufferedReader BR = new BufferedReader(new FileReader(db));
        BufferedWriter BW = new BufferedWriter(new FileWriter(tempDB));

        System.out.print("Enter Item ID:");
        temp = SC.next();

        if (itemValidator(temp) == true) {
            itemID = temp;
        } else {
            System.out.println("Incorrect. Try again");
            Edit();
        }

        System.out.println("New Item Name:");
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
        BufferedReader BR = new BufferedReader(new FileReader("Item.txt"));
        String Result;
        System.out.println("------------------------------------------------------------- ");
        System.out.println("ID\t\t\tName\t\tSupplier\t\tStock 		         ");
        System.out.println("------------------------------------------------------------- ");
        while ((Result = BR.readLine()) != null) {
            for (String w : Result.split(":")) {
                System.out.print(w+"\t\t\t");
            }
            System.out.println("\n");
        }

        BR.close();
    }

    @Override
    public void Delete() throws IOException {

        String SearchID, Result;
        File tempDB = new File("Tempt.txt");
        File Ori = new File("Item.txt");

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


    public int MaxLimit() throws IOException {
        BufferedReader reader = new BufferedReader(new FileReader("Item.txt"));
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

    public boolean itemValidator(String A) throws IOException {
        BufferedReader BR = new BufferedReader(new FileReader("Item.txt"));
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
