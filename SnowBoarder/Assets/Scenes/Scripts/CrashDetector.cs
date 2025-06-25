using UnityEngine;
using UnityEngine.SceneManagement;
public class CrashDetector : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 0.5f; // Delay before reloading the scene
    [SerializeField] private ParticleSystem crashEffect; // Particle effect for crashing

    AudioManager audioManager;
    void Awake()
    {
        // Find the AudioManager in the scene
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // Ensure the AudioManager is not null
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene. Please add an AudioManager.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            audioManager.PlaySFX(audioManager.crashSoundClip); // Play crash sound effect
            // If the player collides with the ground, reload the scene
            crashEffect.Play(); // Play the crash effect
            Invoke("ReloadScene", reloadDelay); // Delay to allow player to see the crash
        }

        if (collision.CompareTag("Obstacles"))
        {
            audioManager.PlaySFX(audioManager.crashSoundClip); // Play crash sound effect
            // If the player collides with an obstacle, reload the scene
            crashEffect.Play(); // Play the crash effect
            Debug.Log("Player crashed into an obstacle!");
            Invoke("ReloadScene", reloadDelay); // Delay to allow player to see the crash
        }

        if (collision.CompareTag("Star"))
        {
            audioManager.PlaySFX(audioManager.starCollectSoundClip); // Play star collection sound effect
            HandleStarCollection(collision.gameObject);
        }
    }

    void ReloadScene()
    {
        GameManager.Instance.isGameOver = true; // Set game over state in GameManager
        GameManager.Instance.HandleGameOver(); // Call the game over handler in GameManager
    }

    void HandleStarCollection(GameObject star)
    {
        // Increment the score in GameManager
        GameManager.Instance.score += 10; // Adjust the score increment as needed
        Destroy(star);
    }
}
