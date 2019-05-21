package com.company;

public class Vehicle implements Runnable{
    private String name;
    Station st;

    public Vehicle(Station st){
        this.st = st;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getName() { return name; }

    public void run() {
        enterDepot();
    }
    private synchronized void enterDepot(){
        System.out.println(this.getName()+" Waiting In-line");
        st.addDepot(this);
    }
}
