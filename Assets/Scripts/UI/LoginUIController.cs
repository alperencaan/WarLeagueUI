using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginUIController : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_InputField confirmPasswordField;

    [Header("Buttons")]
    [SerializeField] private Button loginButton;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private Button newUserButton;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI errorMessageText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private TextMeshProUGUI copyrightText;
    [SerializeField] private TextMeshProUGUI errorText;

    private void Start()
    {
        InitializeUI();
        SetupEventListeners();
        // Başlangıçta login butonunu deaktif et
        SetLoginButtonState(false);
    }

    private void InitializeUI()
    {
        // UI metinlerini ayarla
        titleText.text = "WARLEAGUE™";
        subtitleText.text = "MOBILE";
        copyrightText.text = "©2024 WARLEAGUE™";
        
        // Input field placeholder'ları
        usernameField.placeholder.GetComponent<TextMeshProUGUI>().text = "Kullanıcı Adı";
        passwordField.placeholder.GetComponent<TextMeshProUGUI>().text = "Şifre";
        confirmPasswordField.placeholder.GetComponent<TextMeshProUGUI>().text = "Şifreyi Tekrar Girin";
        
        // Şifre input field'ını password type yap
        passwordField.contentType = TMP_InputField.ContentType.Password;
        confirmPasswordField.contentType = TMP_InputField.ContentType.Password;
        
        // Hata mesajını temizle
        ClearErrorMessage();
    }

    private void SetupEventListeners()
    {
        // Input field değişiklik dinleyicileri
        usernameField.onValueChanged.AddListener(_ => ValidateInputs());
        passwordField.onValueChanged.AddListener(_ => ValidateInputs());
        confirmPasswordField.onValueChanged.AddListener(_ => ValidateInputs());

        // Buton click dinleyicileri
        loginButton.onClick.AddListener(OnLoginButtonClicked);
        forgotPasswordButton.onClick.AddListener(OnForgotPasswordClicked);
        newUserButton.onClick.AddListener(OnNewUserButtonClicked);
    }

    private void ValidateInputs()
    {
        bool isValid = !string.IsNullOrEmpty(usernameField.text) && 
                      !string.IsNullOrEmpty(passwordField.text) &&
                      !string.IsNullOrEmpty(confirmPasswordField.text);

        SetLoginButtonState(isValid);
        
        if (isValid)
        {
            ClearErrorMessage();
        }
    }

    private void SetLoginButtonState(bool isEnabled)
    {
        loginButton.interactable = isEnabled;
        // Buton rengini duruma göre ayarla
        var colors = loginButton.colors;
        colors.normalColor = isEnabled ? new Color(1f, 0.65f, 0f) : new Color(0.5f, 0.5f, 0.5f);
        loginButton.colors = colors;
    }

    private void OnLoginButtonClicked()
    {
        // Giriş bilgilerini kontrol et
        if (ValidateLoginCredentials())
        {
            // Başarılı giriş - Ana menüye yönlendir
            LoadMainMenu();
        }
        else
        {
            // Başarısız giriş
            ShowErrorMessage("Hatalı kullanıcı adı veya şifre!");
        }
    }

    private bool ValidateLoginCredentials()
    {
        // TODO: Gerçek kimlik doğrulama mantığını buraya ekleyin
        // Şimdilik basit bir demo kontrolü
        return usernameField.text.Length >= 3 && passwordField.text.Length >= 6;
    }

    private void OnForgotPasswordClicked()
    {
        // TODO: Şifre sıfırlama ekranına yönlendir
        Debug.Log("Şifre sıfırlama ekranına yönlendiriliyor...");
        // SceneManager.LoadScene("ForgotPasswordScene");
    }

    private void OnNewUserButtonClicked()
    {
        // TODO: Yeni kullanıcı kayıt ekranına yönlendir
        Debug.Log("Yeni kullanıcı kayıt ekranına yönlendiriliyor...");
        // SceneManager.LoadScene("RegisterScene");
    }

    private void LoadMainMenu()
    {
        Debug.Log("LoadMainMenu called");
        try
        {
            // Scene ismini tam olarak Build Settings'teki gibi yazalım
            SceneManager.LoadScene("Scenes/MainMenuScene");  // Yolu tam olarak belirttik
            // veya
            SceneManager.LoadScene(1);  // Index numarası ile de çağırabiliriz
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load MainMenuScene: {e.Message}");
        }
    }

    private void ShowErrorMessage(string message)
    {
        errorMessageText.text = message;
        errorMessageText.color = Color.red;
        errorMessageText.gameObject.SetActive(true);
    }

    private void ClearErrorMessage()
    {
        errorMessageText.text = "";
        errorMessageText.gameObject.SetActive(false);
    }

    private void OnRegisterClicked()
    {
        if (passwordField.text != confirmPasswordField.text)
        {
            ShowError("Passwords do not match!");
            return;
        }

        // TODO: Gerçek kayıt işlemleri burada yapılacak
        Debug.Log($"Registering new user: {usernameField.text}");
        
        // Başarılı kayıt sonrası ana menüye yönlendir
        SceneManager.LoadScene("MainMenuScene");  // LoginScene yerine MainMenuScene'e yönlendiriyoruz
    }

    private void ShowError(string message)
    {
        if (errorText != null)
        {
            errorText.text = message;
            errorText.gameObject.SetActive(true);
        }
    }

    private void HideError()
    {
        if (errorText != null)
        {
            errorText.gameObject.SetActive(false);
        }
    }
} 