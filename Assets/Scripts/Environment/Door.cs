using UnityEngine;

public class Door : MonoBehaviour
{
    private SceneController sceneController;

    private void Awake()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("NextLevelDoor"))
            {
                sceneController.LoadNextLevel();
            }
            else if (gameObject.CompareTag("RetryDoor"))
            {
                sceneController.Retry();
            }
            else if (gameObject.CompareTag("TitleScreenDoor"))
            {
                sceneController.LoadTitleScreen();
            }
        }
    }
}