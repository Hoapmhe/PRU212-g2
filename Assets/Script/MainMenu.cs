using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
        Debug.Log("start game");
    }

    public void QuitGame()
    {

        Application.Quit();
        Debug.Log("quit game");
    }
}
