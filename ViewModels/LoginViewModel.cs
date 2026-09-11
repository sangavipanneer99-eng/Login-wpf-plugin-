using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using LoginMVVM.Commands;
using LoginMVVM.Views;

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
                string json = File.ReadAllText(
                    @"C:\Users\Sangavi\OneDrive\Desktop\WPF\LoginMVVM\Views\users.json");

                List<User> users =
                    JsonSerializer.Deserialize<List<User>>(json)
                    ?? new List<User>();

                User? user = users.FirstOrDefault(
                    u => u.Username == Username &&
                         u.Password == Password);

                if (user != null)
                {
                    MessageBox.Show(
                        "Login successful!",
                        "Step 1");

                    Dashboard dashboard = new Dashboard();

                    dashboard.Show();

                    Application.Current.Windows
                        .OfType<Login>()
                        .FirstOrDefault()
                        ?.Close();
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
                MessageBox.Show(
                    ex.Message,
                    "Error");
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