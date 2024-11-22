using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebApp.Models;

[Table("RiddleCategoryExt")]
public class RiddleCategory
{
    [Key]
    [Column("id", TypeName = "varchar(255)")]
    public required string Id { get; set; }

    [Column("serial_num", TypeName = "smallint")]
    public int? SerialNum { get; set; } = 0;

    [Column("update_time", TypeName = "bigint")]
    public long? UpdateTime { get; set; } = 0;

    [Required]
    [Column("name", TypeName = "varchar(255)")]
    public required string Name { get; set; }

    [Column("description", TypeName = "varchar(255)")]
    public string? Description { get; set; }

    [Column("image", TypeName = "varchar(255)")]
    public string? Image { get; set; }

    [Required]
    [Column("start_num", TypeName = "int")]
    public int StartNum { get; set; }

    [Required]
    [Column("total_num", TypeName = "int")]
    public int TotalNum { get; set; }
}
