using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandschuheAnziehen : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    [Header("Audio")]
    public AudioSource audioSource;       // AudioSource auf dem Objekt oder extern
    public AudioClip angezogenSound;      // AudioClip, der abgespielt werden soll

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable fehlt am Objekt HANDSCHUHE!");
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Sound abspielen
        if (audioSource != null && angezogenSound != null)
        {
            audioSource.PlayOneShot(angezogenSound);
        }
        else
        {
            Debug.LogWarning("AudioSource oder Soundclip fehlt bei HANDSCHUHE!");
        }

        // Objekt deaktivieren (nach kurzem Delay, damit Sound noch hörbar ist)
        StartCoroutine(DeactivateAfterSound());
    }

    private System.Collections.IEnumerator DeactivateAfterSound()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        Debug.Log("Handschuhe angezogen → Objekt deaktiviert.");
    }
}

