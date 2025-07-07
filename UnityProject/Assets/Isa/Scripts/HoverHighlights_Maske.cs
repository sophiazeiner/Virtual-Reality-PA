using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverHighlights_Maske : MonoBehaviour
{
    public Material highlightMaterial;
    public Material defaultMaterial;

    private Renderer[] renderers;
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Alle Renderer (MeshRenderer & SkinnedMeshRenderer) sammeln
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        var skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        renderers = new Renderer[meshRenderers.Length + skinnedRenderers.Length];
        meshRenderers.CopyTo(renderers, 0);
        skinnedRenderers.CopyTo(renderers, meshRenderers.Length);

        if (grabInteractable != null)
        {
            grabInteractable.hoverEntered.AddListener(OnHoverEnter);
            grabInteractable.hoverExited.AddListener(OnHoverExit);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable fehlt auf: " + gameObject.name);
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        foreach (var rend in renderers)
        {
            Material[] newMats = new Material[rend.materials.Length];
            for (int i = 0; i < newMats.Length; i++)
            {
                newMats[i] = highlightMaterial;
            }
            rend.materials = newMats;
        }
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        foreach (var rend in renderers)
        {
            Material[] newMats = new Material[rend.materials.Length];
            for (int i = 0; i < newMats.Length; i++)
            {
                newMats[i] = defaultMaterial;
            }
            rend.materials = newMats;
        }
    }
}
