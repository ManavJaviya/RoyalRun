using UnityEngine;

public class Apple : PickUps
{
    [SerializeField] float powerUpSpeed = 3f;
    [SerializeField] AudioClip applePickupAudio;
    LevelGenerator levelGenerator;
     public void Init(LevelGenerator levelGenerator)
     {
        this.levelGenerator = levelGenerator;
     }
    protected override void OnPickUp()
    {
        levelGenerator.ChangeChunkMoveSpeed(powerUpSpeed);
    }
    protected override AudioClip GetPickUpSound()
    {
       return applePickupAudio;
    } 
}
