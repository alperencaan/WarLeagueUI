using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using WarLeague.Controllers;

namespace WarLeague.Controllers
{
    public class LoginController : MonoBehaviour
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

        private const string MAIN_MENU_SCENE = "Scenes/MainMenuScene";
        private const string REGISTER_SCENE = "Scenes/RegisterScene";
        private const int MIN_USERNAME_LENGTH = 3;
        private const int MIN_PASSWORD_LENGTH = 6;

        private void Start()
        {
            InitializeUI();
            SetupEventListeners();
            Debug.Log("LoginController started");
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
            if (_titleText != null) _titleText.text = "WARLEAGUE™";
            if (_subtitleText != null) _subtitleText.text = "MOBILE";
            if (_copyrightText != null) _copyrightText.text = "©2024 WARLEAGUE™";
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
            if (_usernameField != null)
                _usernameField.onValueChanged.AddListener(_ => ValidateInputs());
            if (_passwordField != null)
                _passwordField.onValueChanged.AddListener(_ => ValidateInputs());
            if (_confirmPasswordField != null)
                _confirmPasswordField.onValueChanged.AddListener(_ => ValidateInputs());

            if (_loginButton != null)
            {
                _loginButton.onClick.AddListener(HandleLogin);
                Debug.Log("Login button listener added");
            }
            if (_registerButton != null)
            {
                _registerButton.onClick.AddListener(HandleRegistration);
                Debug.Log("Register button listener added");
            }
            if (_forgotPasswordButton != null)
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
            if (_loginButton != null)
            {
                _loginButton.interactable = isEnabled;
                var colors = _loginButton.colors;
                colors.normalColor = isEnabled ? new Color(1f, 0.65f, 0f) : new Color(0.5f, 0.5f, 0.5f);
                _loginButton.colors = colors;
            }
        }

        private void HandleLogin()
        {
            Debug.Log("Login button clicked");
            if (ValidateLoginFields())
            {
                SceneController.Instance.LoadMainMenuScene();
            }
            else
            {
                ShowError("Invalid username or password!");
            }
        }

        private void HandleRegistration()
        {
            Debug.Log("Register button clicked");
            SceneController.Instance.LoadRegisterScene();
        }

        private void HandleForgotPassword()
        {
            Debug.Log("Forgot password button clicked");
        }

        private void LoadScene(string sceneName)
        {
            Debug.Log($"Attempting to load scene: {sceneName}");
            try
            {
                SceneManager.LoadScene(sceneName);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load {sceneName}: {e.Message}");
                ShowError($"Failed to load {sceneName}");
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

        public void HandleSettings()
        {
            var settingsController = FindObjectOfType<SettingsController>();
            if (settingsController != null)
            {
                settingsController.ShowSettings();
            }
        }
    }
} 