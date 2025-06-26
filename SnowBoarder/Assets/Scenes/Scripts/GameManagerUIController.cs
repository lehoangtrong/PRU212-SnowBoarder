using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Thêm vào để xử lý các nút bấm

public class GameManagerUIController : MonoBehaviour
{
    [Header("Game State")]
    public int score = 0; // Điểm của người chơi
    public float distanceTraveled = 0f; // Khoảng cách đã đi
    public bool isGameOver = false; // Cờ kiểm tra game đã kết thúc chưa
    [SerializeField] private GameObject player;

    [Header("In-Game UI")]
    [SerializeField] private TextMeshProUGUI scoreText; // Text hiển thị điểm trong game
    [SerializeField] private TextMeshProUGUI distanceText; // Text hiển thị khoảng cách trong game

    [Header("End Game UI")]
    [SerializeField] private GameObject EndGamePanel;
    [SerializeField] private GameObject MaskPanel;
    [SerializeField] private TextMeshProUGUI EndGameText; // Text hiển thị "You Win" hoặc "Game Over"
    [SerializeField] private TextMeshProUGUI YourScore;
    [SerializeField] private TextMeshProUGUI YourDistance; // Text hiển thị khoảng cách cuối cùng
    [SerializeField] public Animator EndGameAnimator;

    [Header("Scene Objects & Managers")]
    [SerializeField] private Animator animator; // Animator của Player
    [SerializeField] private SurfaceEffector2D surfaceEffector2D; // Hiệu ứng trượt của mặt đất
    [SerializeField] public LeaderboardManager leaderboardManager;

    private void Awake()
    {
        // Đảm bảo chỉ có một GameManager duy nhất trong Scene | Đăng ký toàn bộ các sự kiện cần thiết

    }
}