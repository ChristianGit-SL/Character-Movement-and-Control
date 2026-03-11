using UnityEngine;
using TMPro;

public class HealthPackPickup : MonoBehaviour
{
    [Header("UI Prompt")]
    [SerializeField] private TMP_Text promptText;

    [Header("Input")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private bool playerInRange = false;

    private void Start()
    {
        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(interactKey))
        {
            // Later you’ll add: heal player here
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        if (promptText != null)
            promptText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }
}
