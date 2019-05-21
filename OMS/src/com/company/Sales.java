package com.company;

import java.io.*;
import java.util.Scanner;

public class Sales {
    public void SalesMenu() throws IOException {
        Scanner SC = new Scanner(System.in);
        String temp;
        String ItemID="";
        int Stock;
        int Sales;
        int nStock;
        System.out.println("Enter Item ID:");
        temp = SC.next();

        if (itemValidator(temp) == true) {
            ItemID = temp;
        } else {
            System.out.println("Try Again.");
            SalesMenu();
        }

        System.out.println("Enter Quantity:");
        Sales = SC.nextInt();
        Stock = StockRetriever("TP1");
        nStock = Stock - Sales;
        StockSet(ItemID, nStock);
        StockWrite(ItemID, Sales);
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

    public int StockRetriever(String A) throws IOException {

        String[] Stock = new String[3];
        BufferedReader BR = new BufferedReader(new FileReader("Item.txt"));
        String Result;
        while ((Result = BR.readLine()) != null) {
            if (Result.contains(A)) {
                Stock = Result.split(":");
                String ID = Stock[0];
                String abc = Stock[1];
                String SPID = Stock[2];
                String C = Stock[3];
                BR.close();
                return Integer.parseInt(C);
            }
        }
        BR.close();
        return 0;
    }

    public void StockSet(String A, int B) throws IOException {
        String[] Stock = new String[3];
        File tempDB = new File("temp.txt");
        File db = new File("Item.txt");
        BufferedReader BR = new BufferedReader(new FileReader(db));
        BufferedWriter BW = new BufferedWriter(new FileWriter(tempDB));
        String Result;
        while ((Result = BR.readLine()) != null) {
            if (Result.contains(A)) {
                Stock = Result.split(":");
                String ID = Stock[0];
                String abc = Stock[1];
                String SPID = Stock[2];
                String C = Stock[3];
                Result = Result.replace(C, Integer.toString(B));
                BW.write(Result);
            }
        }
        BW.close();
        BR.close();
        db.delete();
        tempDB.renameTo(db);

    }
    public void StockWrite(String A, int b) throws IOException {
        BufferedWriter BW = new BufferedWriter(new FileWriter("Sales.txt", true));
        BW.write(A + ":" + b);
        BW.flush();
        BW.newLine();
        BW.close();
    }
}
