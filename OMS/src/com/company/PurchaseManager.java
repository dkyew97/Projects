package com.company;

import java.io.IOException;
import java.util.Scanner;

public class PurchaseManager {
    String PMID;

    public void PMenu() throws IOException {

        Scanner SC = new Scanner(System.in);
        System.out.println("1. List Item");
        System.out.println("2. List Supplier ");
        System.out.println("3. Display Requisition");
        System.out.println("4. Create Purchase Orders");
        System.out.println("5. List Purchase Orders");
        System.out.print("Selection:");
        int A = SC.nextInt();

        Selection(A);
    }

    public void Selection(int Input) throws IOException {
        switch (Input) {
            case 1:
                Item IT = new Item();
                IT.View();
                PMenu();
                break;
            case 2:
                Supplier SP = new Supplier();
                SP.View();
                PMenu();
                break;
            case 3:
                PurchaseRequisition DS = new PurchaseRequisition();
                DS.View();
                PMenu();
                break;
            case 4:
                PurchaseOrder PR = new PurchaseOrder();
                PR.PMID = PMID;
                PR.Add();
                PMenu();
                break;
            case 5:
                PurchaseOrder PO = new PurchaseOrder();
                PO.View();
                PMenu();
                break;
        }
    }
}
