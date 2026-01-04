using UnityEngine;
using UnityEngine.AI;

public class MoveCustomer : MonoBehaviour
{
    [Header("---Link end position---")]
    [SerializeField] GameObject endPosition;
    public Vector3 targetPosition;
    private NavMeshAgent agent;
    public bool hasHotdog;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        LinkTarget();
    }

    public void LinkTarget()
    {
        targetPosition = ManageWaitingSpots.instance.GetOpenSpot();
        agent.SetDestination(targetPosition);
    }

    public void Stop()
    {
        agent.isStopped = true;
    }

    public void Proceed()
    {
        agent.isStopped = false;
    }

    public void UpdateTargetToEnd()
    {
        agent.SetDestination(endPosition.transform.position);
        Proceed();
    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("WaitingSpot"))
        {
            Stop();
        }
        if(col.gameObject.CompareTag("LeavePoint"))
        {
            CustomerPooler.instance.CollectCustomer(this.gameObject);
        }
    }
}
