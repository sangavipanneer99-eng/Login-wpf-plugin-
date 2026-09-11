using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using LoginMVVM.Commands;


namespace LoginMVVM.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username = "";
        private string _password = "";

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        
            
     private void Login()
{
    try
    {
        // Read users.json
        string json = File.ReadAllText(
    @"C:\Users\Sangavi\OneDrive\Desktop\WPF\LoginMVVM\Views\users.json");

        // Convert JSON into User objects
       List<User> users = JsonSerializer.Deserialize<List<User>>(json)
                    ?? new List<User>();

User? user = users.FirstOrDefault(
    u => u.Username == Username &&
         u.Password == Password);
        if (user != null)
            
        {
            // Step 1
            MessageBox.Show(
                "Login successful!",
                "Step 1");

            // After clicking OK, Step 2 appears
            MessageBox.Show(
                "Welcome to Dashboard!",
                "Step 2");
        }
        else
        {
            MessageBox.Show(
                "Invalid username or password",
                "Login Failed");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Error");
    }
}

        

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }

    public class User
    {
        [JsonPropertyName("username")]
    public string Username { get; set; } = "";

    [JsonPropertyName("password")]
    public string Password { get; set; } = "";
    }
}

