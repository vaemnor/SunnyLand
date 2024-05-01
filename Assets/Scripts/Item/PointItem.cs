using UnityEngine;

public class PointItem : MonoBehaviour
{
    protected GameController gameController;

    [SerializeField] protected GameObject itemFeedbackVFX;

    [SerializeField] protected int pointsToAdd;

    protected virtual void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            gameController.AddPoints(pointsToAdd);
            CreateItemFeedbackVFX();
            Destroy(gameObject);
        }
    }

    protected void CreateItemFeedbackVFX()
    {
        Instantiate(itemFeedbackVFX, transform.position, transform.rotation);
    }
}