using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OverallAnziehen : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Hole das XRGrabInteractable-Component vom aktuellen GameObject
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Füge die Methode OnGrabbed als Listener hinzu, wenn das Objekt gegriffen wird
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable fehlt am Objekt OVERALL!");
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Deaktiviert das tatsächlich gegriffene Objekt (auch wenn es instanziiert wurde)
        args.interactableObject.transform.gameObject.SetActive(false);

        Debug.Log("Overall angezogen → Objekt deaktiviert.");
    }
}

