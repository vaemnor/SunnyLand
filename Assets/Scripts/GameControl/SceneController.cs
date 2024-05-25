using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private GameController gameController;
    private PlayerController playerController;

    private Animator sceneTransition;

    /// <summary>
    /// The amount of time (in seconds) to wait for the GameOverScreen scene to load.
    /// </summary>
    [Tooltip("The amount of time (in seconds) to wait for the GameOverScreen scene to load.")]
    [SerializeField] private float loadGameOverScreenWaitTime = 0f;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        sceneTransition = GameObject.Find("SceneTransition").GetComponent<Animator>();
    }

    public void LoadTitleScreen()
    {
        StartCoroutine(StartSceneTransitionAndLoadScene("TitleScreen"));
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(StartSceneTransitionAndLoadScene("Level1"));
    }

    public void LoadNextLevel()
    {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            StartCoroutine(StartSceneTransitionAndLoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            StartCoroutine(StartSceneTransitionAndLoadScene(0));
        }
    }

    public IEnumerator LoadGameOverScreen()
    {
        yield return new WaitForSeconds(loadGameOverScreenWaitTime);

        StartCoroutine(StartSceneTransitionAndLoadScene("GameOverScreen"));
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

    private IEnumerator StartSceneTransitionAndLoadScene(int sceneBuildIndex)
    {
        sceneTransition.SetTrigger("startSceneTransition");

        yield return new WaitForSeconds(sceneTransition.GetCurrentAnimatorClipInfo(0).Length);

        SceneManager.LoadScene(sceneBuildIndex);
    }

    private IEnumerator StartSceneTransitionAndLoadScene(string sceneName)
    {
        sceneTransition.SetTrigger("startSceneTransition");

        yield return new WaitForSeconds(sceneTransition.GetCurrentAnimatorClipInfo(0).Length);

        SceneManager.LoadScene(sceneName);
    }
}