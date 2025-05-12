using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace WarLeagueUI.Views
{
    public class RegisterView : MonoBehaviour
    {
        public TMP_InputField UsernameField;
        public TMP_InputField EmailField;
        public TMP_InputField PasswordField;
        public TMP_InputField ConfirmPasswordField;
        public Button RegisterButton;
        public Button BackToLoginButton;
        public TextMeshProUGUI ErrorText;

        public void ShowError(string message)
        {
            if (ErrorText != null)
            {
                ErrorText.text = message;
                ErrorText.gameObject.SetActive(true);
            }
        }

        public void HideError()
        {
            if (ErrorText != null)
            {
                ErrorText.gameObject.SetActive(false);
            }
        }
    }
} 