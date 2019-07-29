using System;

namespace XamarinGitHubViewer.ViewModels
{
    public class NeedMoreItemsEventArgs<T> : EventArgs
    {
        public NeedMoreItemsEventArgs(T lastItem)
        {
            LastItem = lastItem;
        }

        public T LastItem { get; }
    }
}
