package com.company;

import java.io.*;
import java.util.Scanner;

public class PurchaseRequisition implements Interface{
    String ItemID, UserID, Date, PRID;
    Scanner SC = new Scanner(System.in);

    public void PRMenu() throws IOException {
        int a;
        System.out.println("1. Create Purchase Requisition:");
        System.out.println("2. View Requisition");
        System.out.print("Selection");
        a = SC.nextInt();
        switch(a){
            case 1:Add();break;
            case 2: View();break;
        }
    }

    @Override
    public void Add() throws IOException {
        System.out.print("Item ID:");
        ItemID = SC.next();
        System.out.println("Purchase Requisition ID: PR" + MaxLimit());
        PRID = "PR" + MaxLimit();
        System.out.print("Date Wanted in format DD/MM/YYYY:");
        Date = SC.next();
        System.out.println("Quantity:");
        String q = SC.next();
        BufferedWriter BW = new BufferedWriter(new FileWriter("PR.txt", true));
        BW.write(PRID + ":" + ItemID + ":" + UserID + ":" + Date + ":" + q +":" +"Pending");
        BW.flush();
        BW.newLine();
        BW.close();
    }

    @Override
    public void Edit() throws IOException {

    }

    @Override
    public void View() throws IOException {
        BufferedReader BR = new BufferedReader(new FileReader("PR.txt"));
        String Result;
        System.out.println("------------------------------------------------------------------------------------------ ");
        System.out.println("PR ID\t\tItem ID\t\tUser\t\tDate\t\t\t\tQuantity\t\tStatus");
        System.out.println("------------------------------------------------------------------------------------------ ");
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

    }

    public int MaxLimit() throws IOException {
        BufferedReader reader = new BufferedReader(new FileReader("PR.txt"));
        int lines = 0;
        while (reader.readLine() != null) lines++;
        reader.close();
        return lines + 1;
    }
}
