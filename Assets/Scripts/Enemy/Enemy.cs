using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected GameController gameController;
    protected PlayerMovement playerMovement;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigidBody;
    protected Collider2D enemyCollider;
    protected AudioSource audioSource;

    [Tooltip("The material to switch to during the flash.")]
    [SerializeField] protected Material flashMaterial;

    [Tooltip("The duration of the flash.")]
    [SerializeField] protected float flashDuration;

    [SerializeField] protected GameObject enemyDeathVFX;
    [SerializeField] protected AudioClip enemyDeathSFX;
    [SerializeField] [Range(0, 1)] protected float enemyDeathSFXVolume = 0f;

    protected bool isDying = false;

    protected virtual void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDying)
        {
            if (collision.GetContact(0).normal.y == -1f)
            {
                playerMovement.JumpAfterKillingEnemy();

                isDying = true;

                StartCoroutine(Flash());
                CreateEnemyDeathVFX();
                StartCoroutine(CreateEnemyDeathSFXAndDestroy());
            }
            else
            {
                if (playerMovement.transform.position.x < transform.position.x)
                {
                    gameController.HurtOrKillPlayer(-1f);
                }
                else if (playerMovement.transform.position.x > transform.position.x)
                {
                    gameController.HurtOrKillPlayer(1f);
                }
            }
        }
    }

    protected IEnumerator Flash()
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Pause the execution of this function for "flashDuration" seconds.
        yield return new WaitForSeconds(flashDuration);

        // After the pause, hide the enemy.
        spriteRenderer.enabled = false;
    }

    protected void CreateEnemyDeathVFX()
    {
        Instantiate(enemyDeathVFX, transform.position, transform.rotation);
    }

    protected IEnumerator CreateEnemyDeathSFXAndDestroy()
    {
        enemyCollider.enabled = false;
        audioSource.PlayOneShot(enemyDeathSFX, enemyDeathSFXVolume);

        yield return new WaitForSeconds(enemyDeathSFX.length);

        Destroy(gameObject);
    }
}