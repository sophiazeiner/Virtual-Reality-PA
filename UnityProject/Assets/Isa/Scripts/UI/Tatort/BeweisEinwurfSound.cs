using UnityEngine;

public class ObjektInBoxErkennung : MonoBehaviour
{
    [Header("Zielbereich (mit Collider)")]
    public GameObject zielBoxObjekt; // Du ziehst hier einfach das GameObject mit dem Collider rein

    [Header("Sound")]
    public AudioClip platziereSound;
    public AudioSource audioSource; // Optional: falls du den Sound gezielt irgendwo abspielen willst

    private void OnTriggerEnter(Collider other)
    {
        // Sicherstellen, dass das Zielobjekt einen Collider hat
        if (zielBoxObjekt == null) return;

        Collider zielCollider = zielBoxObjekt.GetComponent<Collider>();
        if (zielCollider == null) return;

        // Prüfen, ob das aktuelle Objekt in die Zielbox eingelegt wurde
        if (other == zielCollider)
        {
            Debug.Log("Objekt wurde korrekt in die Box gelegt!");

            // Sound abspielen
            if (platziereSound != null)
            {
                if (audioSource != null)
                    audioSource.PlayOneShot(platziereSound);
                else
                    AudioSource.PlayClipAtPoint(platziereSound, transform.position);
            }
        }
    }
}
