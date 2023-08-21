using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTube.Models;

public class Video
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    [Column(TypeName = "DateTime")]
    [Required]
    public DateTime UploadDate { get; set; }

    [Required]
    public Int16 Duration { get; set; }

    [Required]
    public byte AgeRating { get; set; } = 0;

    [StringLength(200)]
    public string Thumbnail { get; set; }

    [StringLength(200)]
    public string VideoFile { get; set; }

    [NotMapped]
    public string HourDuration { get {
        return TimeSpan.FromMinutes(Duration).ToString(@"%h'h 'mm'min'");
    } }

    [NotMapped]
    public string Classification { get {
        return AgeRating == 0 ? "Livre" : AgeRating + " anos";
    } }

    [NotMapped]
    public List<Tag> Tags { get; set; }
}