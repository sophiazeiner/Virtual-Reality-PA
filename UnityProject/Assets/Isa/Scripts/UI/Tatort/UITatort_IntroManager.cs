using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITatort_IntroManager : MonoBehaviour
{
    [Header("Intro Elemente")]
    public GameObject canvasRoot;
    public GameObject intro01;
    public GameObject intro02;

    [Header("Buttons")]
    public Button weiterButton;
    public Button okButton;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    [Header("Fade Einstellungen")]
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    private void Start()
    {
        // Initial Setup
        intro01.SetActive(true);
        weiterButton.gameObject.SetActive(true);

        intro02.SetActive(false);
        okButton.gameObject.SetActive(false);

        weiterButton.onClick.AddListener(ShowIntro2);
        okButton.onClick.AddListener(EndIntro);
    }

    void ShowIntro2()
    {
        PlayClickSound();

        intro01.SetActive(false);
        weiterButton.gameObject.SetActive(false);

        intro02.SetActive(true);
        okButton.gameObject.SetActive(true);
    }

    void EndIntro()
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
        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        canvasRoot.SetActive(false); // oder Destroy(canvasRoot);
    }
}
