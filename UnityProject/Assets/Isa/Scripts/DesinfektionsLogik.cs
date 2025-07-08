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
    public float kippWinkelThreshold = 90f;
    public float aufrechtWinkelLimit = 45f; // Für Tisch-Snapping

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

                Debug.Log("Flasche gekippt – Hände gelten als desinfiziert.");
            }
        }

        // Snapping erst NACH dem Drehen UND wenn sie wieder aufrecht UND nah am Tisch ist
        if (wurdeGedreht && !wurdeAbgestellt)
        {
            float distanz = Vector3.Distance(transform.position, abstellPosition.position);
            float winkelZurVertikalen = Vector3.Angle(transform.up, Vector3.up);

            if (distanz <= abstellToleranz && winkelZurVertikalen <= aufrechtWinkelLimit)
            {
                wurdeAbgestellt = true;

                // Loslassen aus der Hand
                if (grabInteractable.isSelected && grabInteractable.firstInteractorSelecting != null)
                {
                    grabInteractable.interactionManager.SelectExit(grabInteractable.firstInteractorSelecting, grabInteractable);
                }

                // Snappen
                transform.position = abstellPosition.position;
                transform.rotation = abstellPosition.rotation;

                // Optional fixieren
                grabInteractable.enabled = false;
                rb.isKinematic = true;

                // Ton abspielen
                if (abgestelltSound != null && audioSource != null)
                    audioSource.PlayOneShot(abgestelltSound);

                Debug.Log("Flasche korrekt abgestellt.");

                // 🟢 Alle restlichen Objekte interaktiv machen
                VRIntroManager manager = FindObjectOfType<VRIntroManager>();
                if (manager != null)
                {
                    manager.AktiviereInteraktiveObjekte();
                    Debug.Log("Interaktive Objekte aktiviert.");
                }
                else
                {
                    Debug.LogWarning("VRIntroManager nicht gefunden.");
                }
            }
        }
    }
}
