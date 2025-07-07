using UnityEngine;

public class MikroskopInteraktionn : MonoBehaviour
{
    // Dieses Feld muss öffentlich (public) sein, damit es im Inspector erscheint
    public GameObject analyseCanvas;

    private bool canvasAktiv = false;

    public void ToggleAnalysemikro()
    {
        canvasAktiv = !canvasAktiv;
        analyseCanvas.SetActive(canvasAktiv);
    }
}