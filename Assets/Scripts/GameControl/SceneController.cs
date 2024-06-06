using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private GameController gameController;
    private PlayerController playerController;

    private Animator sceneTransition;

    [Tooltip("The delay (in seconds) for the player's horizontal movement to stop after the player enters a door.")]
    [SerializeField] private float stopPlayerHorizontalMovementDelay = 0f;

    [Tooltip("The delay (in seconds) for the GameOverScreen scene to load after the player dies.")]
    [SerializeField] private float loadGameOverScreenDelay = 0f;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        sceneTransition = GameObject.Find("SceneTransition").GetComponent<Animator>();
    }

    public void LoadTitleScreen()
    {
        gameController.ResetLivesAndPoints();
        StartCoroutine(StartSceneTransitionAndLoadScene("TitleScreen"));
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
        yield return new WaitForSeconds(loadGameOverScreenDelay);

        StartCoroutine(StartSceneTransitionAndLoadScene("GameOverScreen"));
    }

    public void Retry()
    {
        gameController.ResetLevelLivesAndPointsToValuesAtLevelStart();
        ReloadCurrentLevel();
    }

    private void ReloadCurrentLevel()
    {
        StartCoroutine(StartSceneTransitionAndLoadScene(WorldState.Level));
    }

    

    private IEnumerator StartSceneTransitionAndLoadScene(int sceneBuildIndex)
    {
        sceneTransition.SetTrigger("startSceneTransition");
        playerController.PreparePlayerForSceneTransition(stopPlayerHorizontalMovementDelay);

        yield return new WaitForSeconds(sceneTransition.GetCurrentAnimatorClipInfo(0).Length);

        SceneManager.LoadScene(sceneBuildIndex);
    }

    private IEnumerator StartSceneTransitionAndLoadScene(string sceneName)
    {
        sceneTransition.SetTrigger("startSceneTransition");
        playerController.PreparePlayerForSceneTransition(stopPlayerHorizontalMovementDelay);

        yield return new WaitForSeconds(sceneTransition.GetCurrentAnimatorClipInfo(0).Length);

        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(QuitGame());
        }
    }

    public IEnumerator QuitGame()
    {
        sceneTransition.SetTrigger("startSceneTransition");

        yield return new WaitForSeconds(sceneTransition.GetCurrentAnimatorClipInfo(0).Length);

        Application.Quit();
    }
}