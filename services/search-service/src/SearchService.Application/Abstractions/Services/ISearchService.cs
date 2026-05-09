using SearchService.Domain.Documents;

namespace SearchService.Application.Abstractions.Services;

public interface ISearchService
{
    Task IndexPost(PostDocument post);

    Task<List<PostDocument>> Search(string keyword);
}