using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OverallAnziehen : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    [Header("Audio")]
    public AudioSource audioSource;       // Referenz zur AudioSource
    public AudioClip angezogenSound;      // Clip, der beim Anziehen abgespielt werden soll

    void Start()
    {
        // Hole das XRGrabInteractable-Component vom aktuellen GameObject
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Füge die Methode OnGrabbed als Listener hinzu, wenn das Objekt gegriffen wird
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable fehlt am Objekt OVERALL!");
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Audio abspielen, falls vorhanden
        if (audioSource != null && angezogenSound != null)
        {
            audioSource.PlayOneShot(angezogenSound);
        }
        else
        {
            Debug.LogWarning("AudioSource oder AudioClip fehlt bei OVERALL!");
        }

        // Deaktiviert das tatsächlich gegriffene Objekt (auch wenn es instanziiert wurde)
        args.interactableObject.transform.gameObject.SetActive(false);

        Debug.Log("Overall angezogen → Objekt deaktiviert.");
    }
}
