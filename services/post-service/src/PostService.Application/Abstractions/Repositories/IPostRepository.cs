using PostService.Domain.Entities;

namespace PostService.Application.Abstractions.Repositories;

public interface IPostRepository
{
    Task AddAsync(Post post);

    Task SaveChangesAsync();
}