using UnityEngine;

/// <summary>
/// Play a simple sound using PlayOneShot with volume and pitch,
/// but only once at a time – avoids overlapping.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayQuick_Angepasst : MonoBehaviour
{
    [Tooltip("The sound that is played")]
    public AudioClip sound = null;

    [Tooltip("The volume of the sound")]
    public float volume = 1.0f;

    [Tooltip("The range of pitch the sound is played at (-pitch, pitch)")]
    [Range(0, 1)] public float randomPitchVariance = 0.0f;

    private AudioSource audioSource = null;
    private float defaultPitch = 1.0f;
    private bool isPlaying = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Play()
    {
        if (isPlaying || sound == null) return;

        StartCoroutine(PlaySoundOnce());
    }

    private System.Collections.IEnumerator PlaySoundOnce()
    {
        isPlaying = true;

        float randomVariance = Random.Range(-randomPitchVariance, randomPitchVariance);
        randomVariance += defaultPitch;
        audioSource.pitch = randomVariance;

        audioSource.PlayOneShot(sound, volume);

        yield return new WaitForSeconds(sound.length / audioSource.pitch); // Warte, bis Sound fertig
        audioSource.pitch = defaultPitch;
        isPlaying = false;
    }

    private void OnValidate()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }
}
