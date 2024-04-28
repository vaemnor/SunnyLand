using UnityEngine;

public class Danger : MonoBehaviour
{
    private GameController gameController;
    private PlayerMovement playerMovement;

    private Collider2D dangerCollider;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        dangerCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 collisionPoint = collision.GetContact(0).point;

            if (collisionPoint.x < dangerCollider.bounds.center.x)
            {
                playerMovement.Rebound(-1f);
            }
            else if (collisionPoint.x > dangerCollider.bounds.center.x)
            {
                playerMovement.Rebound(1f);
            }

            gameController.HurtOrKillPlayer();
        }
    }
}