using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIEntscheidungsraum : MonoBehaviour
{
    [Header("Intro Elemente")]
    public GameObject canvasRoot;

    [Header("Button")]
    public Button startButton;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    [Header("Fade Einstellungen")]
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    void Start()
    {
        // Alles sichtbar zu Beginn
        canvasRoot.SetActive(true);
        canvasGroup.alpha = 1f;
        startButton.gameObject.SetActive(true);

        startButton.onClick.AddListener(FadeOutIntro);
    }

    void FadeOutIntro()
    {
        PlayClickSound();
        StartCoroutine(FadeOutCanvas());
    }

    void PlayClickSound()
    {
        if (audioSource && clickSound)
            audioSource.PlayOneShot(clickSound);
    }

    System.Collections.IEnumerator FadeOutCanvas()
    {
        float time = 0f;
        float startAlpha = canvasGroup.alpha;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasRoot.SetActive(false); // Intro ausgeblendet
    }
}