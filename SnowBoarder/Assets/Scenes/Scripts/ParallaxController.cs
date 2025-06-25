using System.Collections;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private Transform cam; // Main Camera
    private Vector3 camStartPos;
    private Material mat;

    [Range(0.01f, 0.1f)]
    public float parallaxSpeed = 0.05f;

    [Range(1f, 10f)]
    public float followSmoothness = 5f;

    private float distance;

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        mat = GetComponent<Renderer>().material;

        if (!mat.HasProperty("_MainTex"))
        {
            Debug.LogWarning("Material is missing _MainTex. Please assign a texture.");
        }
    }

    void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;

        // Parallax offset cho background (hiệu ứng cuộn texture)
        Vector2 offset = new Vector2(distance * parallaxSpeed, 0);
        mat.SetTextureOffset("_MainTex", offset);

        // Smooth di chuyển theo cam (nếu muốn background di chuyển cùng camera)
        Vector3 targetPos = new Vector3(cam.position.x, cam.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, followSmoothness * Time.deltaTime);
    }
}
