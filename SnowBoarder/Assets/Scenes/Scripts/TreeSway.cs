using UnityEngine;

public class TreeSway : MonoBehaviour
{
    public float swayAmount = 5f;       // độ lệch tối đa
    public float swaySpeed = 1f;        // tốc độ lắc
    private float randomOffset;

    void Start()
    {
        randomOffset = Random.Range(0f, 100f); // tạo độ lệch khác nhau cho mỗi cây
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed + randomOffset) * swayAmount;
        transform.localRotation = Quaternion.Euler(0, 0, sway); // nếu là 2D
    }
}
