using UnityEngine;

public class UeberschuheAnziehen : MonoBehaviour
{
    public AudioClip anziehSound;         // Optionaler Sound
    public GameObject feedbackUI;         // Optionales UI-Feedback
    public float triggerDistanz = 1.5f;   // Abstand zur Kamera, um Anziehen zu erlauben

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FootTrigger"))
        {
            float abstandZurKamera = Vector3.Distance(Camera.main.transform.position, transform.position);

            if (abstandZurKamera <= triggerDistanz)
            {
                // Objekt "verschwindet" (also: angezogen)
                gameObject.SetActive(false);

                // Optional: Sound abspielen
                if (anziehSound != null)
                    AudioSource.PlayClipAtPoint(anziehSound, transform.position);

                // Optional: UI Feedback anzeigen
                if (feedbackUI != null)
                    feedbackUI.SetActive(true);

                Debug.Log("Überschuhe angezogen!");
            }
        }
    }
}
