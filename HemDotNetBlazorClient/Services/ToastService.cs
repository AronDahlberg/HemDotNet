using System.Timers;
using Timer = System.Timers.Timer;

namespace HemDotNetBlazorClient.Services
{
    public class ToastService : IToastService, IDisposable
    {
        public event Action<string, ToastLevel, int> OnShow;
        public event Action OnHide;
        private Timer _countdown;

        public void ShowToast(string message, ToastLevel level, int duration = 5000)
        {
            OnShow?.Invoke(message, level, duration);
            StartCountdown(duration);
        }

        public void ShowSuccess(string message, int duration = 3000)
        {
            ShowToast(message, ToastLevel.Success, duration);
        }

        public void ShowInfo(string message, int duration = 3000)
        {
            ShowToast(message, ToastLevel.Info, duration);
        }

        public void ShowWarning(string message, int duration = 3000)
        {
            ShowToast(message, ToastLevel.Warning, duration);
        }

        public void ShowError(string message, int duration = 5000)
        {
            ShowToast(message, ToastLevel.Error, duration);
        }

        private void StartCountdown(int duration)
        {
            SetCountdown(duration);
            _countdown.Start();
        }

        private void SetCountdown(int duration)
        {
            if (_countdown == null)
            {
                _countdown = new Timer(duration);
                _countdown.Elapsed += HideToast;
                _countdown.AutoReset = false;
            }
            else
            {
                _countdown.Interval = duration;
            }
        }

        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }

        public void Dispose()
        {
            _countdown?.Dispose();
        }
    }

    public enum ToastLevel
    {
        Info,
        Success,
        Warning,
        Error
    }
}