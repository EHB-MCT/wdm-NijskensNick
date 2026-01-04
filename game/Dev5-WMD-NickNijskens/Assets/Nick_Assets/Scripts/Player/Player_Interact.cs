using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    [Header("---Layer Mask---")]
    [SerializeField] LayerMask layerMask;

    public void OnInteract()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if(hit.collider.CompareTag("Furnace"))
            {
                if(hit.collider.gameObject.GetComponent<Furnace_manager>() != null)
                {
                    switch(hit.collider.gameObject.GetComponent<Furnace_manager>().InteractWithFurnace())
                    {
                        case Furnace_manager.FurnaceOutputs.hotdogPlaced:
                            {
                                Player_Inventory_Manager.instance.inventory.RemoveRawHotDog();
                                break;
                            }
                        case Furnace_manager.FurnaceOutputs.returnHotdog:
                            {
                                Player_Inventory_Manager.instance.inventory.AddCookedHotDog();
                                break; 
                            }
                        case Furnace_manager.FurnaceOutputs.full:
                            {
                                // Do nothing
                                break;
                            }
                        case Furnace_manager.FurnaceOutputs.inventoryEmpty:
                            {
                                // Do nothing
                                break;
                            }
                    }
                }
            }
            if(hit.collider.CompareTag("Countertop"))
            {
                if(hit.collider.gameObject.GetComponent<Countertop_Manager>() != null)
                {
                    hit.collider.gameObject.GetComponent<Countertop_Manager>().InteractWithCountertop();
                }
                else
                {
                    Debug.Log("script error");
                }
            }
            if(hit.collider.CompareTag("Customer"))
            {
                if(!Player_Inventory_Manager.instance.inventory.HasNoFinishedHotdogs())
                {
                    if(hit.collider.gameObject.GetComponent<MoveCustomer>() != null)
                    {
                        hit.collider.gameObject.GetComponent<MoveCustomer>().UpdateTargetToEnd();
                        Player_Inventory_Manager.instance.inventory.RemoveFinishedHotDog();
                    }
                }
            }
            if(hit.collider.CompareTag("BunsBox"))
            {
                Player_Inventory_Manager.instance.inventory.AddBun();
            }
            if(hit.collider.CompareTag("SausageBox"))
            {
                Player_Inventory_Manager.instance.inventory.AddRawHotDog();
            }
        }
    }
}
