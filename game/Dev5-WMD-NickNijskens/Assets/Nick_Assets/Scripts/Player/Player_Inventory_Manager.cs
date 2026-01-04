using UnityEngine;

public class Player_Inventory_Manager : MonoBehaviour
{
    public Player_Inventory inventory;
    public static Player_Inventory_Manager instance;
    public void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void Start()
    {
        inventory = new Player_Inventory();
    }
}
