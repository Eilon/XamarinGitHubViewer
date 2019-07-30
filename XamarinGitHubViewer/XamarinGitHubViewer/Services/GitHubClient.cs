using GraphQL.Client;
using GraphQL.Common.Request;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinGitHubViewer.Models;

namespace XamarinGitHubViewer.Services
{
    public class GitHubClient
    {
        public GitHubClient(string token)
        {
            Token = token;
        }

        public string Token { get; }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<RepoListResult> GetRepositories(string orgName, string afterCursor = null)
        {
            if (IsConnected)
            {
                var recipeGraphQLRequest = new GraphQLRequest
                {
                    Query = @"
query($login:String!, $after:String)
{
  organization(login: $login) {
    repositories(first: 10, after:$after, orderBy: {field: NAME, direction: ASC}) {
      edges {
        cursor
        node {
          name
          url
          description
          stargazers {
            totalCount
          }
        }
      }
      totalCount
    }
  }
}
",
                };
                recipeGraphQLRequest.Variables =
                    new
                    {
                        Login = orgName,
                        After = afterCursor,
                    };

                // TODO: This type is probably thread safe, so consider having only 1 instance
                using (var graphQLClient = new GraphQLClient(App.GitHubGraphQLUrl))
                {
                    graphQLClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                    graphQLClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("XamarinGitHubViewer", VersionTracking.CurrentVersion));

                    var graphQLResponse = await graphQLClient.PostAsync(recipeGraphQLRequest);
                    var orgRepos = graphQLResponse.GetDataFieldAs<RepositoriesItem>("organization");

                    return new RepoListResult(orgRepos.Repositories.Edges, orgRepos.Repositories.TotalCount);
                }
            }

            return null;
        }
    }
}
