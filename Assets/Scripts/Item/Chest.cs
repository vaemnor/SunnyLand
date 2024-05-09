using UnityEngine;

public class Chest : PointItem
{
    private Animator animator;
    private Collider2D chestCollider;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
        chestCollider = GetComponent<Collider2D>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            gameController.AddPoints(pointsToAdd);

            CreateItemFeedbackVFX();
            audioController.PlaySoundEffect(pointItemFeedbackSFX, pointItemFeedbackSFXVolume);

            chestCollider.enabled = false;
            animator.SetTrigger("isOpen");
        }
    }
}