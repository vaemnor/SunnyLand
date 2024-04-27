using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected GameController gameController;
    protected PlayerMovement playerMovement;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigidBody;
    protected Collider2D enemyCollider;

    [SerializeField] protected GameObject enemyDeathVFX;

    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] protected Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] protected float flashDuration;

    protected bool isDying = false;

    protected virtual void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDying)
        {
            Vector2 collisionPoint = collision.GetContact(0).point;

            if (collisionPoint.y >= enemyCollider.bounds.max.y -0.125f) // -0.125 because collision detection is faulty when enemies move on Y-axis
            {
                playerMovement.MakePlayerGoUp();

                isDying = true;

                StartCoroutine(FlashAndDestroy());
                CreateEnemyDeathVFX();
            }
            else
            {
                gameController.HurtOrKillPlayer();
            }
        }
    }

    protected IEnumerator FlashAndDestroy()
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Pause the execution of this function for "flashDuration" seconds.
        yield return new WaitForSeconds(flashDuration);

        // After the pause, destroy the enemy.
        Destroy(gameObject);
    }

    protected void CreateEnemyDeathVFX()
    {
        Instantiate(enemyDeathVFX, transform.position, transform.rotation);
    }
}