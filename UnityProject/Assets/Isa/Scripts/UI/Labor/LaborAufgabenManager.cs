using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaborAufgabenManager : MonoBehaviour
{
    [Header("Texte in Reihenfolge")]
    public List<GameObject> textPanels = new List<GameObject>();

    [Header("Buttons")]
    public Button weiterButton;
    public Button zurückButton;

    [Header("Click Sound")]
    public AudioSource clickSound;

    [Header("Optional: Canvas Root zum Deaktivieren")]
    public GameObject canvasRoot; // z. B. das ganze UI-Objekt

    private int currentIndex = 0;

    void Start()
    {
        // Nur erster Text sichtbar, Zurück-Button aus
        for (int i = 0; i < textPanels.Count; i++)
            textPanels[i].SetActive(i == 0);

        weiterButton.onClick.AddListener(NextText);
        zurückButton.onClick.AddListener(PreviousText);

        UpdateButtonVisibility();
    }

    void NextText()
    {
        if (clickSound != null) clickSound.Play();

        // Letztes Textfeld → alles ausblenden
        if (currentIndex >= textPanels.Count - 1)
        {
            DeactivateAll();
            return;
        }

        textPanels[currentIndex].SetActive(false);
        currentIndex++;

        textPanels[currentIndex].SetActive(true);
        UpdateButtonVisibility();
    }

    void PreviousText()
    {
        if (clickSound != null) clickSound.Play();

        if (currentIndex <= 0) return;

        textPanels[currentIndex].SetActive(false);
        currentIndex--;

        textPanels[currentIndex].SetActive(true);
        UpdateButtonVisibility();
    }

    void UpdateButtonVisibility()
    {
        zurückButton.gameObject.SetActive(currentIndex > 0);
        weiterButton.gameObject.SetActive(true); // immer sichtbar – bis zum letzten Klick
    }

    void DeactivateAll()
    {
        // Textfelder ausblenden
        foreach (var panel in textPanels)
            panel.SetActive(false);

        // Buttons deaktivieren
        weiterButton.gameObject.SetActive(false);
        zurückButton.gameObject.SetActive(false);

        // Optional: gesamtes Canvas ausblenden
        if (canvasRoot != null)
            canvasRoot.SetActive(false);
    }
}
