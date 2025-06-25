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

    void SetCameraZoom()
    {
        if (cinemachineCamera == null) return;

        // Check if player is grounded before adjusting camera zoom
        bool isGrounded = IsPlayerGrounded();
        if (!isGrounded) return;

        // Automatically adjust camera zoom based on player speed
        float speed = rb2d.linearVelocity.magnitude;
        float targetZoomLevel = Mathf.Clamp(speed / 10f, 1f, 3f); // Target zoom level based on speed
        float targetOrthographicSize = targetZoomLevel * 5f;

        // Smooth transition to target zoom
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(
            cinemachineCamera.Lens.OrthographicSize,
            targetOrthographicSize,
            zoomSmoothness * Time.deltaTime
        );
    }

    bool IsPlayerGrounded()
    {
        return groundCheck.GetComponent<CheckGroundController>().IsTouchingGround;
    }
}
