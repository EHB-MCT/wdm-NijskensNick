using System.Diagnostics;

public class Player_Inventory
{
    public int rawHotDogs;
    public int cookedHotDogs;
    public int finishedHotDogs;
    public int buns;
    public int maxRawHotDogs;
    public int maxCookedHotDogs;
    public int maxFinishedHotDogs;
    public int maxBuns;

    public Player_Inventory()
    {
        rawHotDogs = 0;
        cookedHotDogs = 0;
        finishedHotDogs = 0;
        buns = 0;
        maxRawHotDogs = 10;
        maxCookedHotDogs = 10;
        maxFinishedHotDogs = 10;
        maxBuns = 10;
    }

    public void AddRawHotDog()
    {
        if(rawHotDogs < maxRawHotDogs)
        {
            rawHotDogs++;
        }
    }
    public void RemoveRawHotDog()
    {
        if(rawHotDogs > 0)
        {
            rawHotDogs--;
        }
    }
    public void AddCookedHotDog()
    {
        if(cookedHotDogs < maxCookedHotDogs)
        {
            cookedHotDogs++;
        }
    }
    public void RemoveCookedHotDog()
    {
        if(cookedHotDogs > 0)
        {
            cookedHotDogs--;
        }
    }
    public void AddFinishedHotDog()
    {
        if(finishedHotDogs < maxFinishedHotDogs)
        {
            finishedHotDogs++;
        }
    }
    public void RemoveFinishedHotDog()
    {
        if(finishedHotDogs > 0)
        {
            finishedHotDogs--;
        }
    }
    public void AddBun()
    {
        if(buns < maxBuns)
        {
            buns++;
        }
    }

    public void RemoveBun()
    {
        if(buns > 0)
        {
            buns--;
        }
    }

    public bool HasRoomForFinishedHotdogs()
    {
        if(finishedHotDogs < maxFinishedHotDogs)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasNoFinishedHotdogs()
    {
        if(finishedHotDogs == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasRoomForRawHotdogs()
    {
        if(rawHotDogs < maxRawHotDogs)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasNoRawHotdogs()
    {
        if(rawHotDogs == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasRoomForCookedHotdogs()
    {
        if(cookedHotDogs < maxCookedHotDogs)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasNoCookedHotdogs()
    {
        if(cookedHotDogs == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasRoomForBuns()
    {
        if(buns < maxBuns)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasNoBuns()
    {
        if(buns == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}