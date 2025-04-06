using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RegisterManager : MonoBehaviour
{
    // Input alanları için referanslar
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField usernameField;        // Kullanıcı adı
    [SerializeField] private TMP_InputField emailField;           // E-posta
    [SerializeField] private TMP_InputField passwordField;        // Şifre
    [SerializeField] private TMP_InputField confirmPasswordField; // Şifre tekrar

    // Butonlar için referanslar
    [Header("Buttons")]
    [SerializeField] private Button registerButton;     // Kayıt ol butonu
    [SerializeField] private Button backToLoginButton;  // Geri dön butonu

    // UI elementleri için referanslar
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI errorText; // Hata mesajı

    private void Start()
    {
        // Input alanları için değişiklik dinleyicileri
        usernameField.onValueChanged.AddListener(_ => ValidateInputs());
        emailField.onValueChanged.AddListener(_ => ValidateInputs());
        passwordField.onValueChanged.AddListener(_ => ValidateInputs());
        confirmPasswordField.onValueChanged.AddListener(_ => ValidateInputs());

        // Buton dinleyicileri
        registerButton.onClick.AddListener(OnRegisterClicked);
        backToLoginButton.onClick.AddListener(OnBackToLoginClicked);

        // Başlangıçta kayıt butonunu deaktif et
        SetRegisterButtonState(false);
    }

    // Tüm input alanlarının doğruluğunu kontrol eder
    private void ValidateInputs()
    {
        bool isValid = !string.IsNullOrEmpty(usernameField.text) &&
                      !string.IsNullOrEmpty(emailField.text) &&
                      !string.IsNullOrEmpty(passwordField.text) &&
                      !string.IsNullOrEmpty(confirmPasswordField.text) &&
                      passwordField.text == confirmPasswordField.text &&
                      IsValidEmail(emailField.text) &&
                      passwordField.text.Length >= 6;

        SetRegisterButtonState(isValid);
        
        if (isValid)
        {
            HideError();
        }
    }

    // E-posta formatını kontrol eder
    private bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    // Kayıt butonunun durumunu ayarlar
    private void SetRegisterButtonState(bool isEnabled)
    {
        registerButton.interactable = isEnabled;
        var colors = registerButton.colors;
        colors.normalColor = isEnabled ? new Color(1f, 0.65f, 0f) : new Color(0.5f, 0.5f, 0.5f);
        registerButton.colors = colors;
    }

    // Kayıt ol butonu tıklandığında
    private void OnRegisterClicked()
    {
        if (passwordField.text != confirmPasswordField.text)
        {
            ShowError("Passwords do not match!");
            return;
        }

        // TODO: Gerçek kayıt işlemleri burada yapılacak
        Debug.Log($"Registering new user: {usernameField.text}");
        
        // Başarılı kayıt sonrası login ekranına dön
        SceneManager.LoadScene("LoginScene");
    }

    // Geri dön butonu tıklandığında
    private void OnBackToLoginClicked()
    {
        SceneManager.LoadScene("LoginScene");
    }

    // Hata mesajını gösterir
    private void ShowError(string message)
    {
        if (errorText != null)
        {
            errorText.text = message;
            errorText.gameObject.SetActive(true);
        }
    }

    // Hata mesajını gizler
    private void HideError()
    {
        if (errorText != null)
        {
            errorText.gameObject.SetActive(false);
        }
    }
} 