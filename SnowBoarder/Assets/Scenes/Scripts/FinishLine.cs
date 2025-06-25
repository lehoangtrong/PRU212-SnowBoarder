using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 10f; // Delay before reloading the scene

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
        if (collision.CompareTag("Player"))
        {
            // Play all particle effects attached to this GameObject 
            foreach (var effect in GetComponentsInChildren<ParticleSystem>())
            {
                effect.Play();
            }
            // Play the win sound effect
            audioManager.PlaySFX(audioManager.winSoundClip);
            Invoke("Victory", reloadDelay); // Delay to allow player to see the finish line
        }
    }

    void Victory()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.OnPlayerWin(); // Call the method to handle player victory
    }
}
