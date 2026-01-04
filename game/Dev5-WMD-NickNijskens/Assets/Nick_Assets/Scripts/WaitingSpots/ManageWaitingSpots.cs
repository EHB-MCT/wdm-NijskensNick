using UnityEngine;
using UnityEngine.UIElements;

public class ManageWaitingSpots : MonoBehaviour
{
    [Header("---Link Waiting Spots---")]
    [SerializeField] GameObject spot1;
    public bool spot1Full = false;
    [SerializeField] GameObject spot2;
    public bool spot2Full = false;
    [SerializeField] GameObject spot3;
    public bool spot3Full = false;

    public static ManageWaitingSpots instance;

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

    public Vector3 GetOpenSpot()
    {
        if(!spot1Full)
        {
            spot1Full = true;
            return spot1.transform.position;
        }
        if(!spot2Full)
        {
            spot2Full = true;
            return spot2.transform.position;
        }
        spot3Full = true;
        return spot3.transform.position;
    }
}
