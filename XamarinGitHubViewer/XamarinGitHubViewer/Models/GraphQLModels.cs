using System;
using System.Collections.Generic;

namespace XamarinGitHubViewer.Models
{
    public class RepositoryNode
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public StargazersNode Stargazers { get; set; }
    }

    public class StargazersNode
    {
        public int TotalCount { get; set; }
    }

    public class RepositoriesConnection
    {
        public IList<RepositoryEdge> Edges { get; set; }
        public int TotalCount { get; set; }
    }

    public class RepositoryEdge
    {
        public string Cursor { get; set; }
        public RepositoryNode Node { get; set; }
    }

    public class RepositoriesItem
    {
        public RepositoriesConnection Repositories { get; set; }
    }
}
