using UnityEngine;
using UnityEngine.Events;

public class Player_GroundDetection : MonoBehaviour
{
    [Header ("---GROUND DETECTION---")]
    [SerializeField] LayerMask hitLayers;
    [SerializeField] float hitDistance = 1f;
    [SerializeField] float hitRadius = 0.5f;
    [SerializeField] bool debugMode = false;
    Vector3 debugPosition;
    static public Player_GroundDetection instance;
    public UnityAction<bool> isGroundedEvent;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsGrounded(Vector3 position)
    {
        if(Physics.SphereCast(position, hitRadius, Vector3.down, out RaycastHit hit, hitDistance, hitLayers))
        {
            if(debugMode)
            {
                debugPosition = hit.point;
                Debug.DrawRay(position, Vector3.down * hitDistance, Color.green, 0.1f);
            }
            isGroundedEvent?.Invoke(true);
            return true;
        }
        else
        {
            if(debugMode)
            {
                debugPosition = position;
                Debug.DrawRay(position, Vector3.down * 0.5f, Color.red, 0.1f);
            }
            isGroundedEvent?.Invoke(false);
            return false;
        }
    }

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(!debugMode) return;
        else
        {
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawSphere(debugPosition, hitRadius);
        }
    }
    #endif
}
