using UnityEngine;

public class PlayHelpMe : MonoBehaviour
{
    public AudioClip helpMe;
    public float volume = 0.5f;
    [Range(0.0f, 1.0f)]
    public float chance = 0.1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<RandomSittingGuest>() != null)
        {
            if (Random.Range(0.0f, 1.0f) < chance)
            {
                AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
                audioSource.PlayOneShot(helpMe, volume);
            }
        }
    }
}
