using System.Collections;
using UnityEngine;

public class LifeItem : MonoBehaviour
{
    private GameController gameController;
    private PlayerController playerController;

    private SpriteRenderer spriteRenderer;
    private Collider2D lifeItemCollider;
    private AudioSource audioSource;

    [SerializeField] [Range(1, 100)] private int livesToAdd;

    [SerializeField] private GameObject itemFeedbackVFX;
    [SerializeField] private AudioClip lifeItemFeedbackSFX;
    [SerializeField] [Range(0, 1)] private float lifeItemFeedbackSFXVolume = 0f;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        lifeItemCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true && playerController.CanCollectItems)
        {
            gameController.AddLives(livesToAdd);

            CreateItemFeedbackVFX();
            StartCoroutine(CreateItemFeedbackSFXAndDestroy());
        }
    }

    private void CreateItemFeedbackVFX()
    {
        Instantiate(itemFeedbackVFX, transform.position, transform.rotation);
    }

    private IEnumerator CreateItemFeedbackSFXAndDestroy()
    {
        spriteRenderer.enabled = false;
        lifeItemCollider.enabled = false;
        audioSource.PlayOneShot(lifeItemFeedbackSFX, lifeItemFeedbackSFXVolume);

        yield return new WaitForSeconds(lifeItemFeedbackSFX.length);

        Destroy(gameObject);
    }
}