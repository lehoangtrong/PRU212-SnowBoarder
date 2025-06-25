using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public static GameManager Instance; // Singleton instance
    public int score = 0; // Player's score
    public float distanceTraveled = 0f; // Distance traveled by the player
    public bool isGameOver = false; // Flag to check if the game is over
    [SerializeField] private TextMeshProUGUI scoreText; // UI Text to display score
    [SerializeField] private TextMeshProUGUI distanceText; // UI Text to display distance

    [Header("Animator")]
    [SerializeField]
    private Animator animator;

    [Header("Surface Effector")]
    [SerializeField] private SurfaceEffector2D surfaceEffector2D; // Reference to the Surface Effector

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Initialize UI
        UpdateUI();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            // Update distance traveled based on player's position
            distanceTraveled += Time.deltaTime * 5f; // Adjust speed multiplier as needed
            UpdateUI();
        }

        // Check for game over condition (e.g., player has crashed)
        if (isGameOver)
        {
            Debug.Log("Game Over! Final Score: " + score);
            // Handle game over state
            HandleGameOver();
        }

        animator.SetBool("IsGameOver", isGameOver); // Update animator state
    }

    private void UpdateUI()
    {
        // Update the score and distance text
        scoreText.text = "Score: " + score;
        distanceText.text = "Distance: " + Mathf.FloorToInt(distanceTraveled) + "m";
    }

    private void HandleGameOver()
    {
        // Stop the surface effector when the game is over
        if (surfaceEffector2D != null)
        {
            surfaceEffector2D.speed = 0f; // Stop movement
        }

        // Additional game over logic can be added here (e.g., show game over screen)
        Debug.Log("Game Over! Final Score: " + score);

        // Navigate to the Game Over scene or show a game over UI
        // SceneManager.LoadScene("GameOverScene"); // Uncomment and set your game over scene
    }
}
