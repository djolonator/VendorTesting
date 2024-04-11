using MongoDB.Bson.Serialization.Attributes;


namespace VendorTesting.Models
{
    public class VendorCaseModel
    {
        public class PatientCaseRecordsWrapperWithoutPatient
        {
            public List<PatientCaseRecordsVM> PatientCases { get; set; }
            public string Message { get; set; }
        }

        public class PatientCaseRecordResponse
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

        public class PatientCaseRecordRequest
        {

            public string AdmissionCabinetName { get; set; }
            public string AdmissionCabinetCode { get; set; }
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

            public List<PatientCaseRecordResponse> Responses { get; set; }


        }

        public class PatientCaseRecordsVM
        {
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime CaseDate { get; set; }
            public string CaseStatus { get; set; }
            public string CaseInstitution { get; set; }
            public int? InstitutionId { get; set; }
            public string InstitutionCode { get; set; }
            public string CaseNumber { get; set; }
            public string CaseID { get; set; }
            public List<PatientCaseRecordRequest> Requests { get; set; }
        }
    }
}
