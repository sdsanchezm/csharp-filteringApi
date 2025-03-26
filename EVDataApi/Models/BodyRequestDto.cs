using System.ComponentModel.DataAnnotations.Schema;

namespace EVDataApi.Models
{
    public class BodyRequestDto
    {
        [Column("County")]
        public string? County { get; set; }

        [Column("City")]
        public string? City { get; set; }
        [Column("State")]
        public string? State { get; set; }
        [Column("Make")]
        public string? Make { get; set; }
        [Column("Model")]
        public string? Model { get; set; }
    }
}
