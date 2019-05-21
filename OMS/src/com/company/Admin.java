package com.company;

import java.io.IOException;
import java.util.Scanner;

public class Admin {
    String ADID;
    Scanner SC = new Scanner(System.in);

    public void ADMenu() throws IOException {
        int Selection;
        System.out.println("1. Item ");
        System.out.println("2. Supplier ");
        System.out.println("3. Daily Item-wise Sales Entry");
        System.out.println("4. Purchase Requisition");
        System.out.println("5. Purchase Order");
        System.out.println("6. User");
        System.out.println("7. Quit");
        System.out.print("Selection:");
        Selection = SC.nextInt();
        Selection(Selection);

    }

    public void Selection(int Input) throws IOException {
        switch (Input) {
            case 1:
                Item IT = new Item();
                IT.ItemMenu();
                ADMenu();
                break;
            case 2:
                Supplier SP = new Supplier();
                SP.SupplierMenu();
                ADMenu();
                break;
            case 3:
                Sales DS = new Sales();
                DS.SalesMenu();
                ADMenu();
                break;
            case 4:
                PurchaseRequisition PR = new PurchaseRequisition();
                PR.UserID = ADID;
                PR.PRMenu();
                ADMenu();
                break;
            case 5:
                PurchaseOrder PO = new PurchaseOrder();
                PO.PMID = ADID;
                PO.POMenu();
                ADMenu();
                break;
            case 6:
                Registration RG = new Registration();
                RG.RGMenu();
                ADMenu();
                break;
            case 7:

            default:
                System.out.println("Wrong Input. Try again");
                ADMenu();
                break;
        }

    }
}
