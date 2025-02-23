using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    //[SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseGameMenu;

    void Start()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        //gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        //gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        //gameOverMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        pauseGameMenu.SetActive(true);
        mainMenu.SetActive(false);
        //gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        //gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit game");
    }
}
