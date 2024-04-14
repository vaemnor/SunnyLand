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
        if (collision.CompareTag("Player") == true)
        {
            sceneController.LoadNextLevel();
        }
    }
}