/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Modal;

import java.io.Serializable;

/**
 *
 * @author Darryl Yew
 */
public class LibraryBook implements Serializable{
    private String title;
    private String author;
    private String publisher;

    public String getA() {
        return title;
    }

    public void setA(String a) {
        this.title = a;
    }

    public String getB() {
        return author;
    }

    public void setB(String b) {
        this.author = b;
    }

    public String getC() {
        return publisher;
    }

    public void setC(String c) {
        this.publisher = c;
    }
    
}
