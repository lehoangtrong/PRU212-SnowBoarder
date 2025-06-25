using UnityEngine;
using UnityEngine.U2D;
using System.Collections;
using System.Collections.Generic;

public class TreeSpawnerNearCamera : MonoBehaviour
{
    [Header("Cấu hình chính")]
    public SpriteShapeController spriteShapeController;
    public GameObject treePrefab;
    public LayerMask groundLayer;

    [Header("Thông số sinh cây")]
    public int treesPerSegment = 2;
    public float minScale = 0.8f;
    public float maxScale = 1.3f;
    public float raycastHeight = 5f;
    public float raycastDistance = 10f;
    public float yOffset = 0f;

    [Header("Khoảng cách spawn")]
    public float spawnRangeFromCamera = 30f;
    public float spawnCheckDelay = 0.1f;

    private Transform cam;
    private Spline spline;
    private HashSet<Vector2Int> spawnedPairs = new HashSet<Vector2Int>(); // tránh trùng đoạn đã spawn

    void Start()
    {
        if (spriteShapeController == null || treePrefab == null)
        {
            Debug.LogError("Thiếu SpriteShapeController hoặc treePrefab!");
            return;
        }

        cam = Camera.main.transform;
        spline = spriteShapeController.spline;

        StartCoroutine(CheckAndSpawnTrees());
    }

    IEnumerator CheckAndSpawnTrees()
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
                    Vector2Int pairKey = new Vector2Int(i, i + 1);
                    if (!spawnedPairs.Contains(pairKey))
                    {
                        SpawnTreesOnSegment(worldStart, worldEnd);
                        spawnedPairs.Add(pairKey);
                    }
                }
            }

            yield return new WaitForSeconds(spawnCheckDelay);
        }
    }

    void SpawnTreesOnSegment(Vector3 start, Vector3 end)
    {
        for (int j = 0; j < treesPerSegment; j++)
        {
            float t = Random.Range(0f, 1f);
            Vector3 lerpPos = Vector3.Lerp(start, end, t);
            Vector3 rayOrigin = lerpPos + Vector3.up * raycastHeight;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, raycastDistance, groundLayer);
            Debug.DrawRay(rayOrigin, Vector2.down * raycastDistance, Color.green, 2f);

            if (hit.collider != null)
            {
                Vector3 spawnPos = hit.point + Vector2.up * yOffset;

                GameObject tree = Instantiate(treePrefab, spawnPos, Quaternion.identity);
                float scale = Random.Range(minScale, maxScale);
                tree.transform.localScale = new Vector3(scale, scale, 1f);
                tree.transform.parent = this.transform;
            }
        }
    }
}
