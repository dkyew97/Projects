package com.company;

import java.io.IOException;
import java.util.Scanner;

public class SalesManager {
    String SMID ;
    Scanner SC = new Scanner(System.in);

    public void SalesManagerMenu() throws IOException {


        System.out.println("1. Item Entry");
        System.out.println("2. Supplier Entry");
        System.out.println("3. Daily Item-wise Sales Entry");
        System.out.println("4. Request Purchase Requisition");
        System.out.println("5. Display Requisition");
        System.out.println("6. List of Purchase Orders");
        System.out.println("7. Quit");
        System.out.print("Selection:");
        int Selection = SC.nextInt();
        Selection(Selection);

    }

    public void Selection(int Input) throws IOException {
        switch (Input) {
            case 1:
                Item IT = new Item();
                IT.ItemMenu();
                SalesManagerMenu();
                break;
            case 2:
                Supplier SP = new Supplier();
                SP.SupplierMenu();
                SalesManagerMenu();
                break;
            case 3:
                Sales DS = new Sales();
                DS.SalesMenu();
                SalesManagerMenu();
                break;
            case 4:
                PurchaseRequisition PR = new PurchaseRequisition();
                PR.UserID = SMID;
                PR.Add();
                SalesManagerMenu();
                break;
            case 5:
                PR = new PurchaseRequisition();
                PR.View();
                SalesManagerMenu();
                break;
            case 6:
                PurchaseOrder PO = new PurchaseOrder();
                PO.View();
                SalesManagerMenu();
                break;

        }
    }
}
