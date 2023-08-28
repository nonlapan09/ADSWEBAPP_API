using System.Runtime.Serialization;

namespace ADSWEBAPP_API.Dto
{
    public class ResponseAddressData
    {
        [DataMember]
        public long Seq { get; set; }

        [DataMember]
        public long? ThpId { get; set; }

        [DataMember]
        public string? Hno { get; set; }

        [DataMember]
        public string? Village { get; set; }

        [DataMember]
        public string? Lane { get; set; }

        [DataMember]
        public string? Road { get; set; }

        [DataMember]
        public string? Alley { get; set; }

        [DataMember]
        public string? SubDistrict { get; set; }

        [DataMember]
        public string? District { get; set; }

        [DataMember]
        public string? Province { get; set; }

        [DataMember]
        public string? Postcode { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class ResponsePostCode
    {
        [DataMember]
        public long Seq { get; set; }

        [DataMember]
        public string Province { get; set; } = string.Empty;

        [DataMember]
        public string District { get; set; } = string.Empty;

        [DataMember]
        public string SubDistrict { get; set; } = string.Empty;
    }

    public class ResponseDataGetByCCAATT
    {
        [DataMember]
        public long Seq { get; set; }

        [DataMember]
        public string? Hno { get; set; }
        [DataMember]
        public string? Village { get; set; }
        [DataMember]
        public string? Road { get; set; }
        [DataMember]
        public string? Lane { get; set; }
        [DataMember]
        public string? Alley { get; set; }
    }

}
