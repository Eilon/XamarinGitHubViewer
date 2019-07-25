﻿using System;
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
        public ObservableCollection<RepositoryEdge> Repos { get; set; }
        public Command LoadReposCommand { get; set; }
        public Command GetMoreReposCommand { get; set; }

        public ReposViewModel()
        {
            Title = "Browse";
            Repos = new ObservableCollection<RepositoryEdge>();
            LoadReposCommand = new Command(async () => await ExecuteLoadReposCommand());
            GetMoreReposCommand = new Command(async (context) => await ExecuteGetMoreReposCommand(afterCursor: (string)context));

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteGetMoreReposCommand(string afterCursor)
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

                var items = await new GitHubClient(token).GetRepositories("xamarin", afterCursor);
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
