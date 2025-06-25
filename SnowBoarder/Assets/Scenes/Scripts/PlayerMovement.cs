using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float moveForce = 10f; // 💡 lực di chuyển ngang
    private Rigidbody2D rb2d;

    [Header("Camera Settings")]
    private CinemachineCamera cinemachineCamera;
    [SerializeField] private float zoomSmoothness = 2f; // Control how smooth the zoom is

    [Header("Surface Effector Settings")]
    [SerializeField] private SurfaceEffector2D surfaceEffector2D;
    [SerializeField] private float speedChangeRate = 5f;
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float maxSpeed = 90f;

    [SerializeField]
    [Header("Ground Check")]
    private GameObject groundCheck;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Initialize Cinemachine Camera
        cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
        if (cinemachineCamera == null)
        {
            Debug.LogError("Cinemachine Camera not found in the scene. Please add a Cinemachine Camera.");
        }
    }

    void FixedUpdate()
    {
        RotatePlayer();
    }

    void Update()
    {
        SetCameraZoom();
    }

    void RotatePlayer()
    {
        // Xoay nhân vật
        if (Keyboard.current.aKey.isPressed)
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            rb2d.AddTorque(-torqueAmount);
        }

        // Di chuyển nhân vật
        if (Keyboard.current.wKey.isPressed)
        {
            // Tăng tốc độ trượt
            surfaceEffector2D.speed = Mathf.Clamp(surfaceEffector2D.speed + speedChangeRate, minSpeed, maxSpeed);
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            // Giảm tốc độ trượt
            surfaceEffector2D.speed = Mathf.Clamp(surfaceEffector2D.speed - speedChangeRate, minSpeed, maxSpeed);
        }

        // Nhảy nhân vật
        Debug.Log("Checking for jump input...");
        // Kiểm tra nếu phím Space được nhấn và nhân vật đang đứng trên mặt đất
        // Thêm lực nhảy nếu đúng
        Debug.Log($"Space key pressed: {Keyboard.current.spaceKey.wasPressedThisFrame}, Is Player Grounded: {IsPlayerGrounded()}");
        // Nếu phím Space được nhấn và nhân vật đang đứng trên mặt đất
        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsPlayerGrounded())
        {
            rb2d.AddForce(Vector2.up * moveForce, ForceMode2D.Impulse); // Thêm lực nhảy
        }
    }

    // Giả sử các biến rb2d, cinemachineCamera, zoomSmoothness đã được khai báo ở trên

    void SetCameraZoom()
    {
        // Nếu chưa có camera thì không làm gì cả
        if (cinemachineCamera == null) return;

        // Biến để lưu kích thước camera mục tiêu
        float targetOrthographicSize;

        // Kiểm tra trạng thái của game
        if (GameManager.Instance.isGameOver)
        {
            // Khi game kết thúc, mục tiêu zoom là 5f
            targetOrthographicSize = 5f;
        }
        else
        {
            // Khi game đang chạy bình thường
            bool isGrounded = IsPlayerGrounded(); // Giả sử bạn có hàm này

            // Nếu người chơi không trên mặt đất, giữ nguyên zoom hiện tại để tránh thay đổi đột ngột
            if (!isGrounded)
            {
                // Đặt mục tiêu bằng chính giá trị hiện tại để camera đứng yên
                targetOrthographicSize = cinemachineCamera.Lens.OrthographicSize;
            }
            else
            {
                // Tự động điều chỉnh zoom dựa trên tốc độ của người chơi
                float speed = rb2d.linearVelocity.magnitude;
                float targetZoomLevel = Mathf.Clamp(speed / 10f, 1f, 3f);
                targetOrthographicSize = targetZoomLevel * 5f;
            }
        }

        // Luôn luôn thực hiện Lerp ở cuối hàm để đảm bảo mọi thay đổi đều mượt mà
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(
            cinemachineCamera.Lens.OrthographicSize, // Giá trị hiện tại
            targetOrthographicSize,                 // Giá trị mục tiêu
            zoomSmoothness * Time.deltaTime         // Tốc độ thay đổi
        );
    }

    bool IsPlayerGrounded()
    {
        return groundCheck.GetComponent<CheckGroundController>().IsTouchingGround;
    }
}
