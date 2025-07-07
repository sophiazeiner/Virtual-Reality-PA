using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlaySoundOnGrab : MonoBehaviour
{
    private AudioSource audioSource;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}