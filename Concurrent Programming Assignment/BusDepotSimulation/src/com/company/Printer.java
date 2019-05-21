package com.company;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;

public class Printer {
    public String printFree(){ return "Control: Ramp is Free";}
    public String printBusy(){return "Control: Ramp is Busy";}
    public String printApproach(String name,String type){return name+ " "+type +" Depot at " + LocalTime.now().format(DateTimeFormatter.ofPattern("HH:mm:ss"));}
    public String printPark(){return "Parking Full";}
    public String printRequest(String name, String type){return name + ": Requesting " + type;}
    public String printProceed(String name){return "Control: " +name+" Clear to Proceed";}
    public String printAcquire(String name){return name + " Acquiring Ramp at " +LocalTime.now().format(DateTimeFormatter.ofPattern("HH:mm:ss"));}
    public String printDuration(String name, long duration){return name + ": Took " + duration + " seconds to enter Waiting area";}
    public String printEntered(String name, String area){return name + ": Entered " + area +" Area at "+ LocalTime.now().format(DateTimeFormatter.ofPattern("HH:mm:ss"));}
    public String printWait(String name){return name + ": Waiting for bus";}
    public String printWork(String worker, String bus, String work){return worker +": "+work+" " + bus+ " at "+ LocalTime.now().format(DateTimeFormatter.ofPattern("HH:mm:ss"));}
    public String printDone(String worker, String bus, long time, String work){return worker +": Completed "+work+" " + bus + " in " + time + " seconds.";}
    public String printComplete(){return "Completed at " + LocalTime.now().format(DateTimeFormatter.ofPattern("HH:mm:ss"));}
    public String printProceedExit(String name){return "Control: "+name+ ", Please proceed to Exit Area";}
    public String printExitDuration(String bus, long duration){return bus + ": Left depot in " + duration + " second";}
    public String printExitTime(String bus){return bus + ": Left Depot at " + LocalTime.now().format(DateTimeFormatter.ofPattern("HH:mm:ss"));}
    public String printStart(String name){return  name + " Service started";}
    public String printStop(String name){return name+": Stop Working";}
    public String printClose(){return "Last 30 minutes. Come Back Tomorrow. Remaining Bus will be Served";}
}
