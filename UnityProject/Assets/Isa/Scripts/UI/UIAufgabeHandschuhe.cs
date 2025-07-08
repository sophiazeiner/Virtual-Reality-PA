using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDurchlaufMitSound : MonoBehaviour
{
    [Header("Textfelder in Reihenfolge")]
    public List<GameObject> texte;

    [Header("Button für Weiter")]
    public Button weiterButton;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip klickSound;

    private int aktuellerIndex = 0;

    void Start()
    {
        // Nur erstes Textfeld zeigen
        for (int i = 0; i < texte.Count; i++)
            texte[i].SetActive(i == 0);

        weiterButton.onClick.AddListener(WeiterKlicken);
    }

    void WeiterKlicken()
    {
        // Sound abspielen
        if (audioSource && klickSound)
            audioSource.PlayOneShot(klickSound);

        // Aktuellen Text ausblenden
        texte[aktuellerIndex].SetActive(false);
        aktuellerIndex++;

        if (aktuellerIndex < texte.Count)
        {
            texte[aktuellerIndex].SetActive(true);

            // Wenn letzter Text erreicht → Button ausblenden
            if (aktuellerIndex == texte.Count - 1)
            {
                weiterButton.gameObject.SetActive(false);

                // Aufgabenmanager triggern, wenn vorhanden
                AufgabenUIManager manager = FindObjectOfType<AufgabenUIManager>();
                if (manager != null)
                    manager.NächsteAufgabeStarten();
            }
        }
    }
}
