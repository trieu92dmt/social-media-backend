using BuildingBlocks.Domain.Entities;

namespace PostService.Domain.Entities;

public class PostMedia : BaseEntity
{
    public Guid PostId { get; set; }
    public Guid MediaId { get; set; }
    public int SortOrder { get; set; }
}
