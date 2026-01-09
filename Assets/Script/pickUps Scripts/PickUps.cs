using UnityEngine;

public abstract class PickUps : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;

    const string PlayerString = "Player";

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);    
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerString))
        {
            AudioSource playPickupAudio = other.GetComponent<AudioSource>();
            AudioClip clip = GetPickUpSound();
            if(clip != null && playPickupAudio != null)
            {
                playPickupAudio.PlayOneShot(clip);
            }
            OnPickUp();
            Destroy(gameObject);
        }
    }
    protected abstract void OnPickUp(); // subclasses return an AudioSource
    protected abstract AudioClip GetPickUpSound();
}
