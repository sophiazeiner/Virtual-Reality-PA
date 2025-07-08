using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRIntroManager : MonoBehaviour
{
    [Header("Texte mit CanvasGroup")]
    public List<CanvasGroup> introTexte = new List<CanvasGroup>();
    public CanvasGroup uiHintergrund;

    [Header("Buttons")]
    public Button weiterButton;
    public Button loslegenButton;

    [Header("Click Sound")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    private int aktuellerIndex = 0;
    private bool isTransitioning = false;

    void Start()
    {
        // Alle Texte ausblenden
        foreach (var text in introTexte)
            SetCanvasGroupVisible(text, false);

        SetCanvasGroupVisible(uiHintergrund, true);
        weiterButton.gameObject.SetActive(true);
        loslegenButton.gameObject.SetActive(false);

        if (introTexte.Count > 0)
            StartCoroutine(FadeCanvasGroup(introTexte[0], true));

        weiterButton.onClick.AddListener(() =>
        {
            SpieleClickSound();
            ZeigeNaechstenText();
        });

        loslegenButton.onClick.AddListener(() =>
        {
            SpieleClickSound();
            IntroBeenden();
        });
    }

    void ZeigeNaechstenText()
    {
        if (isTransitioning || aktuellerIndex >= introTexte.Count - 1)
            return;

        StartCoroutine(SwitchTextSmooth(introTexte[aktuellerIndex], introTexte[aktuellerIndex + 1]));
        aktuellerIndex++;

        if (aktuellerIndex == introTexte.Count - 1)
        {
            weiterButton.gameObject.SetActive(false);
            loslegenButton.gameObject.SetActive(true);
        }
    }

    void IntroBeenden()
    {
        // Alles ausblenden
        foreach (var text in introTexte)
            SetCanvasGroupVisible(text, false);

        SetCanvasGroupVisible(uiHintergrund, false);
        weiterButton.gameObject.SetActive(false);
        loslegenButton.gameObject.SetActive(false);
    }

    void SpieleClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    IEnumerator SwitchTextSmooth(CanvasGroup current, CanvasGroup next)
    {
        isTransitioning = true;
        yield return StartCoroutine(FadeCanvasGroup(current, false));
        yield return StartCoroutine(FadeCanvasGroup(next, true));
        isTransitioning = false;
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, bool fadeIn, float duration = 0.5f)
    {
        float startAlpha = fadeIn ? 0 : 1;
        float endAlpha = fadeIn ? 1 : 0;
        float time = 0f;

        cg.interactable = fadeIn;
        cg.blocksRaycasts = fadeIn;

        while (time < duration)
        {
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        cg.alpha = endAlpha;
    }

    void SetCanvasGroupVisible(CanvasGroup cg, bool sichtbar)
    {
        if (cg != null)
        {
            cg.alpha = sichtbar ? 1 : 0;
            cg.interactable = sichtbar;
            cg.blocksRaycasts = sichtbar;
        }
    }
}
