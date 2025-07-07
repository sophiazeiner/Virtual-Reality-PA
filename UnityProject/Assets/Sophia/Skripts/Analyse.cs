using UnityEngine;

public class MedikamentAnalyse : MonoBehaviour
{
    public GameObject infobox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Medikament"))
        {
            infobox.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Medikament"))
        {
            infobox.SetActive(false);
        }
    }
}
