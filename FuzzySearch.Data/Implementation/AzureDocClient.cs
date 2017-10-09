using System;
using Microsoft.Azure.Documents.Client;
using FuzzySearch.Core.Models.Config;
using Microsoft.Extensions.Options;

namespace FuzzySearch.Data
{
    public class AzureDocClient : IAzureDocClient
    {
        private DocumentClient _client;
        private readonly DocumentDbConfig _documentDbConfig;

        public AzureDocClient(IOptions<DocumentDbConfig> documentDbConfig)
        {
            _documentDbConfig = documentDbConfig.Value;
            InitializeClient();
        }

        public DocumentClient Client => _client;

        #region Initialization Methods

        public void InitializeClient()
        {
            if (_client != null) return;
            _client = new DocumentClient(new Uri(_documentDbConfig.Endpoint), _documentDbConfig.Key);
        }

        #endregion
    }
}
