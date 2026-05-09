using Microsoft.AspNetCore.Mvc;
using SearchService.Application.Abstractions.Services;

namespace SearchService.Api.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<IActionResult> Search(string keyword)
    {
        var result = await _searchService.Search(keyword);

        return Ok(result);
    }
}