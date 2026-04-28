using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteReaderUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text bodyText;

    [Header("Lock Player Scripts")]
    [Tooltip("Drag scripts that control movement/combat here (ex: VThirdPersonInput).")]
    [SerializeField] private MonoBehaviour[] scriptsToDisable;

    private float prevTimeScale;
    public bool IsOpen => panel != null && panel.activeSelf;

    private void Awake()
    {
        if (panel != null) panel.SetActive(false);
    }

    public void Open(string text)
    {
        if (panel == null || bodyText == null)
        {
            Debug.LogError("NoteReaderUI is missing panel/bodyText references.");
            return;
        }

        bodyText.text = text;
        panel.SetActive(true);

        // Lock player by disabling scripts
        foreach (var s in scriptsToDisable)
            if (s != null) s.enabled = false;

        // Freeze world
        prevTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        // Cursor for mouse users
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Close()
    {
        if (!IsOpen) return;

        panel.SetActive(false);

        // Unfreeze world
        Time.timeScale = prevTimeScale;

        // Restore player scripts
        foreach (var s in scriptsToDisable)
            if (s != null) s.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!IsOpen) return;

        // Close while paused (Update still runs when timeScale == 0)
        if (Input.GetKeyDown(KeyCode.Escape))
            Close();
    }
}