using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaborIntro : MonoBehaviour
{
    [Header("UI Elemente")]
    public CanvasGroup canvasGroup;           // Komplettes UI-Canvas (inkl. Text, Button, Hintergrund)
    public TextMeshProUGUI introText;         // Dein Textfeld
    public Button okButton;                   // OK Button
    public AudioSource clickSound;            // Button Click Sound

    [Header("Fade Einstellungen")]
    public float fadeDuration = 1.0f;

    private void Start()
    {
        // Alles unsichtbar starten
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        okButton.onClick.AddListener(OnOkClicked);

        // Intro anzeigen
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        canvasGroup.gameObject.SetActive(true);
        float t = 0f;
        while (t < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void OnOkClicked()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float t = 0f;
        while (t < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }
}
