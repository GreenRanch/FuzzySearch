using Microsoft.AspNetCore.Mvc;
using FuzzySearch.Core.Models;

namespace FuzzySearch.Service
{
  [Route("api/[controller]")]
  public class SearchController : Controller
  {
    private IFuzzySearchService _fuzzySearchService;

    public SearchController(IFuzzySearchService fuzzySearchService)
    {
      _fuzzySearchService = fuzzySearchService;
    }

    [Route("{name}")]
    [HttpGet]
    public IActionResult Get(string name)
    {
      var result = _fuzzySearchService.SearchAccount(new SearchRequestModel() { Name = name });

      return new JsonResult(result.MatchingAccounts);
    }
  }
}
