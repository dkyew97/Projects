package com.company;

import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.Semaphore;
import java.util.concurrent.TimeUnit;

public class Station extends Printer{
    Semaphore sm = new Semaphore(1);
    List<Vehicle> preWaitingList;
    List<Vehicle> serviceWaitingList;
    List<Vehicle> postWaitingList ;
    long workTime;
    long waitTime;
    int i = 1;
    int totalBus;
    public Station(long workTime) {
        this.workTime = workTime;
        preWaitingList = new LinkedList<>();
        serviceWaitingList = new LinkedList<>();
        postWaitingList = new LinkedList<>();
    }
    public void addDepot(Vehicle bs) {
        synchronized (preWaitingList) {

            ((LinkedList<Vehicle>) preWaitingList).offer(bs);
            System.out.println(printApproach(bs.getName(),"Approaching"));
            while (preWaitingList.size() != 0 && serviceWaitingList.size() > 10) {
                try {
                    System.out.println(printPark());
                    preWaitingList.wait();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
            addServiceDepot();
        }
    }
    public void addServiceDepot() {
        synchronized (serviceWaitingList) {
            Vehicle bs;
            bs = (Vehicle) ((LinkedList<?>) preWaitingList).poll();
            long duration = 0;
            System.out.println(printRequest(bs.getName(),"Entrance"));
            try {
                sm.acquire();
                System.out.println(printProceed(bs.getName()));
                System.out.println(printAcquire(bs.getName()));
                System.out.println(printBusy());
                duration = (long) (Math.random() * ((3 - 2) + 1)) + 2;
                TimeUnit.SECONDS.sleep(duration);
                waitTime = waitTime + duration;
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            if (serviceWaitingList.size() == 1) {
                serviceWaitingList.notify();
            }
            ((LinkedList<Vehicle>) serviceWaitingList).offer(bs);
            System.out.println(printDuration(bs.getName(),duration));
            System.out.println(printEntered(bs.getName(),"Waiting"));
            sm.release();
            System.out.println(printFree());
        }
    }
    public void serviceBus(String name) {
        Vehicle bs;
        synchronized (serviceWaitingList) {
            while (serviceWaitingList.size() == 0) {
                System.out.println(printWait(name));
                try {
                    serviceWaitingList.wait();
                    return;
                } catch (InterruptedException iex) {
                    iex.printStackTrace();
                }
            }
            if (serviceWaitingList.size() >= 1) {
                serviceWaitingList.notify();
            }
            bs = (Vehicle) ((LinkedList<?>) serviceWaitingList).poll();
            System.out.println(printEntered(bs.getName(),"Service"));
            synchronized (serviceWaitingList){
                if(serviceWaitingList.size()<10){
                    serviceWaitingList.notify();
                }
            }
        }
        try {
            System.out.println(printWork(name,bs.getName(),"Servicing"));
            TimeUnit.SECONDS.sleep(workTime);
            waitTime = waitTime + workTime;
        } catch (InterruptedException iex) {
            iex.printStackTrace();
        }
        System.out.println(printDone(name,bs.getName(),workTime,"Service"));
        System.out.println(printComplete());
        System.out.println(printProceedExit(bs.getName()));
        ((LinkedList<Vehicle>) postWaitingList).offer(bs);
        synchronized (preWaitingList){
            if(preWaitingList.size()!=0){
                preWaitingList.notify();
            }
        }
        leaveDepot();
    }
    public void washBus(String name) {
        Vehicle bs;
        synchronized (serviceWaitingList) {
            while (serviceWaitingList.size() == 0) {
                System.out.println(printWait(name));
                try {
                    serviceWaitingList.wait();
                    return;
                } catch (InterruptedException iex) {
                    iex.printStackTrace();
                }
            }
            if(serviceWaitingList.size() >= 1){
                serviceWaitingList.notify();
            }
            bs = (Vehicle) ((LinkedList<?>) serviceWaitingList).poll();
            System.out.println(printEntered(bs.getName(),"Cleaning"));
        }
        try {
            System.out.println(printWork(name,bs.getName(),"Cleaning"));
            TimeUnit.SECONDS.sleep(workTime);
            waitTime = waitTime + workTime;
        } catch (InterruptedException iex) {
            iex.printStackTrace();
        }
        System.out.println(printDone(name,bs.getName(),workTime,"Cleaning"));
        System.out.println(printProceedExit(bs.getName()));
        ((LinkedList<Vehicle>) postWaitingList).offer(bs);
        synchronized (preWaitingList){
            if(preWaitingList.size()!=0){
                preWaitingList.notify();
            }
        }
        leaveDepot();
    }
    public void leaveDepot() {
        synchronized (postWaitingList) {
            Vehicle bs;
            while (postWaitingList.size() >= 1) {
                postWaitingList.notify();
                bs = (Vehicle) ((LinkedList<?>) postWaitingList).poll();
                System.out.println(printRequest(bs.getName(), "Exit"));
                long duration = 0;
                try {
                    sm.acquire();
                    System.out.println(printProceed(bs.getName()));
                    System.out.println(printApproach(bs.getName(),"Exiting"));
                    System.out.println(printBusy());
                    duration = (long) (Math.random() * ((3 - 2) + 1)) + 2;
                    TimeUnit.SECONDS.sleep(duration);
                    waitTime = waitTime + duration;
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                System.out.println(printExitDuration(bs.getName(),duration));
                System.out.println(printExitTime(bs.getName()));
                sm.release();
                i++;
                System.out.println(printFree());

                synchronized (serviceWaitingList){
                        serviceWaitingList.notify();
                }
                synchronized (preWaitingList){
                        preWaitingList.notify();
                }
                synchronized (postWaitingList){
                        postWaitingList.notify();
                }

            }
        }
        statistic(i);
    }
    public void statistic(int i) {
        synchronized (this) {
            int time = (int) waitTime;
            int avg;
            if (i == totalBus) {
                System.out.println("Total Wait time is: " + waitTime);
                System.out.println("Total number of Buses:" + i);
                avg = time / i;
                System.out.println("Average waiting time: " + avg);

            } else {
                return;
            }
            i++;
        }
    }
}