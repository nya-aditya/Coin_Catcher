using UnityEngine;

public class Coin : MonoBehaviour
{
    public float fallSpeed = 5f;

    void Update()
    {
        // Stop falling when game is over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            return;
        }

        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // Destroy object if it falls below the screen
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(1);
            }

            Destroy(gameObject);
        }
    }
}
