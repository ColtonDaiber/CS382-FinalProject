using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip[] collectSounds;  // Drag your 10 clips into this in the Inspector
    private AudioSource audioSource;

    public GameObject pickupEffect; // Optional

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collectible triggered by: " + other.name);

            if (collectSounds.Length > 0 && audioSource != null)
            {
                AudioClip chosenClip = collectSounds[Random.Range(0, collectSounds.Length)];
                AudioSource.PlayClipAtPoint(chosenClip, transform.position);

            }

            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // Optional: delay destroy so sound plays
            Destroy(gameObject, 1f); // Adjust duration if clips vary
        }
    }
}
