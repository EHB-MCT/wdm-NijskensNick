using System;
using UnityEngine;

public class Furnace_manager : MonoBehaviour
{
    [Header("---Link Food Preps---")]
    [SerializeField] GameObject foodPrep1;
    [SerializeField] GameObject foodPrep2;
    [SerializeField] GameObject foodPrep3;
    [SerializeField] GameObject foodPrep4;

    public enum FurnaceOutputs
    {
        hotdogPlaced,
        returnHotdog,
        full,
        inventoryEmpty
    }

    public FurnaceOutputs InteractWithFurnace()
    {
        if(foodPrep1.GetComponent<Cook_Hotdog>().stage == Cook_Hotdog.CookingStage.finished)
        {
            if(Player_Inventory_Manager.instance.inventory.cookedHotDogs < Player_Inventory_Manager.instance.inventory.maxCookedHotDogs)
            {
                foodPrep1.GetComponent<Cook_Hotdog>().ResetCooking();
                return FurnaceOutputs.returnHotdog;
            }
        }
        if(foodPrep2.GetComponent<Cook_Hotdog>().stage == Cook_Hotdog.CookingStage.finished)
        {
            if(Player_Inventory_Manager.instance.inventory.cookedHotDogs < Player_Inventory_Manager.instance.inventory.maxCookedHotDogs)
            {
                foodPrep2.GetComponent<Cook_Hotdog>().ResetCooking();
                return FurnaceOutputs.returnHotdog;
            }
        }
        if(foodPrep3.GetComponent<Cook_Hotdog>().stage == Cook_Hotdog.CookingStage.finished)
        {
            if(Player_Inventory_Manager.instance.inventory.cookedHotDogs < Player_Inventory_Manager.instance.inventory.maxCookedHotDogs)
            {
                foodPrep3.GetComponent<Cook_Hotdog>().ResetCooking();
                return FurnaceOutputs.returnHotdog;
            }
        }
        if(foodPrep4.GetComponent<Cook_Hotdog>().stage == Cook_Hotdog.CookingStage.finished)
        {
            if(Player_Inventory_Manager.instance.inventory.cookedHotDogs < Player_Inventory_Manager.instance.inventory.maxCookedHotDogs)
            {
                foodPrep4.GetComponent<Cook_Hotdog>().ResetCooking();
                return FurnaceOutputs.returnHotdog;
            }
        }


        if(foodPrep1.GetComponent<Cook_Hotdog>().stage == Cook_Hotdog.CookingStage.empty)
        {
            if(Player_Inventory_Manager.instance.inventory.rawHotDogs > 0)
            {   
                foodPrep1.GetComponent<Cook_Hotdog>().StartCooking();
                return FurnaceOutputs.hotdogPlaced;
            }
            else
            {
                return FurnaceOutputs.inventoryEmpty;
            }
        }if(foodPrep2.GetComponent<Cook_Hotdog>().stage == Cook_Hotdog.CookingStage.empty)
        {
            if(Player_Inventory_Manager.instance.inventory.rawHotDogs > 0)
            {   
                foodPrep2.GetComponent<Cook_Hotdog>().StartCooking();
                return FurnaceOutputs.hotdogPlaced;
            }
            else
            {
                return FurnaceOutputs.inventoryEmpty;
            }
        }if(foodPrep3.GetComponent<Cook_Hotdog>().stage == Cook_Hotdog.CookingStage.empty)
        {
            if(Player_Inventory_Manager.instance.inventory.rawHotDogs > 0)
            {   
                foodPrep3.GetComponent<Cook_Hotdog>().StartCooking();
                return FurnaceOutputs.hotdogPlaced;
            }
            else
            {
                return FurnaceOutputs.inventoryEmpty;
            }
        }if(foodPrep4.GetComponent<Cook_Hotdog>().stage == Cook_Hotdog.CookingStage.empty)
        {
            if(Player_Inventory_Manager.instance.inventory.rawHotDogs > 0)
            {   
                foodPrep4.GetComponent<Cook_Hotdog>().StartCooking();
                return FurnaceOutputs.hotdogPlaced;
            }
            else
            {
                return FurnaceOutputs.inventoryEmpty;
            }
        }
        else
        {
            return FurnaceOutputs.full;
        }
    }
}
