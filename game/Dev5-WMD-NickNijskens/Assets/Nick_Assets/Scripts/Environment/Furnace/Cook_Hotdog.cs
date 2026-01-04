using System;
using UnityEngine;

public class Cook_Hotdog : MonoBehaviour
{
    [Header("---Link Prefabs---")]
    [SerializeField] GameObject uncooked_hotdog;
    [SerializeField] GameObject cooked_hotdog;
    [Header("---Cooking parameters---")]
    [SerializeField] float cooking_time = 0f;
    private float timer = 0f;
    private bool cooking = false;
    public enum CookingStage
    {
        empty,
        cooking,
        finished
    }
    public CookingStage stage = CookingStage.empty;
    public void Start()
    {
        uncooked_hotdog.SetActive(false);
        cooked_hotdog.SetActive(false);
    }

    public void Update()
    {
        if(cooking)
        {
            timer += Time.deltaTime;
            if(timer >= cooking_time)
            {
                StopCooking();
            }
        }
    }

    public void StartCooking()
    {
        stage = CookingStage.cooking;
        uncooked_hotdog.SetActive(true);
        cooking = true;
    }

    public void StopCooking()
    {
        stage = CookingStage.finished;
        cooking = false;
        uncooked_hotdog.SetActive(false);
        cooked_hotdog.SetActive(true);
    }

    public void ResetCooking()
    {
        stage = CookingStage.empty;
        cooking = false;
        timer = 0f;
        cooked_hotdog.SetActive(false);
        uncooked_hotdog.SetActive(false);
    }
}
