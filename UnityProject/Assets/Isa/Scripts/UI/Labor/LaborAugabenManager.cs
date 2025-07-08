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

    private int currentIndex = 0;

    void Start()
    {
        // Initial Setup: nur erster Text sichtbar, Zurück-Button aus
        for (int i = 0; i < textPanels.Count; i++)
            textPanels[i].SetActive(i == 0);

        weiterButton.onClick.AddListener(NextText);
        zurückButton.onClick.AddListener(PreviousText);

        UpdateButtonVisibility();
    }

    void NextText()
    {
        if (clickSound != null) clickSound.Play();

        textPanels[currentIndex].SetActive(false);
        currentIndex++;

        textPanels[currentIndex].SetActive(true);
        UpdateButtonVisibility();
    }

    void PreviousText()
    {
        if (clickSound != null) clickSound.Play();

        textPanels[currentIndex].SetActive(false);
        currentIndex--;

        textPanels[currentIndex].SetActive(true);
        UpdateButtonVisibility();
    }

    void UpdateButtonVisibility()
    {
        // "Zurück" nur anzeigen, wenn wir nicht am ersten Text sind
        zurückButton.gameObject.SetActive(currentIndex > 0);

        // "Weiter" nur anzeigen, wenn wir nicht am letzten Text sind
        weiterButton.gameObject.SetActive(currentIndex < textPanels.Count - 1);
    }
}
