using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DesinfektionsLogik : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip spruehSound;     // Wird beim Kippen abgespielt
    public AudioClip abgestelltSound; // Wird beim Wiederaufstellen abgespielt

    [Header("Kippwinkel-Erkennung")]
    public float kippWinkelThreshold = 90f;  // Als "gekippt" erkannt
    public float aufrechtLimit = 30f;        // Als "wieder aufrecht" erkannt

    private XRGrabInteractable grabInteractable;
    private bool wurdeGedreht = false;
    private bool aktionAbgeschlossen = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if (aktionAbgeschlossen || grabInteractable == null) return;

        // Prüfen, ob mit einem Controller gehalten wird
        if (grabInteractable.isSelected &&
            grabInteractable.firstInteractorSelecting != null)
        {
            float winkel = Vector3.Angle(transform.up, Vector3.up);

            // Schritt 1: Flasche wurde stark gekippt
            if (!wurdeGedreht && winkel > kippWinkelThreshold)
            {
                wurdeGedreht = true;

                if (audioSource && spruehSound)
                    audioSource.PlayOneShot(spruehSound);

                Debug.Log("Flasche wurde gekippt.");
            }

            // Schritt 2: Flasche wieder aufrecht
            else if (wurdeGedreht && winkel <= aufrechtLimit)
            {
                aktionAbgeschlossen = true;

                if (audioSource && abgestelltSound)
                    audioSource.PlayOneShot(abgestelltSound);

                Debug.Log("Flasche wurde zurückgestellt – Aktion abgeschlossen.");
            }
        }
    }
}
