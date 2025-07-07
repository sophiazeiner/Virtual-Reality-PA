using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DesinfektionsLogik : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip spruehSound;

    [Header("Rotation & Platzierung")]
    public float kippWinkelThreshold = 60f;  // z. B. mind. 60° Kippung
    public Transform abstellPosition;       // Referenz zur Zielposition
    public float abstellToleranz = 0.2f;    // Wie genau die Flasche abgestellt sein muss

    private XRGrabInteractable grabInteractable;
    private bool wurdeGekippt = false;
    private bool wurdeAbgestellt = false;

    public static bool haendeDesinfiziert = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnLosgelassen);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable fehlt!");
        }
    }

    void Update()
    {
        if (grabInteractable.isSelected && !wurdeGekippt)
        {
            float kippung = Vector3.Angle(transform.up, Vector3.up);
            if (kippung >= kippWinkelThreshold)
            {
                wurdeGekippt = true;
                Debug.Log("Flasche wurde gekippt!");

                if (audioSource != null && spruehSound != null)
                    audioSource.PlayOneShot(spruehSound);
            }
        }
    }

    void OnLosgelassen(SelectExitEventArgs args)
    {
        if (wurdeGekippt && !wurdeAbgestellt)
        {
            float distanz = Vector3.Distance(transform.position, abstellPosition.position);
            if (distanz <= abstellToleranz)
            {
                wurdeAbgestellt = true;
                haendeDesinfiziert = true;
                Debug.Log("✔ Hände erfolgreich desinfiziert!");
            }
            else
            {
                Debug.Log("Flasche nicht korrekt abgestellt.");
            }
        }
    }
}
