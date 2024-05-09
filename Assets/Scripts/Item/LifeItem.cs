using UnityEngine;

public class LifeItem : MonoBehaviour
{
    private GameController gameController;
    private AudioController audioController;

    [SerializeField] [Range(1, 100)] private int livesToAdd;

    [SerializeField] private GameObject itemFeedbackVFX;
    [SerializeField] private AudioClip lifeItemFeedbackSFX;
    [SerializeField] [Range(0, 1)] private float lifeItemFeedbackSFXVolume = 0f;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            gameController.AddLives(livesToAdd);

            CreateItemFeedbackVFX();
            audioController.PlaySoundEffect(lifeItemFeedbackSFX, lifeItemFeedbackSFXVolume);

            Destroy(gameObject);
        }
    }

    private void CreateItemFeedbackVFX()
    {
        Instantiate(itemFeedbackVFX, transform.position, transform.rotation);
    }
}