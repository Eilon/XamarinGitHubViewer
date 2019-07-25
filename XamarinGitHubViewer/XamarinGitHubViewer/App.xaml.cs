using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinGitHubViewer.Services;
using XamarinGitHubViewer.Views;

namespace XamarinGitHubViewer
{
    public partial class App : Application
    {
        public static readonly string GitHubGraphQLUrl = "https://api.github.com/graphql";

        public App()
        {
            InitializeComponent();

            DependencyService.Register<GitHubClient>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
