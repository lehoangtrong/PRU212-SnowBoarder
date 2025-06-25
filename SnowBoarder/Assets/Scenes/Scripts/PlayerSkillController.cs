using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillController : MonoBehaviour
{
    private Animator myAnimator;
    public GameObject boardObject; // Kéo GameObject "Board" vào đây
    public GameObject checkGround; // Kéo GameObject "CheckGround" vào đây
    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
        if (myAnimator == null)
        {
            Debug.LogError("Không tìm thấy Animator nào trong các đối tượng con!");
        }
    }

    void Update()
    {
        // Dùng wasPressedThisFrame để lệnh chỉ được gọi một lần khi nhấn phím
        // tương đương với GetKeyDown
       
        if (Keyboard.current.lKey.wasPressedThisFrame && !checkGround.GetComponent<CheckGroundController>().IsTouchingGround)
        {
            //GameManager.Instance.StartAudition();
            //Kiểm tra xem boardObject đã được gán chưa
            if (boardObject != null)
            {
                boardObject.SetActive(false); // Tắt ván trượt
            }
            Debug.Log("Skill L activated!");
            myAnimator.SetTrigger("Flip");
        }
    }

    // HÀM MỚI: Animation Event sẽ gọi hàm này
    // Hàm này phải là public để Animation có thể thấy nó
    public void ShowBoard()
    {
        // Kiểm tra xem boardObject đã được gán chưa
        if (boardObject != null)
        {
            boardObject.SetActive(true); // Bật lại ván trượt
        }
        GameManager.Instance.score += 100; // Tăng điểm khi hoàn thành skill
        GameManager.Instance.UpdateUI(); // Cập nhật UI điểm số
        Debug.Log("Flip animation finished, showing board!");
    }
}
