using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinGitHubViewer.ViewModels;

namespace XamarinGitHubViewer.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GitHubSettingsPage : ContentPage
    {
        private GitHubSettingsPageViewModel _viewModel;

        public GitHubSettingsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new GitHubSettingsPageViewModel();

            _viewModel.Title = "GitHub Settings";
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var token = _viewModel.OAuthToken;

            await SecureStorage.SetAsync("Token", token);

            await Navigation.PopModalAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}