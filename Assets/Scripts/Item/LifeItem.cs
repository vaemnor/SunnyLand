using UnityEngine;

public class LifeItem : MonoBehaviour
{
    private GameController gameController;

    [SerializeField] private GameObject itemFeedbackVFX;

    [SerializeField] private int livesToAdd;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            gameController.AddLives(livesToAdd);
            CreateItemFeedbackVFX();
            Destroy(gameObject);
        }
    }

    private void CreateItemFeedbackVFX()
    {
        Instantiate(itemFeedbackVFX, transform.position, transform.rotation);
    }
}