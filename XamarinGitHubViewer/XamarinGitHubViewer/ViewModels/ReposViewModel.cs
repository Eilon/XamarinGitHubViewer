using System;
using System.Diagnostics;
using System.Linq;
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
        private RepositoryEdge _selectedRepo;

        public bool ReposLoaded { get; private set; }
        public VirtualReadOnlyList<RepositoryEdge> Repos { get; }
        public Command LoadReposCommand { get; set; }
        public Command GetMoreReposCommand { get; set; }

        public ReposViewModel()
        {
            Title = "Browse";
            Repos = new VirtualReadOnlyList<RepositoryEdge>();
            Repos.NeedMoreItems += Repos_NeedMoreItems;
            LoadReposCommand = new Command(async () => await ExecuteLoadReposCommand());
            GetMoreReposCommand = new Command(async (context) => await ExecuteGetMoreReposCommand((RepositoryEdge)context));

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        private async void Repos_NeedMoreItems(object sender, NeedMoreItemsEventArgs<RepositoryEdge> e)
        {
            await ExecuteGetMoreReposCommand(e.LastItem);
        }

        public Command SelectionChangedCommand => new Command(ItemSelectionChanged);

        public RepositoryEdge SelectedRepo
        {
            get => _selectedRepo;
            set
            {
                if (_selectedRepo != value)
                {
                    _selectedRepo = value;
                    OnPropertyChanged();
                }
            }
        }

        void ItemSelectionChanged()
        {
            Debug.WriteLine($"Selected item: " + (SelectedRepo?.Node.Name ?? "<null>"));

            // De-select the item
            SelectedRepo = null;
        }

        async Task ExecuteGetMoreReposCommand(RepositoryEdge repo)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                // TODO: Check here if the data is not available in settings
                var token = await SecureStorage.GetAsync("Token");

                var cursorOfOldLastItem = repo.Cursor;

                var items = await new GitHubClient(token).GetRepositories("xamarin", cursorOfOldLastItem);
                foreach (var item in items)
                {
                    Repos.Add(item);
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

                // TODO: Check here if the data is not available in settings
                var token = await SecureStorage.GetAsync("Token");

                var items = await new GitHubClient(token).GetRepositories("xamarin");
                foreach (var item in items)
                {
                    Repos.Add(item);
                }
                ReposLoaded = true;
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
