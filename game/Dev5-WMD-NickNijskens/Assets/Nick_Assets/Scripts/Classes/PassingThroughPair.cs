using System;
using Unity.VisualScripting;

public class PassingThroughPair
{
    public String db_id;
    public String deviceName;
    public String userName;
    public DateTime startTime;
    public DateTime endTime;
    public bool ended;

    public PassingThroughPair()
    {
        db_id = "";
        deviceName = System.Environment.MachineName;
        userName = "gameUser";
        startTime = DateTime.Now;
        endTime = startTime;
        ended = false;
    }

    public PassingThroughPair(String userName)
    {
        db_id = "";
        deviceName = System.Environment.MachineName;
        this.userName = userName;
        startTime = DateTime.Now;
        endTime = startTime;
        ended = false;
    }
}