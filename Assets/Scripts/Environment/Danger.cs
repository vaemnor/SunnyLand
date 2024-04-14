using UnityEngine;

public class Danger : MonoBehaviour
{
    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.CheckIfLivesAreGreaterThanOne();
        }
    }
}