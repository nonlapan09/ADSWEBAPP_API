using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADSWEBAPP_API.Dto.AuthenData
{
    [Table("ADS_ACCOUNT")]
    public class MasterAuthentication
    {
        [Key]
        [Column("TBL_ID")]
        public long TblId { get; set; }

        [Column("USERNAME")]
        public string? Username { get; set; }

        [Column("PASSWORD")]
        public string? Password { get; set; }

        /*[Column("DESCRIPTION")]
        public string Description { get; set; }*/

        [Column("START_DATE")]
        public DateTime Startdate { get; set; }

        [Column("EXPIRE_DATE")]
        public DateTime ExeDate { get; set; }

        [Column("INACTIVE")]
        public int Inactive { get; set; }

        [Column("FISRTNAME")]
        public string? firstname { get; set; }
        [Column("LASTNAME")]
        public string? lastname { get; set; }
        [Column("EMAIL")]
        public string? email { get; set; }

    }
}
