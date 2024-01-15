using WalletApp.ViewModels;

namespace WalletApp
{
    public static class Manager
    {
        private static SortedList<string, int> requestList = new();


        public static async Task<bool> POSTLoginAsync(BaseViewModel callerViewModel, string username, string password)
        {
            const string Request_ID = "Post-Login";
            await PreRequest(callerViewModel, Request_ID);
            try
            {
                return true;
            }
            catch (HttpRequestException e)
            {
                DisplayRequestError(callerViewModel, e);
            }
            finally
            {
                AfterRequest(callerViewModel, Request_ID);
            }
            return true;
        }
        private static void AfterRequest(BaseViewModel callerViewModel, string requestID)
        {
            requestList[requestID] = Math.Abs(requestList[requestID]);
            NotBusy(callerViewModel);
        }
        private static async Task PreRequest(BaseViewModel callerViewModel, string requestID, int delay = 0, bool isBusy = true)
        {
            await OrderRequest(requestID, delay);

            if (isBusy)
                try { callerViewModel.IsBusy = true; } catch { }

            await Task.Delay(delay);
        }

        private static bool NotBusy(BaseViewModel callerViewModel)
        {
            for (int i = 0; i < requestList.Count; i++)
            {
                string key = requestList.Keys[i];
                if (requestList.TryGetValue(key, out int value) && value < 0)
                    return false;
            }

            if (callerViewModel != null)
                callerViewModel.IsBusy = false;

            return true;
        }
        private static async Task<bool> OrderRequest(string requestName, int delay = 0)
        {
            if (requestList.TryGetValue(requestName, out int order))
            {
                if (requestList[requestName] > 0)
                {
                    order++;
                    requestList[requestName] = order;
                }
                else
                {
                    if (requestList[requestName] < 0)
                    {
                        order--;
                        requestList[requestName] = order;

                        int count = 0;
                        while (requestList[requestName] < 0 || requestList[requestName] != Math.Abs(order))
                        {
                            await Task.Delay(8);
                            if (count > 700)
                                return false;
                        }
                    }
                }
            }
            else
            {
                order = 1;
                requestList[requestName] = 1;
            }

            await Task.Delay(delay);

            if (requestList.TryGetValue(requestName, out int value) && value != Math.Abs(order))
                return false;

            requestList[requestName] *= -1;

            return true;
        }

        private static async void DisplayRequestError(BaseViewModel callerViewModel, Exception e)
        {
            await callerViewModel.ContentPage.DisplayAlert("Request Error", e.Message, "Okay");
        }


    }
}
