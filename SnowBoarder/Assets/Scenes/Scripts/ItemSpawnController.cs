using UnityEngine;
using UnityEngine.U2D;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawnController : MonoBehaviour
{
    [Header("Cấu hình chính")]
    public SpriteShapeController spriteShapeController;
    public GameObject starPrefab;
    public LayerMask groundLayer;

    [Header("Thông số spawn")]
    public int minStarsPerSegment = 5;
    public int maxStarsPerSegment = 9;
    public float yOffset = 2f;
    public float raycastHeight = 3f;
    public float raycastDistance = 6f;

    [Header("Khoảng cách spawn")]
    public float spawnRangeFromCamera = 30f;
    public float spawnCheckDelay = 0.2f;

    private Transform cam;
    private Spline spline;
    private HashSet<Vector2Int> spawnedSegments = new HashSet<Vector2Int>();

    void Start()
    {
        if (spriteShapeController == null || starPrefab == null)
        {
            Debug.LogError("Thiếu SpriteShapeController hoặc starPrefab!");
            return;
        }

        cam = Camera.main.transform;
        spline = spriteShapeController.spline;

        StartCoroutine(CheckAndSpawnStars());
    }

    IEnumerator CheckAndSpawnStars()
    {
        while (true)
        {
            int pointCount = spline.GetPointCount();

            for (int i = 0; i < pointCount - 1; i++)
            {
                Vector3 localStart = spline.GetPosition(i);
                Vector3 localEnd = spline.GetPosition(i + 1);

                Vector3 worldStart = spriteShapeController.transform.TransformPoint(localStart);
                Vector3 worldEnd = spriteShapeController.transform.TransformPoint(localEnd);
                Vector3 segmentCenter = (worldStart + worldEnd) / 2f;

                if (Vector3.Distance(cam.position, segmentCenter) <= spawnRangeFromCamera)
                {
                    Vector2Int segmentKey = new Vector2Int(i, i + 1);
                    if (!spawnedSegments.Contains(segmentKey))
                    {
                        SpawnStarsOnSegment(worldStart, worldEnd);
                        spawnedSegments.Add(segmentKey);
                    }
                }
            }

            yield return new WaitForSeconds(spawnCheckDelay);
        }
    }

    void SpawnStarsOnSegment(Vector3 start, Vector3 end)
    {
        int starCount = Random.Range(minStarsPerSegment, maxStarsPerSegment + 1);

        for (int j = 0; j < starCount; j++)
        {
            float t = Random.Range(0f, 1f);
            Vector3 lerpPos = Vector3.Lerp(start, end, t);
            Vector3 rayOrigin = lerpPos + Vector3.up * raycastHeight;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, raycastDistance, groundLayer);
            Debug.DrawRay(rayOrigin, Vector2.down * raycastDistance, Color.yellow, 2f);

            if (hit.collider != null)
            {
                Vector3 spawnPos = hit.point + Vector2.up * yOffset;

                GameObject star = Instantiate(starPrefab, spawnPos, Quaternion.identity);
                star.transform.parent = this.transform;
            }
        }
    }
}
