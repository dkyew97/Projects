package com.company;

        import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        String rainAnswer;
        int totalBus;
        int c;
        long workTime;
        boolean rain;
        Scanner sc = new Scanner(System.in);
        System.out.println("BUS DEPOT SIMULATION START!!!");
        System.out.println("How many Buses will enter today?");
        totalBus = sc.nextInt();
        System.out.println("How long will each worker work?");
        workTime = sc.nextLong();
        System.out.println("How many Cleaners will be needed?");
        c = sc.nextInt();
        System.out.println("Raining today? (Y/N)");
        rainAnswer = sc.next();
        switch (rainAnswer){
            case "y": rain = true; break;
            case "Y": rain = true; break;
            case "n": rain = false; break;
            case "N": rain = false; break;
            default:
                System.out.println("Try Again"); Main.main(args);return;
        }
        Station st = new Station(workTime);
        MechanicWork fo = new MechanicWork(st, "Mechanic 1");
        MechanicWork fo1 = new MechanicWork(st,"Mechanic 2");
        VehicleGenerator vg = new VehicleGenerator(st, totalBus);
        int i = 1;
        if (!rain) {
            while (i <= c) {
                CleanerWork bs = new CleanerWork(st, "Cleaner " + i);
                Thread threadBus = new Thread(bs);
                threadBus.start();
                threadBus.setPriority(Thread.MIN_PRIORITY);
                i++;
            }
        }
        else{
            System.out.println("Raining today. Cleaning will not happen today.");
        }
        Thread threadFO = new Thread(fo);
        Thread threadFO1 = new Thread(fo1);
        Thread threadVG = new Thread(vg);
        threadFO.start();
        threadFO.setPriority(Thread.MAX_PRIORITY);
        threadFO1.start();
        threadFO1.setPriority(Thread.MAX_PRIORITY);
        threadVG.start();
    }
}
