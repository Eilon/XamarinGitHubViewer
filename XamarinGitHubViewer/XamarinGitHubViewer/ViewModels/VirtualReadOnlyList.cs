using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace XamarinGitHubViewer.ViewModels
{
    public class VirtualReadOnlyList<T> : IReadOnlyList<T>, INotifyCollectionChanged, INotifyPropertyChanged, IList
    {
        public VirtualReadOnlyList()
        {
            InnerCollection = new ObservableCollection<T>();
        }

        private ObservableCollection<T> InnerCollection { get; }

        public T this[int index]
        {
            get
            {
                if (index == Count - 1)
                {
                    RaiseNeedMoreItems(InnerCollection[InnerCollection.Count - 1]);
                }
                return InnerCollection[index];
            }
        }

        public void Add(T item)
        {
            InnerCollection.Add(item);
        }

        public void Clear()
        {
            InnerCollection.Clear();
        }

        private void RaiseNeedMoreItems(T lastItem)
        {
            NeedMoreItems?.Invoke(this, new NeedMoreItemsEventArgs<T>(lastItem));
        }

        public event EventHandler<NeedMoreItemsEventArgs<T>> NeedMoreItems;

        public int Count => InnerCollection.Count;

        public bool IsFixedSize => false;

        public bool IsSynchronized => ((IList)InnerCollection).IsSynchronized;

        public object SyncRoot => ((IList)InnerCollection).SyncRoot;

        public bool IsReadOnly => true;

        object IList.this[int index]
        {
            get => this[index];
            set => ((IList)InnerCollection)[index] = value;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)InnerCollection).PropertyChanged += value;
            }
            remove
            {
                ((INotifyPropertyChanged)InnerCollection).PropertyChanged -= value;
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                ((INotifyCollectionChanged)InnerCollection).CollectionChanged += value;
            }
            remove
            {
                ((INotifyCollectionChanged)InnerCollection).CollectionChanged -= value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InnerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)InnerCollection).GetEnumerator();
        }

        public int Add(object value)
        {
            return ((IList)InnerCollection).Add(value);
        }

        public bool Contains(object value)
        {
            return ((IList)InnerCollection).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList)InnerCollection).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            ((IList)InnerCollection).Insert(index, value);
        }

        public void Remove(object value)
        {
            ((IList)InnerCollection).Remove(value);
        }

        public void CopyTo(Array array, int index)
        {
            ((IList)InnerCollection).CopyTo(array, index);
        }

        public void RemoveAt(int index)
        {
            ((IList)InnerCollection).RemoveAt(index);
        }
    }
}
