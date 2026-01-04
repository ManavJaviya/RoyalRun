using UnityEngine;

public class PlayerCollisionHandle : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] float adjestChangeMoveSpeedAmt = -2f;
    
    LevelGenerator levelGenerator;
    const string hitString = "Hit";


    float cooldownTimer = 0f;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }
    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }
    void OnCollisionEnter(Collision other)
    {
        if (cooldownTimer < collisionCooldown) return;

        levelGenerator.ChangeChunkMoveSpeed(adjestChangeMoveSpeedAmt);

        animator.SetTrigger(hitString);
        cooldownTimer = 0f;
    }
}
