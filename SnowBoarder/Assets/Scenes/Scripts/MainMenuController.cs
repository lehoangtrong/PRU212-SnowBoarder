using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Kéo và thả GameObject "OptionsPanel" của bạn vào đây trong Inspector
    [Header("UI Elements")]
    public GameObject optionsPanel;

    // Biến để lưu trữ component Animator của OptionsPanel
    private Animator optionsPanelAnimator;

    // Awake được gọi trước Start, rất tốt để lấy các component
    void Awake()
    {
        // Lấy component Animator từ optionsPanel khi game bắt đầu
        if (optionsPanel != null)
        {
            optionsPanelAnimator = optionsPanel.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Chưa gán OptionsPanel cho MainMenuController!");
        }
    }

    // Hàm này sẽ được gọi khi người dùng nhấn nút "Options"
    public void OnOptionsButtonClick()
    {
        if (optionsPanelAnimator != null)
        {
            // Kích hoạt trigger "Show" trong Animator
            optionsPanelAnimator.SetTrigger("Show");
        }
    }

    // Hàm này sẽ được gọi khi người dùng nhấn nút "Quay Lại" trong bảng Options
    public void OnBackButtonClick()
    {
        if (optionsPanelAnimator != null)
        {
            // Kích hoạt trigger "Hide" trong Animator
            optionsPanelAnimator.SetTrigger("Hide");
        }
    }

    // Hàm này sẽ được gọi khi người dùng nhấn nút "Quit"
    public void OnQuitButtonClick()
    {
        Debug.Log("Đã nhấn nút Quit!");
        Application.Quit();

        // Nếu bạn đang chạy trong Unity Editor, dòng lệnh trên sẽ không hoạt động.
        // Dòng lệnh dưới đây sẽ dừng việc chạy game trong Editor.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
