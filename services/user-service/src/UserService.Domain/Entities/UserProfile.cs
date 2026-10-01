using BuildingBlocks.Domain.Entities;

namespace UserService.Domain.Entities;

public class UserProfile : BaseEntity
{
    /// <summary>
    /// Tên đầy đủ của người dùng
    /// </summary>
    public string FullName { get; set; } = default!;
    /// <summary>
    /// Tên hiển thị của người dùng
    /// </summary>
    public string? DisplayName { get; set; }
    /// <summary>
    /// Ngày sinh của người dùng
    /// </summary>
    public DateTime? DOB { get; set; }
    /// <summary>
    /// Địa chỉ của người dùng
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// Ảnh đại diện của người dùng
    /// </summary>
    public Guid? Avatar { get; set; }
    /// <summary>
    /// Ảnh bìa của người dùng
    /// </summary>    
    public Guid? CoverImage { get; set; }
    /// <summary>
    /// Thời gian chỉnh sửa hồ sơ gần nhất
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
