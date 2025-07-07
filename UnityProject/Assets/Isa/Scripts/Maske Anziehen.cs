using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaskeAnziehen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FaceTrigger"))
        {
            // Objekt "verschwindet" (z. B. angezogen)
            gameObject.SetActive(false);
            Debug.Log("Maske angezogen – Objekt deaktiviert.");
        }
    }
}