using System;
using System.Collections;
using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Networking;

public class Tracker_PassThrough : MonoBehaviour
{
    public static Tracker_PassThrough instance;
    private PassingThroughPair trackingPair;
    public bool isTracking = false;
    private LoggedIdentifiers logs = new LoggedIdentifiers();
    private const string FILE_NAME = "logs.json";
    private string filePath;

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

    public void StartTracking()
    {
        trackingPair = new PassingThroughPair();
        trackingPair.userName = logs.username;
        // change username
        StartCoroutine(PostPassThroughPair("localhost:3000/PassingThroughPairs"));
        isTracking = true;
    }

    public void StopTracking()
    {
        trackingPair.endTime = DateTime.Now;
        trackingPair.ended = true;
        StartCoroutine(PutPassingThroughPair("localhost:3000/PassingThroughPairs/" + trackingPair.db_id));
        isTracking = false;
    }

    IEnumerator PostPassThroughPair(String uri)
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

    IEnumerator PutPassingThroughPair(String uri)
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Put(uri, JsonConvert.SerializeObject(trackingPair)))
        {
            webRequest.uploadHandler.contentType = "application/json";
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
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
