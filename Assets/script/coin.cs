using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public float fallSpeed = 5f;

    private static int score = 0;
    private static int bombCount = 0;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // BOMB
            if (gameObject.CompareTag("bomb"))
            {
                bombCount++;

                Debug.Log("Bombs caught: " + bombCount);

                Destroy(gameObject);

                if (bombCount >= 3)
                {
                    Debug.Log("GAME OVER!");
                    Time.timeScale = 0f;
                }
            }
            // COIN
            else
            {
                score++;

                GameObject scoreObject = GameObject.Find("ScoreText");

                if (scoreObject != null)
                {
                    TMP_Text scoreText = scoreObject.GetComponent<TMP_Text>();

                    if (scoreText != null)
                    {
                        scoreText.text = "Score: " + score;
                    }
                }

                Destroy(gameObject);
            }
        }
    }
}