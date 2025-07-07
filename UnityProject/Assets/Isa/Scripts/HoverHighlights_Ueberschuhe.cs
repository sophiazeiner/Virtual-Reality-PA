using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;

public class HoverHighlights_Ueberschuhe : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;  // Leuchtmaterial
    [SerializeField] private Material defaultMaterial;    // Ursprungsmaterial

    private Renderer[] renderers;
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Alle MeshRenderer UND SkinnedMeshRenderer sammeln
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        var skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        // Zusammenführen in ein gemeinsames Renderer-Array
        renderers = meshRenderers.Cast<Renderer>()
                                 .Concat(skinnedRenderers.Cast<Renderer>())
                                 .ToArray();

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
