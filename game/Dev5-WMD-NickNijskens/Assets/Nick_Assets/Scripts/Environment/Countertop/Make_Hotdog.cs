using Unity.VisualScripting;
using UnityEngine;

public class Make_Hotdog : MonoBehaviour
{
    [Header("---Link Prefabs---")]
    [SerializeField] GameObject bun_closed;
    public bool bunClosedActive = false;
    [SerializeField] GameObject bun_open;
    public bool bunOpenActive = false;
    [SerializeField] GameObject sausage;
    public bool sausageActive = false;
    [SerializeField] GameObject hotdog;
    public bool hotdogActive = false;

    public void Start()
    {
        ResetMaking();
    }

    public void ResetMaking()
    {
        bun_closed.SetActive(false);
        bunClosedActive = false;
        bun_open.SetActive(false);
        bunOpenActive = false;
        sausage.SetActive(false);
        sausageActive = false;
        hotdog.SetActive(false);
        hotdogActive = false;
    }

    public void PlaceSausage()
    {
        sausageActive = true;
        sausage.SetActive(true);
    }

    public void PlaceBun()
    {
        bunClosedActive = true;
        bun_closed.SetActive(true);
    }

    public void OpenBun()
    {
        bunClosedActive = false;
        bun_closed.SetActive(false);
        bunOpenActive = true;
        bun_open.SetActive(true);
    }

    public void CombineIngredients()
    {
        bunOpenActive = false;
        bun_open.SetActive(false);
        sausageActive = false;
        sausage.SetActive(false);
        hotdogActive = true;
        hotdog.SetActive(true);
    }

    public void MakeHotDog()
    {
        if(!bunClosedActive && !bunOpenActive && !hotdogActive) // Place closed bun
        {
            bunClosedActive = true;
            bun_closed.SetActive(true);
            return;
        }
        if(bunClosedActive) // Open closed bun
        {
            bunClosedActive = false;
            bun_closed.SetActive(false);
            bunOpenActive = true;
            bun_open.SetActive(true);
            return;
        }
        if(!sausageActive && !hotdogActive) // Place sausage
        {
            sausageActive = true;
            sausage.SetActive(true);
            return;
        }
        if(sausageActive && bunOpenActive && !hotdogActive) // Combine sausage and open bun to hotdog
        {
            sausageActive = false;
            sausage.SetActive(false);
            bunOpenActive = false;
            bun_open.SetActive(false);
            hotdogActive = true;
            hotdog.SetActive(true);
            return;
        }
        if(hotdogActive) // Collect hotdog
        {
            ResetMaking();
            return;
        }
    }
}
