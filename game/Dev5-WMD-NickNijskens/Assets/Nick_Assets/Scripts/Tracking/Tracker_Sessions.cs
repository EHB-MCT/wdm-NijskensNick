using System;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Tracker_Sessions : MonoBehaviour
{
    public static Tracker_Sessions instance;
    private Session trackingPair;
    public bool isTracking = false;
    private LoggedIdentifiers logs = new LoggedIdentifiers();
    private const string FILE_NAME = "logs.json";
    private string filePath;
    [Header("---Round Timing---")]
    [SerializeField] float roundTime = 10f;
    private float timer = 0f;

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

        filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);
        if(File.Exists(filePath))
        {
            logs = JsonConvert.DeserializeObject<LoggedIdentifiers>(File.ReadAllText(filePath));
        }
        else
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(logs));
        }
    }

    public void Start()
    {
        StartTracking();
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if(timer >= roundTime)
        {
            StopTracking();
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void StartTracking()
    {
        trackingPair = new Session();
        trackingPair.userName = logs.username;
        // change username
        StartCoroutine(PostSession("localhost:3000/Sessions"));
        isTracking = true;
    }

    public void StopTracking()
    {
        trackingPair.endTime = DateTime.Now;
        trackingPair.ended = true;
        StartCoroutine(PutSession("localhost:3000/Sessions/" + trackingPair.db_id));
        isTracking = false;
    }

    IEnumerator PostSession(String uri)
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Post(uri, JsonConvert.SerializeObject(trackingPair), "application/json"))
        {
            yield return webRequest.SendWebRequest();

            switch(webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    {
                        Debug.Log("Something went wrong: " + webRequest.error);
                        break;
                    }
                case UnityWebRequest.Result.Success:
                    {
                        trackingPair.db_id = webRequest.downloadHandler.text;
                        break;
                    }
            }
        }
    }

    IEnumerator PutSession(String uri)
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Put(uri, JsonConvert.SerializeObject(trackingPair)))
        {
            webRequest.uploadHandler.contentType = "application/json";
            yield return webRequest.SendWebRequest();

            switch(webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    {
                        Debug.Log("Something went wrong: " + webRequest.error);
                        break;
                    }
                case UnityWebRequest.Result.Success:
                    {
                        break;
                    }
            }
        }
    }
}
