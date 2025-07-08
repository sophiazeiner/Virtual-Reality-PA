using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AufgabenUIManager : MonoBehaviour
{
    [Header("Aufgaben-Canvas in Reihenfolge")]
    public List<CanvasGroup> aufgabenCanvas = new List<CanvasGroup>();

    [Header("Fade Settings")]
    public float fadeDuration = 1f;
    public float delayBetweenTasks = 0.5f;

    private int currentIndex = 0;

    void Start()
    {
        // Alle Canvas ausblenden, außer der erste
        for (int i = 0; i < aufgabenCanvas.Count; i++)
        {
            SetCanvasGroupVisible(aufgabenCanvas[i], i == 0);
        }
    }

    public void NächsteAufgabeStarten()
    {
        if (currentIndex >= aufgabenCanvas.Count - 1) return;

        StartCoroutine(WechsleAufgabe(aufgabenCanvas[currentIndex], aufgabenCanvas[currentIndex + 1]));
        currentIndex++;
    }

    private IEnumerator WechsleAufgabe(CanvasGroup current, CanvasGroup next)
    {
        yield return StartCoroutine(FadeOut(current));
        yield return new WaitForSeconds(delayBetweenTasks);
        yield return StartCoroutine(FadeIn(next));
    }

    private IEnumerator FadeIn(CanvasGroup cg)
    {
        cg.gameObject.SetActive(true);
        cg.interactable = true;
        cg.blocksRaycasts = true;

        float t = 0f;
        while (t < fadeDuration)
        {
            cg.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 1f;
    }

    private IEnumerator FadeOut(CanvasGroup cg)
    {
        cg.interactable = false;
        cg.blocksRaycasts = false;

        float t = 0f;
        while (t < fadeDuration)
        {
            cg.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 0f;
        cg.gameObject.SetActive(false);
    }

    private void SetCanvasGroupVisible(CanvasGroup cg, bool visible)
    {
        cg.alpha = visible ? 1f : 0f;
        cg.interactable = visible;
        cg.blocksRaycasts = visible;
        cg.gameObject.SetActive(visible);
    }
}
