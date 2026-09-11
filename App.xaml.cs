using System.Windows;
using LoginMVVM.Views;

namespace LoginMVVM;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Login login = new Login();
        login.Show();
    }
}