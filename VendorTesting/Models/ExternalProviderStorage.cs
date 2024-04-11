using MongoDB.Bson.Serialization.Attributes;


namespace VendorTesting.Models
{
    [BsonIgnoreExtraElements]
    public class ExternalProviderStorage : DefaultModel
    {
        public int InstitutionId { get; set; }
        public string InstitutionCode { get; set; }
        public string InstitutionName { get; set; }
        public bool Active { get; set; }
        public int Timeout { get; set; }
        public string Provides { get; set; }
        public string ProviderBaseAddress { get; set; }
        public string ProviderName { get; set; }
        public string PatientCasesProviderAction { get; set; }
        public string IdentType { get; set; }
        public bool UsePushMethod { get; set; }

    }
}
