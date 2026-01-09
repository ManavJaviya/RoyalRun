using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    CinemachineImpulseSource cinemachineImpulseSource;
    [SerializeField] float shakeModifer = 10f;
    [SerializeField] AudioSource bounceAudioSoursce;
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] float collisionCooldown = 1f;
    float collisionTimer = 0;
    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();    
    }
    void Update()
    {
        collisionTimer += Time.deltaTime;
    }
    void OnCollisionEnter(Collision other)
    {
        if (collisionTimer < collisionCooldown) return;

        FireImplse();
        CollisionFX(other);

        collisionTimer = 0f;
    }

    void FireImplse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifer;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);

        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }
    void CollisionFX(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        bounceAudioSoursce.Play();
    }
}
