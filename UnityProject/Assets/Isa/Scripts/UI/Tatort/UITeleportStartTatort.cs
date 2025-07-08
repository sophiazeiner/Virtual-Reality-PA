using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UITeleportStartTatort : MonoBehaviour
{
    public GameObject canvasUI;

    private void OnEnable()
    {
        // Nur falls Teleportation Anchor genutzt wird
        var teleportAnchor = GetComponent<TeleportationAnchor>();
        if (teleportAnchor != null)
        {
            teleportAnchor.teleporting.AddListener(OnTeleportHere);
        }
    }

    private void OnDisable()
    {
        var teleportAnchor = GetComponent<TeleportationAnchor>();
        if (teleportAnchor != null)
        {
            teleportAnchor.teleporting.RemoveListener(OnTeleportHere);
        }
    }

    void OnTeleportHere(TeleportingEventArgs args)
    {
        if (canvasUI != null)
        {
            canvasUI.SetActive(true);
        }
    }
}
