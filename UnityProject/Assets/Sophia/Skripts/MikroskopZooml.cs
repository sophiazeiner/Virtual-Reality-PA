using UnityEngine;

public class MikroskopBildEinblender : MonoBehaviour
{
    public GameObject mikroskopBild; // Das UI-Bild-Objekt

    private bool istAktiv = false;

    public void Umschalten()
    {
        istAktiv = !istAktiv;
        mikroskopBild.SetActive(istAktiv);
    }
}