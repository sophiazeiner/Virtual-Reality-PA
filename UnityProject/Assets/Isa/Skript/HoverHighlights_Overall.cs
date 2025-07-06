using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverHighlights_Overall : MonoBehaviour
{
    public Material highlightMaterial;
    public Material defaultMaterial;

    private MeshRenderer[] meshRenderers;
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        if (grabInteractable != null)
        {
            grabInteractable.hoverEntered.AddListener(OnHoverEnter);
            grabInteractable.hoverExited.AddListener(OnHoverExit);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable missing on: " + gameObject.name);
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        foreach (var renderer in meshRenderers)
        {
            renderer.material = highlightMaterial;
        }
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        foreach (var renderer in meshRenderers)
        {
            renderer.material = defaultMaterial;
        }
    }
}
