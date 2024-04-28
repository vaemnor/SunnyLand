using UnityEngine;

public class Danger : MonoBehaviour
{
    private GameController gameController;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerMovement.transform.position.x < transform.position.x)
            {
                playerMovement.Rebound(-1f);
            }
            else if (playerMovement.transform.position.x > transform.position.x)
            {
                playerMovement.Rebound(1f);
            }

            gameController.HurtOrKillPlayer();
        }
    }
}