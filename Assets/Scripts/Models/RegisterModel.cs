namespace WarLeagueUI.Models
{
    public class RegisterModel
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }

        private const int MIN_USERNAME_LENGTH = 3;
        private const int MIN_PASSWORD_LENGTH = 6;

        public void SetCredentials(string username, string email, string password, string confirmPassword)
        {
            Username = username;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(Password) &&
                   !string.IsNullOrEmpty(ConfirmPassword) &&
                   Password == ConfirmPassword &&
                   IsValidEmail(Email) &&
                   Username.Length >= MIN_USERNAME_LENGTH &&
                   Password.Length >= MIN_PASSWORD_LENGTH;
        }

        public string GetValidationError()
        {
            if (Password != ConfirmPassword)
                return "Şifreler eşleşmiyor!";
            if (Username.Length < MIN_USERNAME_LENGTH)
                return $"Kullanıcı adı en az {MIN_USERNAME_LENGTH} karakter olmalıdır!";
            if (Password.Length < MIN_PASSWORD_LENGTH)
                return $"Şifre en az {MIN_PASSWORD_LENGTH} karakter olmalıdır!";
            if (!IsValidEmail(Email))
                return "Geçerli bir e-posta adresi giriniz!";
            return null;
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }
    }
} 