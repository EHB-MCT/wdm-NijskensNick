using UnityEngine;

public class CloseSpot : MonoBehaviour
{
    [SerializeField] int spotNumber = 0;

    public void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Customer"))
        {
            switch(spotNumber)
            {
                case 1:
                    {
                        ManageWaitingSpots.instance.spot1Full = false;
                        break;
                    }
                case 2:
                    {
                        ManageWaitingSpots.instance.spot2Full = false;
                        break;
                    }
                case 3:
                    {
                        ManageWaitingSpots.instance.spot3Full = false;
                        break;
                    }
            }
        }
    }
}
