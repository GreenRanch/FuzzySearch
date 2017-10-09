using System;
using FuzzySearch.Core.Models;
using System.Threading.Tasks;

namespace FuzzySearch.Service
{
    public interface IFuzzySearchService
    {
        SearchResponseModel SearchAccount(SearchRequestModel request);
    }
}
