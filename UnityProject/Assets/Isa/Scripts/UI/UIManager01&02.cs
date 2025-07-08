using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISequenzManager : MonoBehaviour
{
    [Header("Texte (GameObjects)")]
    public List<GameObject> textElemente = new List<GameObject>();

    [Header("Weiter-Button")]
    public Button weiterButton;

    [Header("CanvasGroup vom gesamten UI")]
    public CanvasGroup uiCanvasGroup;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip klickSound;

    private int aktuellerIndex = 0;
    private bool uiWirdBereitsAusgeblendet = false;

    void Start()
    {
        // Nur erstes Textelement aktivieren
        for (int i = 0; i < textElemente.Count; i++)
            textElemente[i].SetActive(i == 0);

        weiterButton.onClick.AddListener(ZeigeNaechstenText);
    }

    void ZeigeNaechstenText()
    {
        if (audioSource != null && klickSound != null)
            audioSource.PlayOneShot(klickSound);

        if (aktuellerIndex < textElemente.Count - 1)
        {
            textElemente[aktuellerIndex].SetActive(false);
            aktuellerIndex++;
            textElemente[aktuellerIndex].SetActive(true);
        }
        else
        {
            weiterButton.gameObject.SetActive(false);
        }
    }

    // Diese Methode rufst du auf, sobald die Überschuhe angezogen wurden
    public void SchuheAngezogen()
    {
        if (!uiWirdBereitsAusgeblendet)
            StartCoroutine(UIAusblenden());
    }

    IEnumerator UIAusblenden(float dauer = 1.5f)
    {
        uiWirdBereitsAusgeblendet = true;

        float zeit = 0f;
        float startAlpha = uiCanvasGroup.alpha;

        uiCanvasGroup.interactable = false;
        uiCanvasGroup.blocksRaycasts = false;

        while (zeit < dauer)
        {
            uiCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, zeit / dauer);
            zeit += Time.deltaTime;
            yield return null;
        }

        uiCanvasGroup.alpha = 0f;
        gameObject.SetActive(false); // optional komplett deaktivieren
    }
}
