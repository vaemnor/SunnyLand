using System.Collections;
using UnityEngine;

public class PointItem : MonoBehaviour
{
    protected GameController gameController;
    protected PlayerController playerController;

    protected SpriteRenderer spriteRenderer;
    protected Collider2D pointItemCollider;
    protected AudioSource audioSource;

    [SerializeField] [Range(1, 100)] protected int pointsToAdd;

    [SerializeField] protected GameObject itemFeedbackVFX;
    [SerializeField] protected AudioClip pointItemFeedbackSFX;
    [SerializeField] [Range(0, 1)] protected float pointItemFeedbackSFXVolume = 0f;

    protected virtual void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        pointItemCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true && playerController.CanCollectItems)
        {
            gameController.AddPoints(pointsToAdd);

            CreateItemFeedbackVFX();
            StartCoroutine(CreateItemFeedbackSFXAndDestroy());
        }
    }

    protected void CreateItemFeedbackVFX()
    {
        Instantiate(itemFeedbackVFX, transform.position, transform.rotation);
    }

    protected IEnumerator CreateItemFeedbackSFXAndDestroy()
    {
        spriteRenderer.enabled = false;
        pointItemCollider.enabled = false;
        audioSource.PlayOneShot(pointItemFeedbackSFX, pointItemFeedbackSFXVolume);

        yield return new WaitForSeconds(pointItemFeedbackSFX.length);

        Destroy(gameObject);
    }
}