using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADSWEBAPP_API.Models
{
    [Table("ADS_LOGGING")]
    public class ADS_LOGGING_MODEL
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("MACHINENAME")]
        public string? User { get; set; }

        [Column("LOGGED")]
        public DateTime logged { get; set; }

        [Column("LEVELS")]
        public string? level { get; set; }

        [Column("MESSAGE")]
        public string? meassage { get; set; }

        [Column("LOGGER")]
        public string? logger { get; set; }

        [Column("CALLSITE")]
        public string? callsite { get; set; }

        [Column("EXCEPTION")]
        public string? exception { get; set; }
    }
}
