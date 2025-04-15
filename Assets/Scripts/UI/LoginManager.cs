using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField _usernameField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private TMP_InputField _confirmPasswordField;

    [Header("Buttons")]
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _registerButton;
    [SerializeField] private Button _forgotPasswordButton;

    [Header("UI Text Elements")]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _subtitleText;
    [SerializeField] private TextMeshProUGUI _errorText;
    [SerializeField] private TextMeshProUGUI _copyrightText;

    private const string MAIN_MENU_SCENE = "MainMenuScene";
    private const int MIN_USERNAME_LENGTH = 3;
    private const int MIN_PASSWORD_LENGTH = 6;

    private void Start()
    {
        InitializeUI();
        SetupEventListeners();
    }

    private void InitializeUI()
    {
        SetupUITexts();
        SetupInputFields();
        UpdateLoginButtonState(false);
        HideError();
    }

    private void SetupUITexts()
    {
        _titleText.text = "WARLEAGUE™";
        _subtitleText.text = "MOBILE";
        _copyrightText.text = "©2024 WARLEAGUE™";
    }

    private void SetupInputFields()
    {
        SetInputFieldProperties(_usernameField, "Username", TMP_InputField.ContentType.Standard);
        SetInputFieldProperties(_passwordField, "Password", TMP_InputField.ContentType.Password);
        SetInputFieldProperties(_confirmPasswordField, "Confirm Password", TMP_InputField.ContentType.Password);
    }

    private void SetInputFieldProperties(TMP_InputField field, string placeholder, TMP_InputField.ContentType contentType)
    {
        if (field != null)
        {
            field.contentType = contentType;
            field.placeholder.GetComponent<TextMeshProUGUI>().text = placeholder;
        }
    }

    private void SetupEventListeners()
    {
        _usernameField.onValueChanged.AddListener(_ => ValidateInputs());
        _passwordField.onValueChanged.AddListener(_ => ValidateInputs());
        _confirmPasswordField.onValueChanged.AddListener(_ => ValidateInputs());

        _loginButton.onClick.AddListener(HandleLogin);
        _registerButton.onClick.AddListener(HandleRegistration);
        _forgotPasswordButton.onClick.AddListener(HandleForgotPassword);
    }

    private void ValidateInputs()
    {
        bool isValid = ValidateLoginFields();
        UpdateLoginButtonState(isValid);
        
        if (isValid)
        {
            HideError();
        }
    }

    private bool ValidateLoginFields()
    {
        return !string.IsNullOrEmpty(_usernameField.text) &&
               !string.IsNullOrEmpty(_passwordField.text) &&
               _usernameField.text.Length >= MIN_USERNAME_LENGTH &&
               _passwordField.text.Length >= MIN_PASSWORD_LENGTH;
    }

    private void UpdateLoginButtonState(bool isEnabled)
    {
        _loginButton.interactable = isEnabled;
        var colors = _loginButton.colors;
        colors.normalColor = isEnabled ? new Color(1f, 0.65f, 0f) : new Color(0.5f, 0.5f, 0.5f);
        _loginButton.colors = colors;
    }

    private void HandleLogin()
    {
        if (ValidateLoginFields())
        {
            // TODO: Implement actual login logic here
            LoadMainMenu();
        }
        else
        {
            ShowError("Invalid username or password!");
        }
    }

    private void HandleRegistration()
    {
        if (_passwordField.text != _confirmPasswordField.text)
        {
            ShowError("Passwords do not match!");
            return;
        }

        // TODO: Implement actual registration logic here
        Debug.Log($"Registering new user: {_usernameField.text}");
        LoadMainMenu();
    }

    private void HandleForgotPassword()
    {
        // TODO: Implement forgot password logic
        Debug.Log("Forgot password process initiated");
    }

    private void LoadMainMenu()
    {
        try
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load {MAIN_MENU_SCENE}: {e.Message}");
            ShowError("Failed to load main menu");
        }
    }

    private void ShowError(string message)
    {
        if (_errorText != null)
        {
            _errorText.text = message;
            _errorText.gameObject.SetActive(true);
        }
    }

    private void HideError()
    {
        if (_errorText != null)
        {
            _errorText.gameObject.SetActive(false);
        }
    }
} 