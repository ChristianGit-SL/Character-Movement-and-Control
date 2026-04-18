using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Groups / Panels")]
    [SerializeField] private GameObject pressToStartGroup;
    [SerializeField] private GameObject mainButtonsGroup;
    [SerializeField] private GameObject settingsPanel;

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [Header("Scene To Load")]
    [SerializeField] private string gameSceneName = "Game";

    private UIInputActions actions;
    private bool started;

    private void Awake()
    {
        actions = new UIInputActions();
    }

    private void OnEnable()
    {
        actions.Enable();
        actions.UI.Submit.performed += OnSubmit;
        actions.UI.Cancel.performed += OnCancel;
    }

    private void OnDisable()
    {
        actions.UI.Submit.performed -= OnSubmit;
        actions.UI.Cancel.performed -= OnCancel;
        actions.Disable();
    }

    private void Start()
    {
        // Initial UI state
        started = false;
        if (pressToStartGroup) pressToStartGroup.SetActive(true);
        if (mainButtonsGroup) mainButtonsGroup.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);

        // Hook up button click events
        if (startButton) startButton.onClick.AddListener(DoStart);
        if (settingsButton) settingsButton.onClick.AddListener(OpenSettings);
        if (quitButton) quitButton.onClick.AddListener(DoQuit);
        if (backButton) backButton.onClick.AddListener(CloseSettings);
    }

    private void OnSubmit(InputAction.CallbackContext ctx)
    {
        // First submit switches from "Press to Start" to the main menu buttons
        if (!started)
        {
            started = true;
            if (pressToStartGroup) pressToStartGroup.SetActive(false);
            if (mainButtonsGroup) mainButtonsGroup.SetActive(true);

            // Ensure controller navigation starts on a valid button
            if (startButton) startButton.Select();
            return;
        }
    }

    private void OnCancel(InputAction.CallbackContext ctx)
    {
        // Cancel closes settings if open
        if (settingsPanel != null && settingsPanel.activeSelf)
        {
            CloseSettings();
            return;
        }

        // Optional: cancel returns to "press to start"
        if (started)
        {
            started = false;
            if (mainButtonsGroup) mainButtonsGroup.SetActive(false);
            if (pressToStartGroup) pressToStartGroup.SetActive(true);
        }
    }

    private void DoStart()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void OpenSettings()
    {
        if (settingsPanel) settingsPanel.SetActive(true);
        if (mainButtonsGroup) mainButtonsGroup.SetActive(false);
        if (backButton) backButton.Select();
    }

    private void CloseSettings()
    {
        if (settingsPanel) settingsPanel.SetActive(false);
        if (mainButtonsGroup) mainButtonsGroup.SetActive(true);
        if (settingsButton) settingsButton.Select();
    }

    private void DoQuit()
    {
        Application.Quit();
    }
}