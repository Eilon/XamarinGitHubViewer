using GraphQL.Client;
using GraphQL.Common.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<IEnumerable<RepositoryNode>> GetRepositoryNames(string orgName)
        {
            if (IsConnected)
            {
                var recipeGraphQLRequest = new GraphQLRequest
                {
                    Query = @"
query($login:String!)
{
  organization(login: $login) {
    repositories(first: 5, orderBy: {field: NAME, direction: ASC}) {
      totalCount
      nodes {
        name
        description
      }
    }
  }
}
",
                };
                recipeGraphQLRequest.Variables = new { Login = orgName };

                var graphQLClient = new GraphQLClient(App.GitHubGraphQLUrl);
                graphQLClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                graphQLClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("hubbup.io", VersionTracking.CurrentVersion));

                var graphQLResponse = await graphQLClient.PostAsync(recipeGraphQLRequest);
                var orgRepos = graphQLResponse.GetDataFieldAs<RepositoriesItem>("organization");

                return orgRepos.Repositories.Nodes;
            }

            return null;
        }
    }
}
