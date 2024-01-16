using WalletApp.ViewModels;

namespace WalletApp
{
    public static class Manager
    {
        private static SortedList<string, int> requestList = new();

        public static async Task<bool> POSTLoginAsync(BaseViewModel callerViewModel, string username, string password)
        {
            const string Request_ID = "Post-Login";
            await PreRequest(callerViewModel);
            try
            {
                return true; 
            }
            catch (Exception e)
            {
                await DisplayRequestError(callerViewModel, e);
                return false; // Return false in case of an exception
            }
            finally
            {
                AfterRequest(callerViewModel, Request_ID);
            }
        }

        private static void AfterRequest(BaseViewModel callerViewModel, string requestId)
        {
            NotBusy(callerViewModel);
            //remove or update the requestId in requestList here
        }

        private static Task PreRequest(BaseViewModel callerViewModel, bool isBusy = true)
        {
            // Add logic for pre-request actions
            return Task.CompletedTask;
        }

        private static bool NotBusy(BaseViewModel callerViewModel)
        {
            foreach (var entry in requestList)
            {
                if (entry.Value < 0)
                    return false;
            }

            if (callerViewModel != null)
                callerViewModel.IsBusy = false;

            return true;
        }

        private static async Task DisplayRequestError(BaseViewModel callerViewModel, Exception e)
        {
            if (callerViewModel?.ContentPage != null)
            {
                await callerViewModel.ContentPage.DisplayAlert("Request Error", e.Message, "Okay");
            }
        }
    }

}
