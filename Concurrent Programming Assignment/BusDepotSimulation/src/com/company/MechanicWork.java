package com.company;

import java.time.LocalTime;

public class MechanicWork extends Printer implements Runnable {
    Station st;
    private String name;
    private LocalTime d1 = LocalTime.now().plusSeconds(30);

    public MechanicWork(Station st, String name) {
        this.st = st;
        this.name = name;
    }
    public void run(){
        try {
            Thread.sleep(1000);
        } catch (InterruptedException iex) {
            iex.printStackTrace();
        }
        System.out.println(printStart(name));
        while(LocalTime.now().isBefore(d1) || st.postWaitingList.size()!=0 || st.preWaitingList.size()!=0 || st.serviceWaitingList.size() !=0) {
            st.serviceBus(name);
        }
        System.out.println(printStop(name));
    }
}