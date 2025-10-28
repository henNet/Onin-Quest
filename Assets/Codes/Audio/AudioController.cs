using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] private AudioSource gunShoot;
    [SerializeField] private AudioSource targetHit;
    [SerializeField] private AudioSource death;
    [SerializeField] private AudioSource hit;

    void Awake()
    {
        instance = this;
    }

    public void PlayGunShoot()
    {
        PlayDynamicSound(gunShoot);
    }

    public void PlayTargetHit()
    {
        PlayDynamicSound(targetHit);
    }

    private void PlayDynamicSound(AudioSource audio)
    {
        GameObject soundGameObject = new GameObject("DynamicSound");

        // Add an AudioSource component to the new GameObject
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;

        // Assign the audio clip
        audioSource.clip = audio.clip;

        // Play the audio
        audioSource.Play();

        // Optionally, destroy the GameObject after the sound finishes playing
        Destroy(soundGameObject, audio.clip.length);
    }

    public void PlayDeathSound()
    {
        death.Play();
    }

    public void PlayHitSound()
    {
        hit.Play();
    }
}
