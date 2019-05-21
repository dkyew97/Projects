package com.company;

import java.io.IOException;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) throws IOException {
        Scanner  SC = new Scanner (System.in);
        System.out.println("Welcome to Order Management System");
        System.out.println("Please Login");
        System.out.print("Enter ID:");
        String A = SC.next();
        System.out.print("Enter Password:");
        String B = SC.next();

        Registration RG = new Registration();


        if(RG.Login(A,Integer.toString(B.hashCode())).equals("Administrator")){

            Admin AD = new Admin();
            AD.ADID =A;
            AD.ADMenu();

        }
        else if(RG.Login(A,Integer.toString(B.hashCode())).equals("SalesManager")){
            SalesManager SM = new SalesManager();
            SM.SMID = A;
            SM.SalesManagerMenu();


        }
        else if(RG.Login(A,Integer.toString(B.hashCode())).equals("PurchaseManager")){
            PurchaseManager PR = new PurchaseManager();
            PR.PMID = A;
            PR.PMenu();

        }
    }

}
