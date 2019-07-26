using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin.Forms
{
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class ParameterAttribute : Attribute
    {
        public ParameterAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}

namespace XamarinGitHubViewer.Controls
{
    [DesignTimeVisible(true)]
    public class InfiniteListView : ListView
    {
        public InfiniteListView()
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
            PropertyChanged += InfiniteListView_PropertyChanged;
        }

        public InfiniteListView([Parameter("CachingStrategy")] ListViewCachingStrategy cachingStrategy)
    : base(cachingStrategy)
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
            PropertyChanged += InfiniteListView_PropertyChanged;
        }


        private void InfiniteListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ItemsSourceProperty.PropertyName)
            {
                Max = (ItemsSource as ICollection)?.Count;

                if (ItemsSource is INotifyCollectionChanged collectionChanged)
                {
                    // TODO: Also need to un-hook event handler
                    collectionChanged.CollectionChanged += CollectionChanged_CollectionChanged;
                }
            }
        }

        private void CollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Max = (ItemsSource as ICollection)?.Count;
        }

        public int? Max { get; set; }

        /// <summary>
        /// Backing store for the GetMoreDataCommand property.
        /// </summary>
        public static readonly BindableProperty GetMoreDataCommandProperty = BindableProperty.Create(nameof(GetMoreDataCommand), typeof(ICommand), typeof(InfiniteListView), null, BindingMode.OneWay, null, OnGetMoreDataCommandChanged);

        /// <summary>
        /// Gets or sets the command that is run when the list view enters the get-more-data state.
        /// </summary>
        public ICommand GetMoreDataCommand
        {
            get
            {
                return (ICommand)GetValue(GetMoreDataCommandProperty);
            }
            set
            {
                SetValue(GetMoreDataCommandProperty, value);
            }
        }

        private static void OnGetMoreDataCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var obj = (InfiniteListView)bindable;
            var oldCommand = (ICommand)oldValue;
            var newCommand = (ICommand)newValue;
            obj.OnGetMoreDataCommandChanged(oldCommand, newCommand);
        }

        private void OnGetMoreDataCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            // TODO: Make this work

            //if (oldCommand != null)
            //{
            //    oldCommand.CanExecuteChanged -= OnCommandCanExecuteChanged;
            //}
            //if (newCommand != null)
            //{
            //    newCommand.CanExecuteChanged += OnCommandCanExecuteChanged;
            //    RefreshAllowed = newCommand.CanExecute(null);
            //}
            //else
            //{
            //    RefreshAllowed = true;
            //}
        }

        private void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (e.ItemIndex == Max - 1)
            {
                if (GetMoreDataCommand?.CanExecute(parameter: e.Item) ?? false)
                {
                    GetMoreDataCommand?.Execute(parameter: e.Item);
                }
            }
        }
    }
}
