using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MaskeAnziehen : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip angezogenSound;

    private XRGrabInteractable grabInteractable;
    private bool istAngezogen = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (istAngezogen) return;

        if (other.CompareTag("FaceTrigger") && grabInteractable != null && grabInteractable.isSelected)
        {
            istAngezogen = true;

            // Ton abspielen
            if (audioSource != null && angezogenSound != null)
                audioSource.PlayOneShot(angezogenSound);
            else
                Debug.LogWarning("AudioSource oder AudioClip fehlt bei MASKE!");

            // Objekt deaktivieren
            gameObject.SetActive(false);
            Debug.Log("Maske wurde zum Gesicht geführt – Objekt deaktiviert.");
        }
    }
}
