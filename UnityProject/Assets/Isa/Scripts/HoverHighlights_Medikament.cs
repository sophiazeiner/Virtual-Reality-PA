using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverHighlights_Medikament : MonoBehaviour
{
    public Material highlightMaterial;

    private MeshRenderer[] meshRenderers;
    private XRGrabInteractable grabInteractable;
    private Material[][] originalMaterials; // 2D-Array für mehrere Materialien pro Renderer

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        // Originalmaterialien speichern
        originalMaterials = new Material[meshRenderers.Length][];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalMaterials[i] = meshRenderers[i].materials;
        }

        if (grabInteractable != null)
        {
            grabInteractable.hoverEntered.AddListener(OnHoverEnter);
            grabInteractable.hoverExited.AddListener(OnHoverExit);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable fehlt an: " + gameObject.name);
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            Material[] highlightMats = new Material[meshRenderers[i].materials.Length];
            for (int j = 0; j < highlightMats.Length; j++)
            {
                highlightMats[j] = highlightMaterial;
            }
            meshRenderers[i].materials = highlightMats;
        }
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].materials = originalMaterials[i];
        }
    }
}
