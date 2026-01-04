using UnityEngine;

public class Countertop_Manager : MonoBehaviour
{
    [Header("---Link Food Preps---")]
    [SerializeField] GameObject foodPrep1;
    [SerializeField] GameObject foodPrep2;
    [SerializeField] GameObject foodPrep3;
    [SerializeField] GameObject foodPrep4;
    public void InteractWithCountertop()
    {
        // Place sausages
        if(!foodPrep1.GetComponent<Make_Hotdog>().sausageActive && !Player_Inventory_Manager.instance.inventory.HasNoCookedHotdogs())
        {
            Player_Inventory_Manager.instance.inventory.RemoveCookedHotDog();
            foodPrep1.GetComponent<Make_Hotdog>().PlaceSausage();
            return;
        }
        if(!foodPrep2.GetComponent<Make_Hotdog>().sausageActive && !Player_Inventory_Manager.instance.inventory.HasNoCookedHotdogs())
        {
            Player_Inventory_Manager.instance.inventory.RemoveCookedHotDog();
            foodPrep2.GetComponent<Make_Hotdog>().PlaceSausage();
            return;
        }
        if(!foodPrep3.GetComponent<Make_Hotdog>().sausageActive && !Player_Inventory_Manager.instance.inventory.HasNoCookedHotdogs())
        {
            Player_Inventory_Manager.instance.inventory.RemoveCookedHotDog();
            foodPrep3.GetComponent<Make_Hotdog>().PlaceSausage();
            return;
        }
        if(!foodPrep4.GetComponent<Make_Hotdog>().sausageActive && !Player_Inventory_Manager.instance.inventory.HasNoCookedHotdogs())
        {
            Player_Inventory_Manager.instance.inventory.RemoveCookedHotDog();
            foodPrep4.GetComponent<Make_Hotdog>().PlaceSausage();
            return;
        }

        // Open closed bun
        if(!foodPrep1.GetComponent<Make_Hotdog>().bunOpenActive && foodPrep1.GetComponent<Make_Hotdog>().bunClosedActive)
        {
            foodPrep1.GetComponent<Make_Hotdog>().OpenBun();
            return;
        }
        if(!foodPrep2.GetComponent<Make_Hotdog>().bunOpenActive && foodPrep2.GetComponent<Make_Hotdog>().bunClosedActive)
        {
            foodPrep2.GetComponent<Make_Hotdog>().OpenBun();
            return;
        }
        if(!foodPrep3.GetComponent<Make_Hotdog>().bunOpenActive && foodPrep3.GetComponent<Make_Hotdog>().bunClosedActive)
        {
            foodPrep3.GetComponent<Make_Hotdog>().OpenBun();
            return;
        }
        if(!foodPrep4.GetComponent<Make_Hotdog>().bunOpenActive && foodPrep4.GetComponent<Make_Hotdog>().bunClosedActive)
        {
            foodPrep4.GetComponent<Make_Hotdog>().OpenBun();
            return;
        }

        // Place closed bun
        if(!foodPrep1.GetComponent<Make_Hotdog>().bunOpenActive && !foodPrep1.GetComponent<Make_Hotdog>().bunClosedActive && !Player_Inventory_Manager.instance.inventory.HasNoBuns())
        {
            Player_Inventory_Manager.instance.inventory.RemoveBun();
            foodPrep1.GetComponent<Make_Hotdog>().PlaceBun();
            return;
        }
        if(!foodPrep2.GetComponent<Make_Hotdog>().bunOpenActive && !foodPrep2.GetComponent<Make_Hotdog>().bunClosedActive && !Player_Inventory_Manager.instance.inventory.HasNoBuns())
        {
            Player_Inventory_Manager.instance.inventory.RemoveBun();
            foodPrep2.GetComponent<Make_Hotdog>().PlaceBun();
            return;
        }
        if(!foodPrep3.GetComponent<Make_Hotdog>().bunOpenActive && !foodPrep3.GetComponent<Make_Hotdog>().bunClosedActive && !Player_Inventory_Manager.instance.inventory.HasNoBuns())
        {
            Player_Inventory_Manager.instance.inventory.RemoveBun();
            foodPrep3.GetComponent<Make_Hotdog>().PlaceBun();
            return;
        }
        if(!foodPrep4.GetComponent<Make_Hotdog>().bunOpenActive && !foodPrep4.GetComponent<Make_Hotdog>().bunClosedActive && !Player_Inventory_Manager.instance.inventory.HasNoBuns())
        {
            Player_Inventory_Manager.instance.inventory.RemoveBun();
            foodPrep4.GetComponent<Make_Hotdog>().PlaceBun();
            return;
        }

        // Collect finished hotdogs
        if(foodPrep1.GetComponent<Make_Hotdog>().hotdogActive && Player_Inventory_Manager.instance.inventory.HasRoomForFinishedHotdogs())
        {
            Player_Inventory_Manager.instance.inventory.AddFinishedHotDog();
            foodPrep1.GetComponent<Make_Hotdog>().ResetMaking();
            return;
        }
        if(foodPrep2.GetComponent<Make_Hotdog>().hotdogActive && Player_Inventory_Manager.instance.inventory.HasRoomForFinishedHotdogs())
        {
            Player_Inventory_Manager.instance.inventory.AddFinishedHotDog();
            foodPrep2.GetComponent<Make_Hotdog>().ResetMaking();
            return;
        }
        if(foodPrep3.GetComponent<Make_Hotdog>().hotdogActive && Player_Inventory_Manager.instance.inventory.HasRoomForFinishedHotdogs())
        {
            Player_Inventory_Manager.instance.inventory.AddFinishedHotDog();
            foodPrep3.GetComponent<Make_Hotdog>().ResetMaking();
            return;
        }
        if(foodPrep4.GetComponent<Make_Hotdog>().hotdogActive && Player_Inventory_Manager.instance.inventory.HasRoomForFinishedHotdogs())
        {
            Player_Inventory_Manager.instance.inventory.AddFinishedHotDog();
            foodPrep4.GetComponent<Make_Hotdog>().ResetMaking();
            return;
        }

        // Combine hotdogs
        if(foodPrep1.GetComponent<Make_Hotdog>().sausageActive && foodPrep1.GetComponent<Make_Hotdog>().bunOpenActive)
        {
            foodPrep1.GetComponent<Make_Hotdog>().CombineIngredients();
            return;
        }
        if(foodPrep2.GetComponent<Make_Hotdog>().sausageActive && foodPrep2.GetComponent<Make_Hotdog>().bunOpenActive)
        {
            foodPrep2.GetComponent<Make_Hotdog>().CombineIngredients();
            return;
        }
        if(foodPrep3.GetComponent<Make_Hotdog>().sausageActive && foodPrep3.GetComponent<Make_Hotdog>().bunOpenActive)
        {
            foodPrep3.GetComponent<Make_Hotdog>().CombineIngredients();
            return;
        }
        if(foodPrep4.GetComponent<Make_Hotdog>().sausageActive && foodPrep4.GetComponent<Make_Hotdog>().bunOpenActive)
        {
            foodPrep4.GetComponent<Make_Hotdog>().CombineIngredients();
            return;
        }
    }
}
