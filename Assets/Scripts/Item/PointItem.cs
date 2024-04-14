using UnityEngine;

public class PointItem : MonoBehaviour
{
    private GameController gameController;

    [SerializeField] private int pointsToAdd;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            gameController.AddPoints(pointsToAdd);
            Destroy(gameObject);
        }
    }
}