using XamarinGitHubViewer.ViewModels;

namespace XamarinGitHubViewer.ViewModels
{
    public class GitHubSettingsPageViewModel : BaseViewModel
    {
        private string _oAuthToken;

        public string OAuthToken
        {
            get => _oAuthToken;
            set => SetProperty(ref _oAuthToken, value);
        }
    }
}