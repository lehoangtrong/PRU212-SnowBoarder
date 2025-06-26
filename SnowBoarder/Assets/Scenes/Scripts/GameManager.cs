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
    [SerializeField] private TextMeshProUGUI EndGameText; // UI Text to display end game message
    [SerializeField] private TextMeshProUGUI YourScore;
    [SerializeField] private TextMeshProUGUI YourDistance; // UI Text to display player's distance at the end
    [SerializeField] private GameObject player;

    [Header("Animator")]
    [SerializeField]
    private Animator animator;

    [Header("Surface Effector")]
    [SerializeField] private SurfaceEffector2D surfaceEffector2D; // Reference to the Surface Effector

    [Header("Leader Board Manager")]
    public LeaderboardManager leaderboardManager;
    [SerializeField]
    public Animator EndGameAnimator;

    [SerializeField] private GameObject EndGamePanel;
    [SerializeField]
    private GameObject MaskPanel;

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
        else
        {
            if (surfaceEffector2D != null)
            {
                surfaceEffector2D.speed = 0f; // Stop movement
            }
        }

        animator.SetBool("IsGameOver", isGameOver); // Update animator state
    }

    public void OnPlayerWin()
    {
        HandleGameOver(); // Call game over logic
        //string playerName = "Player"; // Bạn có thể lấy tên từ một ô input
        //int finalScore = this.score; // Lấy điểm cuối cùng
        //Debug.Log("Player " + playerName + " won with score: " + finalScore);

        //// Gọi hàm để thêm điểm mới
        //if (leaderboardManager != null)
        //{
        //    leaderboardManager.AddNewScore(playerName, finalScore);
        //}
    }

    public void UpdateUI()
    {
        // Update the score and distance text
        scoreText.text = "Score: " + score;
        distanceText.text = "Distance: " + Mathf.FloorToInt(distanceTraveled) + "m";
    }

    public void HandleGameOver()
    {
        EndGamePanel.SetActive(true); // Show the end game panel
        MaskPanel.SetActive(true); // Show the mask panel
        // Additional game over logic can be added here (e.g., show game over screen)
        Debug.Log("Game Over! Final Score: " + score);

        YourScore.text = "Score: " + score; // Set the player's score in the end game panel
        YourDistance.text = "Distance: " + Mathf.FloorToInt(distanceTraveled) + "m"; // Set the player's distance in the end game panel
        // Game Over
        if (isGameOver)
        {
            EndGameText.text = "Game Over"; // Set the end game text to "Game Over"
            EndGameAnimator.SetTrigger("Show"); // Trigger the end game panel animation
            // thay text cua panel thanh "Game Over"
        }
        else
        {
            // victory
            EndGameText.text = "You Win!"; // Set the end game text to "You Win"
            EndGameAnimator.SetTrigger("Show"); // Trigger the end game panel animation
        }
    }

    public void RestartGame()
    {
        // Reset game state
        isGameOver = false;
        score = 0;
        distanceTraveled = 0f;
        UpdateUI();
        EndGamePanel.SetActive(false);
        MaskPanel.SetActive(false);

        // Reset player position if needed
        player.transform.position = new Vector3(-36.41f, 11.79f, 0); // Reset player position
        player.transform.rotation = Quaternion.identity; // Reset player rotation

        // Reset animator state
        animator.SetBool("IsGameOver", false);
        // Optionally, reset player position or other game elements here
        Debug.Log("Game restarted!");
    }
}


// GameManager.cs (Nâng cấp)
//using TMPro;
//using UnityEngine;
//using Unity.Cinemachine;
//using TMPro;
//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    [Header("State")]
//    public bool isGameOver = false;
//    public bool isAuditioning = false; // Trạng thái mới: đang thực hiện skill

//    [Header("Scoring & Distance")]
//    public int score = 0;
//    public float distanceTraveled = 0f;
//    [SerializeField] private TextMeshProUGUI scoreText;
//    [SerializeField] private TextMeshProUGUI distanceText;

//    [Header("Audition Settings")]
//    public GameObject auditionUIPrefab; // Kéo Prefab UI Audition vào đây
//    public CinemachineVirtualCamera virtualCamera; // Kéo Virtual Camera vào đây
//    public float normalCamSize = 10f; // Kích thước camera bình thường
//    public float zoomedCamSize = 5f; // Kích thước camera khi zoom
//    public float slowMotionScale = 0.3f; // Tốc độ game khi slow-mo
//    public int perfectScore = 100;
//    public int goodScore = 50;

//    // Các tham chiếu cũ
//    [SerializeField] private Animator animator;
//    [SerializeField] private SurfaceEffector2D surfaceEffector2D;

//    private void Awake()
//    {
//        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
//        else { Destroy(gameObject); }
//    }

//    private void Update()
//    {
//        // Chỉ cập nhật khoảng cách khi game đang chạy bình thường
//        if (!isGameOver && !isAuditioning)
//        {
//            distanceTraveled += Time.deltaTime * 5f;
//            UpdateUI();
//        }

//        // Các logic game over cũ
//        // ...
//    }

//    // --- HÀM MỚI ---
//    // Được gọi bởi PlayerSkillController
//    public void StartAudition()
//    {
//        if (isAuditioning || isGameOver) return; // Không cho thực hiện skill khi đang làm hoặc đã thua

//        isAuditioning = true;
//        Time.timeScale = slowMotionScale; // Làm chậm thời gian
//        virtualCamera.m_Lens.OrthographicSize = zoomedCamSize; // Zoom camera

//        // Tạo UI Audition trên màn hình
//        Instantiate(auditionUIPrefab, FindObjectOfType<Canvas>().transform);
//    }

//    // --- HÀM MỚI ---
//    // Được gọi bởi AuditionUIController khi thử thách kết thúc
//    public void EndAudition(AuditionResult result)
//    {
//        isAuditioning = false;
//        Time.timeScale = 1f; // Trả lại tốc độ bình thường
//        virtualCamera.m_Lens.OrthographicSize = normalCamSize; // Zoom camera ra

//        // Cộng điểm dựa trên kết quả
//        switch (result)
//        {
//            case AuditionResult.Perfect:
//                score += perfectScore;
//                Debug.Log("PERFECT! +" + perfectScore);
//                // Bạn có thể gọi trigger animation lộn nhào ở đây nếu muốn
//                // FindObjectOfType<PlayerSkillController>().PerformFlipAnimation();
//                break;
//            case AuditionResult.Good:
//                score += goodScore;
//                Debug.Log("GOOD! +" + goodScore);
//                // FindObjectOfType<PlayerSkillController>().PerformFlipAnimation();
//                break;
//            case AuditionResult.Fail:
//                Debug.Log("FAIL!");
//                // Có thể thêm animation thất bại ở đây
//                break;
//        }

//        UpdateUI();
//    }

//    // Các hàm cũ
//    private void UpdateUI() { /* ... */ }
//    private void HandleGameOver() { /* ... */ }
//}

//// Enum để định nghĩa kết quả Audition
//public enum AuditionResult
//{
//    Perfect,
//    Good,
//    Fail
//}

// --------------------------------------------------------------------

// PlayerSkillController.cs (Đơn giản hóa)
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerSkillController : MonoBehaviour
//{
//    void Update()
//    {
//        // Nhấn L để yêu cầu GameManager bắt đầu Audition
//        if (Keyboard.current.lKey.wasPressedThisFrame)
//        {
//            GameManager.Instance.StartAudition();
//        }
//    }
//    // (Tùy chọn) Hàm để GameManager gọi để chạy animation
//    // public void PerformFlipAnimation() {
//    //      GetComponentInChildren<Animator>().SetTrigger("Flip");
//    // }
//}

// --------------------------------------------------------------------

// AuditionUIController.cs (SCRIPT MỚI - Gắn vào Prefab UI)
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.InputSystem;

//public class AuditionUIController : MonoBehaviour
//{
//    public Image shrinkingCircle; // Vòng tròn sẽ thu nhỏ

//    [Header("Timing Settings")]
//    public float shrinkDuration = 1.5f; // Thời gian để vòng tròn thu nhỏ hoàn toàn
//    [Range(0, 1)] public float perfectWindow = 0.9f; // Vùng "Perfect" (ví dụ từ 90% đến 100%)
//    [Range(0, 1)] public float goodWindow = 0.7f; // Vùng "Good" (ví dụ từ 70% đến 90%)

//    private float timer = 0f;
//    private Vector3 initialScale;

//    void Start()
//    {
//        initialScale = shrinkingCircle.transform.localScale;
//    }

//    void Update()
//    {
//        // Dùng unscaledDeltaTime để UI chạy mượt mà không bị ảnh hưởng bởi slow-mo
//        timer += Time.unscaledDeltaTime;

//        // Thu nhỏ vòng tròn
//        float scale = 1f - (timer / shrinkDuration);
//        shrinkingCircle.transform.localScale = initialScale * Mathf.Max(0, scale);

//        // Kiểm tra người chơi nhấn phím
//        if (Keyboard.current.spaceKey.wasPressedThisFrame)
//        {
//            CheckTiming();
//        }

//        // Nếu hết giờ mà không nhấn, tính là Fail
//        if (timer >= shrinkDuration)
//        {
//            GameManager.Instance.EndAudition(AuditionResult.Fail);
//            Destroy(gameObject);
//        }
//    }

//    void CheckTiming()
//    {
//        float timingPoint = timer / shrinkDuration;
//        AuditionResult result;

//        if (timingPoint >= perfectWindow)
//        {
//            result = AuditionResult.Perfect;
//        }
//        else if (timingPoint >= goodWindow)
//        {
//            result = AuditionResult.Good;
//        }
//        else
//        {
//            result = AuditionResult.Fail;
//        }

//        GameManager.Instance.EndAudition(result);
//        Destroy(gameObject); // Tự hủy sau khi hoàn thành
//    }
//}

