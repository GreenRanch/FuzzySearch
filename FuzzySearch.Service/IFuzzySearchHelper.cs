using FuzzySearch.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzySearch.Service
{
    public interface IFuzzySearchHelper
    {
        IEnumerable<AccountModel> FindMatchingAccounts(string sourceAccountName, IEnumerable<AccountModel> accounts);
    }
}
