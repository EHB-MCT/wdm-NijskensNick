using UnityEngine;

public class CustomerPooler : MonoBehaviour
{
    [Header("---Parent Setup---")]
    [SerializeField] GameObject activeCustomers;
    [SerializeField] GameObject inactiveCustomers;
    [Header("---Activate parameters---")]
    [SerializeField] GameObject spawnLocation;
    [SerializeField] float spawnDelay = 2.0f;
    private float timer = 0f;
    public static CustomerPooler instance;

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

    public void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnDelay)
        {
            SpawnCustomer();
            timer = 0f;
        }
    }

    private void SpawnCustomer()
    {
        if(inactiveCustomers.transform.childCount == 0)
        {
            return;
        }
        inactiveCustomers.transform.GetChild(0).transform.position = spawnLocation.transform.position;
        inactiveCustomers.transform.GetChild(0).GetComponent<MoveCustomer>().hasHotdog = false;
        inactiveCustomers.transform.GetChild(0).gameObject.SetActive(true);
        inactiveCustomers.transform.GetChild(0).GetComponent<MoveCustomer>().LinkTarget();
        inactiveCustomers.transform.GetChild(0).transform.SetParent(activeCustomers.transform);
    }

    public void CollectCustomer(GameObject customer)
    {
        customer.gameObject.transform.SetParent(inactiveCustomers.transform);
        customer.SetActive(false);
    }
}
