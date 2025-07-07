using UnityEngine;

public class MikroskopProximityViewer : MonoBehaviour
{
    public GameObject mikroskopBild;             // Das UI-Bild (World Space)
    public Transform kopfTransform;              // Die XR Kamera (CenterEyeAnchor o. ä.)
    public float aktivierungsDistanz = 0.3f;     // Wie nah man dran sein muss

    private bool bildAktiv = false;

    void Update()
    {
        float distanz = Vector3.Distance(transform.position, kopfTransform.position);

        if (distanz < aktivierungsDistanz && !bildAktiv)
        {
            mikroskopBild.SetActive(true);
            bildAktiv = true;
        }
        else if (distanz >= aktivierungsDistanz && bildAktiv)
        {
            mikroskopBild.SetActive(false);
            bildAktiv = false;
        }
    }
}