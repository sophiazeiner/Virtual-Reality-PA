using UnityEngine;
using TMPro;

public class BeweisSammler : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;    // UI-Text anzeigen
    public int gesuchteBeweise = 4;         // Gesamtzahl
    private int gefundeneBeweise = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Beweis"))
        {
            gefundeneBeweise++;

            // Optional: Beweis deaktivieren
            other.gameObject.SetActive(false);

            // Feedback aktualisieren
            feedbackText.text = $"{gefundeneBeweise} / {gesuchteBeweise} Beweise gefunden";

            // Optional: Etwas tun, wenn alle gefunden wurden
            if (gefundeneBeweise >= gesuchteBeweise)
            {
                Debug.Log("Alle Beweise gefunden!");
            }
        }
    }
}