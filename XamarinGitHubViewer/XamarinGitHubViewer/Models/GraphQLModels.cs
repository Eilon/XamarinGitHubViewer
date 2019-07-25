using System;
using System.Collections.Generic;

namespace XamarinGitHubViewer.Models
{
    public class RepositoryNode
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }

    public class RepositoriesConnection
    {
        public int TotalCount { get; set; }
        public IList<RepositoryNode> Nodes { get; set; }
    }

    public class RepositoriesItem
    {
        public RepositoriesConnection Repositories { get; set; }
    }

    public class OrganizationsQueryResult
    {
        public RepositoriesItem Organization { get; set; }
    }
}
