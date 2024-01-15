using WalletApp.ViewModels;

namespace WalletApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        ((BaseViewModel)BindingContext).ContentPage = this;

    }
}