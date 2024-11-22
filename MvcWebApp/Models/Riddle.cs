using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebApp.Models;

[Table("RiddleExt")]
public class Riddle
{
    [Key]
    [Column("id")]
    [MaxLength(255)]
    
    public required string Id { get; set; }

    [Required]
    [Column("serial_num")]
    public int SerialNum { get; set; }

    [Required]
    [Column("update_time")]
    public long UpdateTime { get; set; }

    [Required]
    [Column("mystery")]
    [MaxLength(512)]
    public required string Mystery { get; set; }

    [Required]
    [Column("mystery_pinyin")]
    [MaxLength(512)]
    public required string MysteryPinyin { get; set; }

    [Required]
    [Column("tips")]
    [MaxLength(64)]
    public required string Tips { get; set; }

    [Column("tips_pinyin")]
    [MaxLength(64)]
    public required string TipsPinyin { get; set; }

    [Required]
    [Column("answer")]
    [MaxLength(64)]
    public required string Answer { get; set; }

    [Column("answer_pinyin")]
    [MaxLength(64)]
    public required string AnswerPinyin { get; set; }

    [Column("analysis")]
    [MaxLength(255)]
    public required string Analysis { get; set; }

    [Required]
    [Column("category_id")]
    [MaxLength(255)]
    public required string CategoryId { get; set; }

    [Column("voice_url")]
    [MaxLength(255)]
    public required string VoiceUrl { get; set; }
}
