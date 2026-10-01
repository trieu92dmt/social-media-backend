using BuildingBlocks.Domain.Entities;

namespace MediaService.Domain.Entities;

public class Media : BaseEntity
{
	public Guid StorageKey { get; set; }
	public string MediaType { get; set; } = string.Empty;
	public string FileName { get; set; } = string.Empty;
	public int FileSize { get; set; }
	public DateTime CreateTime { get; set; }
}
