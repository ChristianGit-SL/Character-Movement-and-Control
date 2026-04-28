using UnityEngine;

public class NotePickup : MonoBehaviour
{
    [TextArea(3, 10)]
    public string noteText;

    [Header("Prompt (turns on/off)")]
    [SerializeField] private GameObject promptObject;

    [Header("Interaction")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private NoteReaderUI noteUI;
    private bool playerInRange;

    private void Awake()
    {
        if (promptObject != null)
            promptObject.SetActive(false);
    }

    private void Start()
    {
        noteUI = FindFirstObjectByType<NoteReaderUI>();
        if (noteUI == null)
            Debug.LogError("No NoteReaderUI found in scene. Create UIManager and add NoteReaderUI.");
    }

    private void Update()
    {
        if (!playerInRange) return;
        if (noteUI == null) return;
        if (noteUI.IsOpen) return;

        if (Input.GetKeyDown(interactKey))
            noteUI.Open(noteText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        if (promptObject != null)
            promptObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        if (promptObject != null)
            promptObject.SetActive(false);
    }
}