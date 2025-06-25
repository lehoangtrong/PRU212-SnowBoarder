using UnityEngine;

public class ForceSpeedController : MonoBehaviour
{
    private GameObject player;
    private SurfaceEffector2D surfaceEffector2D;
    private float lastPlayerPositionY;
    [SerializeField]
    private GameObject groundCheck;
    void Start()
    {
        surfaceEffector2D = GetComponent<SurfaceEffector2D>();
        if (surfaceEffector2D == null)
        {
            Debug.LogError("SurfaceEffector2D component not found on this GameObject.");
        }

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }

        // Gán độ cao ban đầu
        lastPlayerPositionY = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPosition();
    }

    void CheckPosition()
    {
        if (player != null && surfaceEffector2D != null)
        {
            if (player.transform.position.y > lastPlayerPositionY)
            {
                // Người chơi đang leo lên dốc, giảm tốc độ
                surfaceEffector2D.speed = Mathf.Max(4f, surfaceEffector2D.speed - 0.3f);
            }
            else if (player.transform.position.y < lastPlayerPositionY && groundCheck.GetComponent<CheckGroundController>().IsTouchingGround)
            {
                surfaceEffector2D.speed = Mathf.Min(surfaceEffector2D.speed + 0.2f, 50f); // tăng tốc độ khi xuống dốc
            }
            lastPlayerPositionY = player.transform.position.y;
        }
    }
}
