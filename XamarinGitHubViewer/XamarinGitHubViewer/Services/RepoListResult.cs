using System.Collections.Generic;
using XamarinGitHubViewer.Models;

namespace XamarinGitHubViewer.Services
{
    public class RepoListResult
    {
        public RepoListResult(IEnumerable<RepositoryEdge> repos, int totalCount)
        {
            Repos = repos;
            TotalCount = totalCount;
        }

        /// <summary>
        /// A page of repo items, depending on what was queried.
        /// </summary>
        public IEnumerable<RepositoryEdge> Repos { get; }

        /// <summary>
        /// The total repos available (possibly more than in the current result).
        /// </summary>
        public int TotalCount { get; }
    }
}
