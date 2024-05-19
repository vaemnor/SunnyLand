using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private GameController gameController;

    /// <summary>
    /// The amount of time (in seconds) to wait for the GameOverScreen scene to load.
    /// </summary>
    [Tooltip("The amount of time (in seconds) to wait for the GameOverScreen scene to load.")]
    [SerializeField] private float loadGameOverScreenWaitTime = 0f;

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

    public IEnumerator LoadGameOverScreen()
    {
        yield return new WaitForSeconds(loadGameOverScreenWaitTime);

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