/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package libraryrmi;

import java.util.Scanner;

/**
 *
 * @author Darryl Yew
 */
public class LibraryMenu {
    int Selection;
    Scanner sc = new Scanner(System.in);
    public void menu(){
        System.out.println("1. Borrow");
        System.out.println("2. List Book");
        System.out.println("3. Report");

    }
    public void firstMenu(){
        System.out.println("Welcome to APU Distributed Library System");
        System.out.println("1. Login");
        System.out.println("2. Registration");
    }
}
