using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DesinfektionsLogik : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip spruehSound;
    public AudioClip abgestelltSound;

    [Header("Abstellpunkt")]
    public Transform abstellPosition;
    public float abstellToleranz = 0.25f;

    [Header("Kippwinkel-Erkennung")]
    public float kippWinkelThreshold = 90f; // Ab wann gilt: gekippt
    public float aufrechtWinkelLimit = 45f; // Für "steht wieder gerade"

    [Header("Aufgaben-Manager")]
    public AufgabenUIManager aufgabenManager; // Dein UI-Manager, der die nächste Aufgabe startet

    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    private bool wurdeGedreht = false;
    private bool wurdeAbgestellt = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if (!wurdeGedreht)
        {
            float kippwinkel = Vector3.Angle(transform.up, Vector3.up);
            if (kippwinkel > kippWinkelThreshold)
            {
                wurdeGedreht = true;

                if (spruehSound != null && audioSource != null)
                    audioSource.PlayOneShot(spruehSound);

                Debug.Log("Flasche wurde gekippt.");
            }
        }

        if (wurdeGedreht && !wurdeAbgestellt)
        {
            float distanz = Vector3.Distance(transform.position, abstellPosition.position);
            float winkelZurVertikalen = Vector3.Angle(transform.up, Vector3.up);

            if (distanz <= abstellToleranz && winkelZurVertikalen <= aufrechtWinkelLimit)
            {
                wurdeAbgestellt = true;

                // Flasche loslassen (falls gehalten)
                if (grabInteractable.isSelected && grabInteractable.firstInteractorSelecting != null)
                {
                    grabInteractable.interactionManager.SelectExit(grabInteractable.firstInteractorSelecting, grabInteractable);
                }

                // Position & Rotation exakt snappen
                transform.position = abstellPosition.position;
                transform.rotation = abstellPosition.rotation;

                // Flasche fixieren
                grabInteractable.enabled = false;
                rb.isKinematic = true;

                // Sound abspielen
                if (abgestelltSound != null && audioSource != null)
                    audioSource.PlayOneShot(abgestelltSound);

                Debug.Log("Flasche korrekt abgestellt.");

                // 🟢 Aufgaben-UI starten
                if (aufgabenManager != null)
                {
                    aufgabenManager.StarteNaechsteAufgabe(); // Diese Methode musst du ggf. noch in deinem Aufgabenmanager definieren
                    Debug.Log("Nächste Aufgabe gestartet.");
                }
                else
                {
                    Debug.LogWarning("AufgabenUIManager nicht zugewiesen!");
                }
            }
        }
    }
}
