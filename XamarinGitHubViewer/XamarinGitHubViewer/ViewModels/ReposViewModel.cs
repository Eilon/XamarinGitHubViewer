using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

using XamarinGitHubViewer.Models;
using XamarinGitHubViewer.Services;
using XamarinGitHubViewer.Views;

namespace XamarinGitHubViewer.ViewModels
{
    public class ReposViewModel : BaseViewModel
    {
        public ObservableCollection<RepositoryNode> Repos { get; set; }
        public Command LoadReposCommand { get; set; }

        public ReposViewModel()
        {
            Title = "Browse";
            Repos = new ObservableCollection<RepositoryNode>();
            LoadReposCommand = new Command(async () => await ExecuteLoadReposCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadReposCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Repos.Clear();

                // TODO: Check here if the data is not available
                var token = await SecureStorage.GetAsync("Token");

                var items = await new GitHubClient(token).GetRepositories("xamarin");
                foreach (var item in items)
                {
                    Repos.Add(item.Node);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
