using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Thêm vào để có thể tải lại Scene

public class PanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject leaderboardView; // GameObject chứa bảng xếp hạng
    [SerializeField] private GameObject nameInputView; // GameObject chỉ chứa ô nhập tên và nút Save
    private int finalScore; // Biến để lưu điểm cuối cùng

    [Header("Managers")]
    [SerializeField] public LeaderboardManager leaderboardManager;
    public void OnPlayAgainButtonClick()
    {
        // Tải lại màn chơi hiện tại
        GameManager.Instance.RestartGame();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        // Lấy điểm cuối từ GameManager
        finalScore = GameManager.Instance.score;
        // Hiển thị điểm cuối lên UI
        //if (leaderboardView != null)
        //{
        //    leaderboardView.SetActive(true);
        //}
        // Hiển thị ô nhập tên
        if (nameInputView != null)
        {
            nameInputView.SetActive(false);
        }
        // Nếu không có LeaderboardManager, thông báo lỗi
        if (leaderboardManager == null)
        {
            Debug.LogError("LeaderboardManager is not assigned in PanelController!");
        }
    }

    private void Update()
    {
        if(GameManager.Instance.isGameOver != true)
        {
            nameInputView.SetActive(true); // Ẩn ô nhập tên nếu game chưa kết thúc
        }
        else
        {
            nameInputView.SetActive(false); // Hiển thị ô nhập tên nếu game đã kết thúc
        }
    }
    // Hàm này sẽ được kết nối với nút "Home" trên UI
    public void OnHomeButtonClick()
    {
        GameManager.Instance.RestartGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnSaveScoreButtonClick()
    {
        string playerName = "Player"; // Tên mặc định

        if (nameInputField != null && !string.IsNullOrEmpty(nameInputField.text))
        {
            playerName = nameInputField.text;
        }

        // Thêm điểm mới. LeaderboardManager sẽ tự cập nhật lại view
        if (leaderboardManager != null)
        {
            leaderboardManager.AddNewScore(playerName, this.finalScore);
        }

        // Sau khi lưu, ẩn ô nhập tên đi
        if (nameInputView != null)
        {
            nameInputView.SetActive(false);
        }
    }
}