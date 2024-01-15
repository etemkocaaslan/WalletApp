using WalletApp.Views;

namespace WalletApp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string username = string.Empty;
        private string password = string.Empty;
        public LoginPageViewModel()
        {
            #region Commands
            LoginCommand = new Command(async () =>
            {
                await OnLogin();
            }, () => { return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !IsBusy; });

            ChangePasswordCommand = new Command(async () =>
            {
                await OnLogin();
            }, () => { return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !IsBusy; });
            #endregion
        }

        private async Task OnLogin()
        {
            bool isLoggedIn = await Manager.POSTLoginAsync(this, username, password);
            if (isLoggedIn)
            {
                NumberOfSuccessfulLogins++;
                await ContentPage?.Navigation?.PushAsync(new MainPage());
            }
        }

        public override void CommandUpdates()
        {
            TriggerUpdate(LoginCommand);
            TriggerUpdate(ChangePasswordCommand);
        }
        public override void PropertyUpdates()
        {
            TriggerUpdate(nameof(Username));
            TriggerUpdate(nameof(Password));
        }

        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public uint NumberOfSuccessfulLogins { get; set; }
        public Command LoginCommand { get; }
        public Command ChangePasswordCommand { get; }

    }
}
