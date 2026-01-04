using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text scoreText;

    int score = 0;

    public void IncreseScore(int amount)
    {
        if (gameManager.GameOverReturn) return;

        score += amount;
        if (score < 0f) score = 0;
        scoreText.text = score.ToString();
    }
}
