/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Modal;

import java.io.Serializable;
import java.sql.Timestamp;

/**
 *
 * @author Darryl Yew
 */
public class TransactionObject implements Serializable {
    private String username,booktitle;
    private int duration;
    private Timestamp d;

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getBooktitle() {
        return booktitle;
    }

    public void setBooktitle(String booktitle) {
        this.booktitle = booktitle;
    }

    public int getDuration() {
        return duration;
    }

    public void setDuration(int duration) {
        this.duration = duration;
    }

    public Timestamp getD() {
        return d;
    }

    public void setD(Timestamp d) {
        this.d = d;
    }
}
