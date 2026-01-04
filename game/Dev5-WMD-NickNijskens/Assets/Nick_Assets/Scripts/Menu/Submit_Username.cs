using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class Submit_Username : MonoBehaviour
{
    [Header("---Link Input Field---")]
    [SerializeField] TextMeshProUGUI textfield;

    private LoggedIdentifiers logs = new LoggedIdentifiers();
    private const string FILE_NAME = "logs.json";
    private string filePath;

    public void Awake()
    {
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

    public void SubmitUsername()
    {
        logs.username = textfield.GetComponent<TextMeshProUGUI>().text;
        File.WriteAllText(filePath, JsonConvert.SerializeObject(logs));
    }
}
