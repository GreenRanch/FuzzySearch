using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using FuzzySearch.Core.Models.Config;
using Microsoft.Extensions.Options;
using FuzzySearch.Core.Models;

namespace FuzzySearch.Data
{
    public class AzureDocDatabase : IAzureDocDatabase
    {
        private DocumentClient _docClient;
        private readonly DocumentDbConfig _documentDbConfig;

        public AzureDocDatabase(IOptions<DocumentDbConfig> documentDbConfig)
        {
            _documentDbConfig = documentDbConfig.Value;
        }

        #region IAzureDocDatabase Methods

        public DocumentClient Client
        {
            get
            {
                if(_docClient == null)
                    _docClient = new DocumentClient(new Uri(_documentDbConfig.Endpoint), _documentDbConfig.Key);

                return _docClient;
            }
        }

        public List<AccountModel> GetAccounts()
        {
            var database = (Client.ReadDatabaseFeedAsync()).
                Result.Single(d => d.Id == _documentDbConfig.DatabaseName);

            var collection = (Client.ReadDocumentCollectionFeedAsync(database.CollectionsLink)).
                Result.Single(c => c.Id == _documentDbConfig.CollectionName);

            return Client.CreateDocumentQuery<AccountModel>(collection.DocumentsLink).
                ToList();
        }

        #endregion

    }
}
