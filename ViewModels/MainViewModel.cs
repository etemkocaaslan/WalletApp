namespace WalletApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            ProcessShipmentsRedirectionCommand = new Command(async () =>
            {
                //Bir yeni view olustur. 
                await ContentPage?.Navigation?.PushAsync(new Page());
            }, () => { return !IsBusy; });
        }

        public override void PropertyUpdates()
        {
        }
        public override void CommandUpdates()
        {
            TriggerUpdate(ProcessShipmentsRedirectionCommand);
        }

        public Command ProcessShipmentsRedirectionCommand { get; }
    }
}