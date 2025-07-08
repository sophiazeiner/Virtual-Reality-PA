using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIAufgabeTuer : MonoBehaviour
{
    [Header("UI-Elemente")]
    public CanvasGroup canvasGroup;             // Für Ein-/Ausblenden
    public Button ausblendenButton;             // Der Button zum Ausblenden

    [Header("Audio Clips")]
    public AudioClip klickSound;
    public AudioClip anzeigenSound;

    [Header("Fade-Einstellungen")]
    public float fadeDauer = 1f;

    private void Start()
    {
        // Sound beim Anzeigen des Canvases (z. B. beim Start)
        if (anzeigenSound != null)
            AudioSource.PlayClipAtPoint(anzeigenSound, Camera.main.transform.position);

        // Button-Klick registrieren
        if (ausblendenButton != null)
            ausblendenButton.onClick.AddListener(OnButtonGeklickt);
    }

    private void OnButtonGeklickt()
    {
        // Klicksound abspielen
        if (klickSound != null)
            AudioSource.PlayClipAtPoint(klickSound, Camera.main.transform.position);

        // Canvas langsam ausblenden
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = canvasGroup.alpha;
        float t = 0f;

        while (t < fadeDauer)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t / fadeDauer);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        gameObject.SetActive(false);
    }
}
