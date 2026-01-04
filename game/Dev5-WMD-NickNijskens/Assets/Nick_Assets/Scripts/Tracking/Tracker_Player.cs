using UnityEngine;

public class Tracker_Player : MonoBehaviour
{
    [Header("---Tracking mode---")]
    [SerializeField] bool playerMayNotPassThrough = false;
    [SerializeField] bool playerMayNotStandStill = false;
    [Header("---Debug Mode---")]
    [SerializeField] bool logSpeed = false;
    [SerializeField] bool logTracking = false;

    private float playerSpeed = 0;

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(playerMayNotPassThrough)
            {
                if(logTracking)
                {
                    Debug.Log("player entered");
                }
                Tracker_PassThrough.instance.StartTracking();
            }
            if(playerMayNotStandStill)
            {
                playerSpeed = ConvertVectorToFloat(col.gameObject.GetComponent<Rigidbody>().linearVelocity);
                if(logSpeed)
                {
                    Debug.Log(playerSpeed);
                }
            }
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(playerMayNotStandStill && !playerMayNotPassThrough)
            {
                playerSpeed = ConvertVectorToFloat(col.gameObject.GetComponent<Rigidbody>().linearVelocity);
                if(logSpeed)
                {
                    Debug.Log(playerSpeed);
                }
                if(playerSpeed == 0 && Tracker_StandingStill.instance.isTracking)
                {
                    Tracker_StandingStill.instance.StopTracking();
                }
                if(playerSpeed != 0 && !Tracker_StandingStill.instance.isTracking)
                {
                    Tracker_StandingStill.instance.StartTracking();
                }
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(logTracking)
            {
                Debug.Log("player left");
            }
            if(playerMayNotPassThrough)
            {
                Tracker_PassThrough.instance.StopTracking();
            }
            if(playerMayNotStandStill && !playerMayNotPassThrough)
            {
                Tracker_StandingStill.instance.StopTracking();
            }
        }
    }

    private float ConvertVectorToFloat(Vector3 vector)
    {
        return Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y) + (vector.z * vector.z));
    }
}
