using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;

    [Header("Buttons")]
    [SerializeField] private Button startBattleButton;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private Button newWarriorButton;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI errorMessageText;
    [SerializeField] private GameObject errorMessagePanel;

    private void Start()
    {
        // Başlangıçta hata mesajını gizle
        if (errorMessagePanel) errorMessagePanel.SetActive(false);
        
        // Başlangıçta Start Battle butonunu deaktif et
        SetStartBattleButtonState(false);

        // Input field event listeners
        usernameField.onValueChanged.AddListener(OnInputValueChanged);
        passwordField.onValueChanged.AddListener(OnInputValueChanged);

        // Button click listeners
        startBattleButton.onClick.AddListener(OnStartBattleClicked);
        forgotPasswordButton.onClick.AddListener(OnForgotPasswordClicked);
        newWarriorButton.onClick.AddListener(OnNewWarriorClicked);
    }

    private void OnInputValueChanged(string value)
    {
        // Input değiştiğinde hata mesajını temizle
        HideErrorMessage();
        
        // Input validation
        ValidateInputs();
    }

    private void ValidateInputs()
    {
        bool isValid = !string.IsNullOrEmpty(usernameField.text) && 
                      !string.IsNullOrEmpty(passwordField.text) &&
                      usernameField.text.Length >= 3 &&
                      passwordField.text.Length >= 6;

        SetStartBattleButtonState(isValid);
    }

    private void SetStartBattleButtonState(bool isEnabled)
    {
        startBattleButton.interactable = isEnabled;

        // Buton rengini duruma göre ayarla
        var colors = startBattleButton.colors;
        colors.normalColor = isEnabled ? new Color(1f, 0.65f, 0f) : new Color(0.5f, 0.5f, 0.5f);
        startBattleButton.colors = colors;
    }

    private void OnStartBattleClicked()
    {
        // Login işlemi
        if (ValidateLoginCredentials())
        {
            // Başarılı login - Ana menüye yönlendir
            Debug.Log("Login successful! Redirecting to main menu...");
            // TODO: Add your scene loading or menu transition code here
            // SceneManager.LoadScene("MainMenu");
        }
        else
        {
            ShowErrorMessage("Invalid username or password!");
        }
    }

    private bool ValidateLoginCredentials()
    {
        // TODO: Implement your actual login validation logic here
        // This is just a basic example
        return usernameField.text.Length >= 3 && passwordField.text.Length >= 6;
    }

    private void OnForgotPasswordClicked()
    {
        Debug.Log("Forgot password clicked");
        // TODO: Implement forgot password logic
        // Example: Open forgot password panel or load forgot password scene
    }

    private void OnNewWarriorClicked()
    {
        Debug.Log("New warrior registration clicked");
        // TODO: Implement new warrior registration logic
        // Example: Open registration panel or load registration scene
    }

    private void ShowErrorMessage(string message)
    {
        if (errorMessageText)
        {
            errorMessageText.text = message;
            if (errorMessagePanel) errorMessagePanel.SetActive(true);
        }
    }

    private void HideErrorMessage()
    {
        if (errorMessagePanel) errorMessagePanel.SetActive(false);
    }
} 