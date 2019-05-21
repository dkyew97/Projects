package com.company;
import java.time.LocalTime;
import java.util.concurrent.TimeUnit;
public class VehicleGenerator extends Printer implements Runnable{
    Station st;
    private int a;
    private int i = 1;
    LocalTime d1 = LocalTime.now().plusSeconds(30);
    public VehicleGenerator(){

    }
    public VehicleGenerator(Station st, int a){
        this.st = st;
        this.a = a;
    }
    public void run() {
        while  (LocalTime.now().isBefore(d1) && i<=a) {
            synchronized (this) {
                Vehicle bs = new Vehicle(st);
                Thread threadBus = new Thread(bs);
                bs.setName("Bus " + i);
                threadBus.start();
                try {
                    TimeUnit.SECONDS.sleep((long) (Math.random() * ((3 - 2) + 1)) + 2);
                } catch (InterruptedException iex) {
                    iex.printStackTrace();
                }
                ++i;
                st.totalBus = i;
            }

        }
        System.out.println(printClose());
    }
    public int getI(){
        return i;
    }
    public void setI(int i){
        this.i = i;
    }
}