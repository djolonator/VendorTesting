using MongoDB.Bson.Serialization.Attributes;

namespace VendorTesting.Models
{
    [BsonIgnoreExtraElements]
    public class PatientCasesV3
    {
        public string PatientNpi { get; set; }
        public string PatientSsn { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CaseDate { get; set; }
        public string CaseStatus { get; set; }
        public string CaseInstitution { get; set; }
        public int InstitutionId { get; set; }
        public string InstitutionCode { get; set; }
        public string CaseNumber { get; set; }
        public string CaseID { get; set; }
        public Request Request { get; set; }
    }

    public class Request
    {

        public string AdmissionCabinetName { get; set; }
        public string AdmissionCabinetCode { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime AdmissionDate { get; set; }
        public string AdmissionDoctor { get; set; }
        public string ReferralID { get; set; }
        public string AdmissionID { get; set; }
        public string ReferralTypeID { get; set; }
        public string ReferralType { get; set; }
        public string ReferralStatus { get; set; }
        public int ReferralStatusID { get; set; }
        public string ReferralStatusLocalID { get; set; }
        public string AdmissionProceduresSerialized { get; set; }
        public string ReferralImageUrl { get; set; }

        public Response Response { get; set; }
    }

    public class Response
    {
        public string ReferralResponseTypeName { get; set; }
        public string ReferralResponseStatusCode { get; set; }
        public string ReferralResponseStatusName { get; set; }
        public int ReferralResponseStatusID { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ReferralResponseDate { get; set; }
        public string ReferralResponseID { get; set; }
        public string ReferralID { get; set; }
        public string ReferralResponseUrl { get; set; }
    }
}
