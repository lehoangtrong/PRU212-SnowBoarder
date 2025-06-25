using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusicClip;
    public AudioClip crashSoundClip;
    public AudioClip starCollectSoundClip;
    public AudioClip winSoundClip;

    private void Start()
    {
        // Play background music on start
        if (backgroundMusicClip != null)
        {
            backgroundMusic.clip = backgroundMusicClip;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
        else
        {
            Debug.LogWarning("Background music clip is not assigned.");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
