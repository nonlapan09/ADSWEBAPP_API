using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSWEBAPP_API.Log
{
    [Table("TBL_LOGGING")]
    public class ADS_LOGGING_Model
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
