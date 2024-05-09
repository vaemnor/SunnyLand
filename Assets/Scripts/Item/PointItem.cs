using UnityEngine;

public class PointItem : MonoBehaviour
{
    protected GameController gameController;
    protected AudioController audioController;

    [SerializeField] [Range(1, 100)] protected int pointsToAdd;

    [SerializeField] protected GameObject itemFeedbackVFX;
    [SerializeField] protected AudioClip pointItemFeedbackSFX;
    [SerializeField] [Range(0, 1)] protected float pointItemFeedbackSFXVolume = 0f;

    protected virtual void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            gameController.AddPoints(pointsToAdd);

            CreateItemFeedbackVFX();
            audioController.PlaySoundEffect(pointItemFeedbackSFX, pointItemFeedbackSFXVolume);

            Destroy(gameObject);
        }
    }

    protected void CreateItemFeedbackVFX()
    {
        Instantiate(itemFeedbackVFX, transform.position, transform.rotation);
    }
}