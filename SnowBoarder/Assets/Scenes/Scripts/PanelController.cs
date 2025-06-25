using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Thêm vào để có thể tải lại Scene

public class PanelController : MonoBehaviour
{
    // Kéo GameObject Panel có chứa Animator vào đây
    //public Animator panelAnimator;

    // Hàm này được gọi từ một script khác (ví dụ: GameManager) khi người chơi thắng
    //public void ShowEndGamePanel()
    //{
    //    // Kiểm tra xem animator đã được gán chưa để tránh lỗi
    //    if (panelAnimator != null)
    //    {
    //        // Kích hoạt trigger "Show" để panel trượt vào
    //        panelAnimator.SetTrigger("Show");
    //    }
    //    else
    //    {
    //        Debug.LogError("Chưa gán Panel Animator cho PanelController!");
    //    }
    //}

    // Hàm này sẽ được kết nối với nút "Play Again" trên UI
    public void OnPlayAgainButtonClick()
    {
        // Tải lại màn chơi hiện tại
        GameManager.Instance.RestartGame();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Hàm này sẽ được kết nối với nút "Home" trên UI
    public void OnHomeButtonClick()
    {
        // Thay "MainMenuScene" bằng tên chính xác của Scene Main Menu của bạn
        SceneManager.LoadScene("MainMenu");
    }
}