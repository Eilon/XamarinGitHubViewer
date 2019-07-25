using System;
using System.ComponentModel;
using Xamarin.Forms;
using XamarinGitHubViewer.Models;
using XamarinGitHubViewer.ViewModels;
using Xamarin.Essentials;
using XamarinGitHubViewer.Services;

namespace XamarinGitHubViewer.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ReposPage : ContentPage
    {
        ReposViewModel viewModel;

        public ReposPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ReposViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //var item = args.SelectedItem as Item;
            //if (item == null)
            //    return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ReposListView.SelectedItem = null;
        }

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Repos.Count == 0)
            {
                viewModel.LoadReposCommand.Execute(null);
            }
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new GitHubSettingsPage()));
        }

        private void ReposListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (e.ItemIndex == viewModel.Repos.Count - 1)
            {
                // if the last item is appearing, get more data
                var appearingItem = (RepositoryEdge)e.Item;

                viewModel.GetMoreReposCommand.Execute(appearingItem.Cursor);
            }
        }
    }
}
