using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float minX = -7.5f;
    public float maxX = 7.5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }
    }

    void Update()
    {
        // Stop movement if the game is over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            return;
        }

        float move = 0f;

        // Input System support
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                move = -1f;
            }
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                move = 1f;
            }
        }

        // Fallback to legacy input
        if (move == 0f)
        {
            move = Input.GetAxisRaw("Horizontal");
        }

        transform.Translate(Vector2.right * move * speed * Time.deltaTime);

        // Clamp player inside screen boundaries
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
