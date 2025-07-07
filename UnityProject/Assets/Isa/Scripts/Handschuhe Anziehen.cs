using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandschuheAnziehen : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable fehlt am Objekt HANDSCHUHE!");
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        gameObject.SetActive(false);
        Debug.Log("Handschuhe angezogen → Objekt deaktiviert.");
    }
}
