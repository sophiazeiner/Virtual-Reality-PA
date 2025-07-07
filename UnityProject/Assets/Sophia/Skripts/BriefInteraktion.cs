using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BriefInteraktion : MonoBehaviour
{
    public GameObject textPlanePrefab;     // Das Prefab mit Text
    public Transform spawnPosition;        // Wo die Plane erscheinen soll

    private XRBaseInteractable interactable;
    private GameObject spawnedPlane;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
    }

    void OnEnable()
    {
        interactable.selectEntered.AddListener(OnBriefGeklickt);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnBriefGeklickt);
    }

    void OnBriefGeklickt(SelectEnterEventArgs args)
    {
        if (spawnedPlane == null && textPlanePrefab != null && spawnPosition != null)
        {
            spawnedPlane = Instantiate(textPlanePrefab, spawnPosition.position, spawnPosition.rotation);
        }
    }
}