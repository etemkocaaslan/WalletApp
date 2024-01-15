using Prism.Mvvm;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WalletApp.ViewModels
{
    public abstract class BaseViewModel : BindableBase, INotifyPropertyChanged
    {
        public abstract void PropertyUpdates();
        public abstract void CommandUpdates();
        protected bool SetPropertyWithoutUpdates<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            return base.SetProperty(ref storage, value, propertyName);
        }
        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            bool result = base.SetProperty(ref storage, value, propertyName);
            if (result)
                AllUpdates();

            return result;
        }
        protected void TriggerUpdate(Command command)
        {
            command.ChangeCanExecute();
        }
        protected void TriggerUpdate(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }
        protected void AllUpdates()
        {
            PropertyUpdates();
            CommandUpdates();
        }
        public bool IsBusy
        {
            get => ContentPage != null && ContentPage.IsBusy;
            set
            {
                if (ContentPage != null)
                {
                    ContentPage.IsBusy = value;
                }

                AllUpdates();
            }
        }
        public ContentPage ContentPage { get; set; }

    }
}
