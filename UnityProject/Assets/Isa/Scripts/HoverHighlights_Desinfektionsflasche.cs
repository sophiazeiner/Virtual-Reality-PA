using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverHighlight_Desinfektionsflasche : MonoBehaviour
{
    [Header("Highlight-Material (nur Slot 0 wird ersetzt)")]
    public Material highlightMaterial;
    public Material defaultMaterial;

    [Header("Optional: Hover-Sound")]
    public AudioSource hoverSound;

    private MeshRenderer[] meshRenderers;
    private XRGrabInteractable grabInteractable;
    private bool isHovered = false;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        if (grabInteractable != null)
        {
            grabInteractable.hoverEntered.AddListener(OnHoverEnter);
            grabInteractable.hoverExited.AddListener(OnHoverExit);
            grabInteractable.selectEntered.AddListener(OnSelectEnter);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable fehlt an: " + gameObject.name);
        }

        ResetMaterial();
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (highlightMaterial == null || grabInteractable.isSelected)
            return;

        isHovered = true;

        foreach (var renderer in meshRenderers)
        {
            Material[] mats = renderer.materials;
            if (mats.Length > 0)
            {
                mats[0] = highlightMaterial;
                renderer.materials = mats;
            }
        }

        if (hoverSound != null)
        {
            hoverSound.Play();
        }
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        isHovered = false;
        ResetMaterial();
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        isHovered = false;
        ResetMaterial();
    }

    private void ResetMaterial()
    {
        if (defaultMaterial == null) return;

        foreach (var renderer in meshRenderers)
        {
            Material[] mats = renderer.materials;
            if (mats.Length > 0)
            {
                mats[0] = defaultMaterial;
                renderer.materials = mats;
            }
        }
    }
}
