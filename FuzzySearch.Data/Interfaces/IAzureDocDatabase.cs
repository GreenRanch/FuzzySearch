using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using FuzzySearch.Core.Models;

public interface IAzureDocDatabase
{
    DocumentClient Client { get; }

    List<AccountModel> GetAccounts();
}