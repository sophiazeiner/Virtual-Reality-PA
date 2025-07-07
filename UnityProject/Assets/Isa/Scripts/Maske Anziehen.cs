using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskeAnziehen : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;           // Audioquelle (z. B. direkt am Objekt)
    public AudioClip angezogenSound;          // Soundclip, der beim Anziehen abgespielt wird

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FaceTrigger"))
        {
            // Ton abspielen, wenn möglich
            if (audioSource != null && angezogenSound != null)
            {
                audioSource.PlayOneShot(angezogenSound);
            }
            else
            {
                Debug.LogWarning("AudioSource oder AudioClip fehlt bei MASKE!");
            }

            // Objekt "verschwindet" (z. B. angezogen)
            gameObject.SetActive(false);
            Debug.Log("Maske angezogen – Objekt deaktiviert.");
        }
    }
}
