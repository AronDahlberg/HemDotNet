namespace HemDotNetBlazorClient.Services
{
    public interface IToastService : IDisposable
    {
        event Action<string, ToastLevel, int> OnShow;
        event Action OnHide;
        void ShowToast(string message, ToastLevel level, int duration = 5000);
        void ShowSuccess(string message, int duration = 3000);
        void ShowInfo(string message, int duration = 3000);
        void ShowWarning(string message, int duration = 3000);
        void ShowError(string message, int duration = 5000);
    }
}
