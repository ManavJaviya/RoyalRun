using UnityEngine;

public class Coin : PickUps
{
    [SerializeField] int scoreAmount = 100;
    [SerializeField] AudioClip coinPickUpAudio;

    ScoreManager scoreManager;

    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }
    protected override void OnPickUp()
    {
        scoreManager.IncreseScore(scoreAmount);
    }

    protected override AudioClip GetPickUpSound()
    {
        return coinPickUpAudio;
    }
}
