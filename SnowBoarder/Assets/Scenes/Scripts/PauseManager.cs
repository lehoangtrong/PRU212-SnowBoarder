using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Singleton Pattern để các script khác có thể truy cập nếu cần
    public static PauseManager Instance { get; private set; }

    [Header("UI")]
    // Kéo Panel của Menu Pause vào đây trong Inspector
    [SerializeField] private GameObject pauseMenuPanel;

    // Biến để theo dõi trạng thái của game
    private bool isPaused = false;

    private void Awake()
    {
        // Thiết lập Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Đảm bảo menu pause luôn được ẩn khi màn chơi bắt đầu
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        // Đảm bảo thời gian chạy bình thường
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        // Luôn lắng nghe nếu người chơi nhấn phím Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // Nếu đang pause thì tiếp tục
                ResumeGame();
            }
            else
            {
                // Nếu đang chơi thì pause
                PauseGame();
            }
        }
    }

    // ---- Các hàm công khai để kết nối với các nút bấm ----

    // Hàm để tạm dừng game
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Đóng băng thời gian
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    // Hàm để tiếp tục chơi game
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Cho thời gian chạy lại
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
    }

    // Hàm để quay về màn hình chính
    public void GoToMainMenu()
    {
        // Rất quan trọng: Luôn trả lại Time Scale về 1 trước khi chuyển scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Thay "MainMenu" bằng tên scene của bạn
    }
}
