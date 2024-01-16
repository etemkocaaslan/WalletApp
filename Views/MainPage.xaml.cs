using WalletApp.ViewModels;

namespace WalletApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).ContentPage = this;
        }
    }

}
