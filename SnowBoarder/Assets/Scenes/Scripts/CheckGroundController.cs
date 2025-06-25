using UnityEngine;

public class CheckGroundController : MonoBehaviour
{
    public bool IsTouchingGround { get; private set; }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            IsTouchingGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            IsTouchingGround = false;
    }
}
