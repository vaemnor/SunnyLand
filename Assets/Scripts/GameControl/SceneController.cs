using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadNextLevel()
    {
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            LoadGameOverScreen();
        }
    }

    public void LoadGameOverScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

    public void Retry()
    {
        gameController.ResetLivesAndPoints();
        LoadFirstLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}