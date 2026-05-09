using Nest;
using SearchService.Application.Abstractions.Services;
using SearchService.Domain.Documents;

namespace SearchService.Infrastructure.Services;

public class ElasticsearchService : ISearchService
{
    private readonly ElasticClient _client;

    public ElasticsearchService(ElasticClient client)
    {
        _client = client;
    }

    public async Task IndexPost(PostDocument post)
    {
        await _client.IndexDocumentAsync(post);
    }

    public async Task<List<PostDocument>> Search(string keyword)
    {
        var response = await _client.SearchAsync<PostDocument>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Content)
                    .Query(keyword)
                )
            ));

        return response.Documents.ToList();
    }
}