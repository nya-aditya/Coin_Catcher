using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text scoreText;
    public TMP_Text gameOverText;

    public int score = 0;
    public int bombCount = 0;
    public int maxBombs = 3;
    public bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;

        // Auto-find references if not assigned in Inspector
        if (scoreText == null)
        {
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
            {
                scoreText = scoreObj.GetComponent<TMP_Text>();
            }
        }

        if (gameOverText == null)
        {
            GameObject goObj = GameObject.Find("GameOverText");
            if (goObj != null)
            {
                gameOverText = goObj.GetComponent<TMP_Text>();
            }
        }

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        UpdateUI();
    }

    void Update()
    {
        if (isGameOver)
        {
            bool restartPressed = false;

            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                restartPressed = true;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                restartPressed = true;
            }

            if (restartPressed)
            {
                RestartGame();
            }
        }
    }

    public void AddScore(int amount = 1)
    {
        if (isGameOver) return;

        score += amount;
        UpdateUI();
    }

    public void AddBomb(int amount = 1)
    {
        if (isGameOver) return;

        bombCount += amount;
        UpdateUI();

        if (bombCount >= maxBombs)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}\nBombs: {bombCount}";
        }
    }

    void GameOver()
    {
        isGameOver = true;

        string gameOverString = $"GAME OVER\nScore: {score}\nBombs: {bombCount}\n\nPress R to Restart";

        if (gameOverText != null)
        {
            gameOverText.text = gameOverString;
            gameOverText.gameObject.SetActive(true);
        }
        else if (scoreText != null)
        {
            scoreText.text = gameOverString;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
