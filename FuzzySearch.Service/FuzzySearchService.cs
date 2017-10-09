using FuzzySearch.Core.Models;
using FuzzySearch.Core.Models.Config;
using Microsoft.Extensions.Options;
using System;

namespace FuzzySearch.Service
{
    public class FuzzySearchService : IFuzzySearchService
    {
        private IAzureDocDatabase _dbContext;
        private readonly DocumentDbConfig _documentDbConfig;
        private readonly IFuzzySearchHelper _searchHelper;

        public FuzzySearchService(IOptions<DocumentDbConfig> documentDbConfig, IAzureDocDatabase dbContext, IFuzzySearchHelper searchHelper )
        {
            _dbContext = dbContext ?? throw new ArgumentNullException($"{nameof(dbContext)} is null");

            _searchHelper = searchHelper ?? throw new ArgumentNullException($"{nameof(searchHelper)} is null");

             if(documentDbConfig == null) throw new ArgumentNullException($"{nameof(documentDbConfig)} is null");
            _documentDbConfig = documentDbConfig.Value;
        }

        public SearchResponseModel SearchAccount(SearchRequestModel request)
        {
            return new SearchResponseModel() {
                MatchingAccounts = _searchHelper.FindMatchingAccounts(request.Name, _dbContext.GetAccounts())
            };
        }
    }
}
