using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AuditionUIController : MonoBehaviour
{
    //// Kéo Image của vòng tròn sẽ thu nhỏ vào đây trong Inspector
    //public Image shrinkingCircle;

    //[Header("Timing Settings")]
    //public float shrinkDuration = 1.5f; // Thời gian để vòng tròn thu nhỏ hoàn toàn
    //[Range(0, 1)] public float perfectWindowStart = 0.9f; // Vùng "Perfect" bắt đầu (ví dụ: từ 90%)
    //[Range(0, 1)] public float goodWindowStart = 0.7f;  // Vùng "Good" bắt đầu (ví dụ: từ 70%)

    //private float timer = 0f;
    //private Vector3 initialScale;
    //private bool actionTaken = false; // Biến để đảm bảo chỉ nhấn được 1 lần

    //void Start()
    //{
    //    if (shrinkingCircle != null)
    //    {
    //        initialScale = shrinkingCircle.transform.localScale;
    //    }
    //}

    //void Update()
    //{
    //    // Nếu đã nhấn rồi thì không làm gì nữa
    //    if (actionTaken) return;

    //    // Dùng unscaledDeltaTime để UI chạy mượt mà không bị ảnh hưởng bởi slow-mo
    //    timer += Time.unscaledDeltaTime;

    //    // Thu nhỏ vòng tròn
    //    if (shrinkingCircle != null)
    //    {
    //        float scale = 1f - (timer / shrinkDuration);
    //        shrinkingCircle.transform.localScale = initialScale * Mathf.Max(0, scale);
    //    }

    //    // Kiểm tra người chơi nhấn phím Space
    //    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    //    {
    //        CheckTiming();
    //    }

    //    // Nếu hết giờ mà không nhấn, tính là Fail
    //    if (timer >= shrinkDuration)
    //    {
    //        GameManager.Instance.EndAudition(AuditionResult.Fail);
    //        Destroy(gameObject);
    //        actionTaken = true; // Đánh dấu đã xử lý
    //    }
    //}

    //void CheckTiming()
    //{
    //    actionTaken = true; // Đánh dấu đã nhấn nút

    //    float timingPoint = timer / shrinkDuration;
    //    AuditionResult result;

    //    if (timingPoint >= perfectWindowStart)
    //    {
    //        result = AuditionResult.Perfect;
    //    }
    //    else if (timingPoint >= goodWindowStart)
    //    {
    //        result = AuditionResult.Good;
    //    }
    //    else
    //    {
    //        result = AuditionResult.Fail;
    //    }

    //    GameManager.Instance.EndAudition(result);
    //    Destroy(gameObject); // Tự hủy sau khi hoàn thành
    //}
}
