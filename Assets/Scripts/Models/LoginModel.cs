using UnityEngine;

namespace WarLeagueUI.Models
{
    public class LoginModel
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }
        public bool IsLoggedIn { get; private set; }

        private const int MIN_USERNAME_LENGTH = 3;
        private const int MIN_PASSWORD_LENGTH = 6;

        public LoginModel()
        {
            Username = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            IsLoggedIn = false;
        }

        public void SetCredentials(string username, string password, string confirmPassword = "")
        {
            Username = username;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public bool ValidateLoginCredentials()
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(Password) &&
                   Username.Length >= MIN_USERNAME_LENGTH &&
                   Password.Length >= MIN_PASSWORD_LENGTH;
        }

        public bool ValidateRegistrationCredentials()
        {
            return ValidateLoginCredentials() &&
                   !string.IsNullOrEmpty(ConfirmPassword) &&
                   Password == ConfirmPassword;
        }

        public void SetLoggedIn(bool isLoggedIn)
        {
            IsLoggedIn = isLoggedIn;
        }
    }
} 