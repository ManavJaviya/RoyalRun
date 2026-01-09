using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject restartButton;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool gameOver = false;

    public bool GameOver => gameOver;
    public bool GameOverReturn { get; private set; }

    void Start()
    {
        timeLeft = startTime;
        // Hide restart button at start
        if (restartButton != null)
            restartButton.SetActive(false);
    }

    void Update()
    {
        DecreaseTime();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }
    public void IncreaseTime(float amount)
    {
        timeLeft += amount;
    }

    void DecreaseTime()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");

        if (timeLeft <= 0f)
        {
            PlayerGameOver();
        }
    }

    void PlayerGameOver()
    {
        if (timeLeft <= 0f)
        {
            gameOver = true;
            playerController.enabled = false;
            gameOverText.SetActive(true);

            // Show restart button
            if (restartButton != null)
                restartButton.SetActive(true);

            Time.timeScale = 0.1f;
        }
    }

    // Method to restart the game
    public void RestartGame()
    {
        // Reset time scale to normal
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
