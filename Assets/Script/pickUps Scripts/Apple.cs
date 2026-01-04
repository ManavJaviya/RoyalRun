using UnityEngine;

public class Apple : PickUps
{
    [SerializeField] float powerUpSpeed = 3f;
    //[SerializeField] AudioSource applePickupAudio;
    LevelGenerator levelGenerator;
     public void Init(LevelGenerator levelGenerator)
     {
        this.levelGenerator = levelGenerator;
     }
    protected override void OnPickUp()
    {
        levelGenerator.ChangeChunkMoveSpeed(powerUpSpeed);
    }

}
