using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzySearch.Core.Models
{
    public class SearchResponseModel
    {
        public IEnumerable<AccountModel> MatchingAccounts;
    }
}
