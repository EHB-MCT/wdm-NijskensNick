using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_Game : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay_scene");
    }
}
