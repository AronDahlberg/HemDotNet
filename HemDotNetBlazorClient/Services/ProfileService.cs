using HemDotNetBlazorClient.Providers;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public class ProfileService
    {
        private string _currentUserProfilePicture;
        

        public event Action ProfilePictureChanged;

        public string CurrentUserProfilePicture
        {
            get => _currentUserProfilePicture;
            set
            {
                _currentUserProfilePicture = value;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => ProfilePictureChanged?.Invoke();


        public void UpdateProfilePicture(string newImageUrl)
        {
            CurrentUserProfilePicture = newImageUrl;
        }
    }
}
