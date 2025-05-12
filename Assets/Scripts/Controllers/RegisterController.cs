using UnityEngine;
using UnityEngine.SceneManagement;
using WarLeagueUI.Models;
using WarLeagueUI.Views;

namespace WarLeague.Controllers
{
    public class RegisterController : MonoBehaviour
    {
        [SerializeField] private RegisterView registerView;
        private RegisterModel registerModel;

        private const string LOGIN_SCENE = "LoginScene";

        private void Awake()
        {
            registerModel = new RegisterModel();
        }

        private void Start()
        {
            registerView.RegisterButton.onClick.AddListener(HandleRegister);
            registerView.BackToLoginButton.onClick.AddListener(HandleBackToLogin);
            registerView.UsernameField.onValueChanged.AddListener(_ => ValidateInputs());
            registerView.EmailField.onValueChanged.AddListener(_ => ValidateInputs());
            registerView.PasswordField.onValueChanged.AddListener(_ => ValidateInputs());
            registerView.ConfirmPasswordField.onValueChanged.AddListener(_ => ValidateInputs());
            SetRegisterButtonState(false);
        }

        private void ValidateInputs()
        {
            registerModel.SetCredentials(
                registerView.UsernameField.text,
                registerView.EmailField.text,
                registerView.PasswordField.text,
                registerView.ConfirmPasswordField.text
            );
            bool isValid = registerModel.IsValid();
            SetRegisterButtonState(isValid);
            if (isValid)
            {
                registerView.HideError();
            }
        }

        private void SetRegisterButtonState(bool isEnabled)
        {
            registerView.RegisterButton.interactable = isEnabled;
            var colors = registerView.RegisterButton.colors;
            colors.normalColor = isEnabled ? new Color(1f, 0.65f, 0f) : new Color(0.5f, 0.5f, 0.5f);
            registerView.RegisterButton.colors = colors;
        }

        private void HandleRegister()
        {
            registerModel.SetCredentials(
                registerView.UsernameField.text,
                registerView.EmailField.text,
                registerView.PasswordField.text,
                registerView.ConfirmPasswordField.text
            );
            if (!registerModel.IsValid())
            {
                registerView.ShowError(registerModel.GetValidationError());
                return;
            }
            // TODO: Gerçek kayıt işlemleri burada yapılacak
            Debug.Log($"Yeni kullanıcı kaydediliyor: {registerModel.Username}");
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
                registerView.ShowError("Giriş ekranına dönülemedi!");
            }
        }
    }
} 