using UnityEngine;

public class MedicationScanner : MonoBehaviour
{
    public GameObject scanUI;            // World-Space UI Panel
    public AudioSource scanSound;        // Soundeffekt
    private bool scanned = false;

    void OnTriggerEnter(Collider other)
    {
        if (scanned) return;

        if (other.CompareTag("Medikament"))  // tagge dein Medikament entsprechend
        {
            scanned = true;

            if (scanUI != null) scanUI.SetActive(true);
            if (scanSound != null) scanSound.Play();

            // Option: Medikament automatisch "einrasten"
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
        }
    }
}