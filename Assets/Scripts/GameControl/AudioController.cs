using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceBGM;
    [SerializeField] private AudioSource audioSourceSFX;

    [SerializeField] private AudioClip footstep1;
    [SerializeField] private AudioClip footstep2;
    [SerializeField] private AudioClip player_jump;
    [SerializeField] private AudioClip jump_land;
    [SerializeField] private AudioClip player_hurt;
    [SerializeField] private AudioClip player_die;

    [SerializeField] private AudioClip enemy_jump;
    [SerializeField] private AudioClip enemy_hurt1;
    [SerializeField] private AudioClip enemy_hurt2;

    [SerializeField] private AudioClip lifeitem;
    [SerializeField] private AudioClip pointitem;

    public void PlaySoundEffect(AudioClip clip, float volume)
    {
        audioSourceSFX.PlayOneShot(clip, volume);
    }
}