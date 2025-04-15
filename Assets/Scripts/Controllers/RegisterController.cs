using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace WarLeague.Controllers
{
    public class RegisterController : MonoBehaviour
    {
        [Header("Giriş Alanları")]
        [SerializeField] private TMP_InputField _usernameField;
        [SerializeField] private TMP_InputField _emailField;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private TMP_InputField _confirmPasswordField;

        [Header("Butonlar")]
        [SerializeField] private Button _registerButton;
        [SerializeField] private Button _backToLoginButton;

        [Header("UI Elementleri")]
        [SerializeField] private TextMeshProUGUI _errorText;

        private const string LOGIN_SCENE = "LoginScene";
        private const int MIN_USERNAME_LENGTH = 3;
        private const int MIN_PASSWORD_LENGTH = 6;

        private void Start()
        {
            SetupEventListeners();
            SetRegisterButtonState(false);
        }

        private void SetupEventListeners()
        {
            _usernameField.onValueChanged.AddListener(_ => ValidateInputs());
            _emailField.onValueChanged.AddListener(_ => ValidateInputs());
            _passwordField.onValueChanged.AddListener(_ => ValidateInputs());
            _confirmPasswordField.onValueChanged.AddListener(_ => ValidateInputs());

            _registerButton.onClick.AddListener(HandleRegister);
            _backToLoginButton.onClick.AddListener(HandleBackToLogin);
        }

        private void ValidateInputs()
        {
            bool isValid = !string.IsNullOrEmpty(_usernameField.text) &&
                         !string.IsNullOrEmpty(_emailField.text) &&
                         !string.IsNullOrEmpty(_passwordField.text) &&
                         !string.IsNullOrEmpty(_confirmPasswordField.text) &&
                         _passwordField.text == _confirmPasswordField.text &&
                         IsValidEmail(_emailField.text) &&
                         _usernameField.text.Length >= MIN_USERNAME_LENGTH &&
                         _passwordField.text.Length >= MIN_PASSWORD_LENGTH;

            SetRegisterButtonState(isValid);
            
            if (isValid)
            {
                HideError();
            }
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private void SetRegisterButtonState(bool isEnabled)
        {
            _registerButton.interactable = isEnabled;
            var colors = _registerButton.colors;
            colors.normalColor = isEnabled ? new Color(1f, 0.65f, 0f) : new Color(0.5f, 0.5f, 0.5f);
            _registerButton.colors = colors;
        }

        private void HandleRegister()
        {
            if (_passwordField.text != _confirmPasswordField.text)
            {
                ShowError("Şifreler eşleşmiyor!");
                return;
            }

            if (_usernameField.text.Length < MIN_USERNAME_LENGTH)
            {
                ShowError($"Kullanıcı adı en az {MIN_USERNAME_LENGTH} karakter olmalıdır!");
                return;
            }

            if (_passwordField.text.Length < MIN_PASSWORD_LENGTH)
            {
                ShowError($"Şifre en az {MIN_PASSWORD_LENGTH} karakter olmalıdır!");
                return;
            }

            if (!IsValidEmail(_emailField.text))
            {
                ShowError("Geçerli bir e-posta adresi giriniz!");
                return;
            }

            // TODO: Gerçek kayıt işlemleri burada yapılacak
            Debug.Log($"Yeni kullanıcı kaydediliyor: {_usernameField.text}");
            
            // Başarılı kayıt sonrası giriş ekranına dön
            LoadLoginScene();
        }

        private void HandleBackToLogin()
        {
            LoadLoginScene();
        }

        private void LoadLoginScene()
        {
            try
            {
                SceneManager.LoadScene(LOGIN_SCENE);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"{LOGIN_SCENE} yüklenirken hata oluştu: {e.Message}");
                ShowError("Giriş ekranına dönülemedi!");
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
} 