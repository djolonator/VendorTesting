
using System.ComponentModel;
using static VendorTesting.Models.VendorCaseModel;

namespace VendorTesting.Models
{
    public class Models
    {
        public class InstitutionModel
        {
            public string InstitutionCode { get; set; }
            public string InstitutionName { get; set; }
            public string Provides { get; set; }
            public string ProviderBaseAddress { get; set; }
            public string ProviderName { get; set; }
            public string PatientCasesProviderAction { get; set; }
        }

        public class PatientModel
        {
            public string PatientNpi { get; set; }
            public string PatientSsn { get; set; }
        }

        public class CaseModel
        {
            public PatientModel? Patient { get; set; }
            public InstitutionModel Institution { get; set; }
            public HttpResponseMessage? VendorResponse  { get; set; }
            public string VendorResponseContent { get; set; }
            public HttpResponseMessage? TestJsonResponse { get; set; }
            public PatientCaseRecordsWrapperWithoutPatient? TestJsonSucessContent { get; set; }
            public TestJsonResponseModel TestJsonFailedResponseContent { get; set; }
            public string TestJsonErrorMessage { get; set; }
            public string? CompleteReferralLinkValue { get; set; }
            public HttpResponseMessage? ReferralLinkResponse { get; set; }
            public string TestFailed {  get; set; }
            public List<string> ErrorMessages { get; set; } = new List<string>();
        }

        public class Test
        {
            public List<CaseModel> TestPassed { get; set; } = new List<CaseModel>();
            public List<CaseModel> TestFailed { get; set; } = new List<CaseModel>();
        }

        public class TestResult
        {
            public List<ExcelModel> TestPassed { get; set; } = new List<ExcelModel>();
            public List<ExcelModel> TestFailed { get; set; } = new List<ExcelModel>();
        }

        public class ExcelModel
        {
            [Description("Naziv")]
            public string? InstitutionName { get; set; }

            [Description("Provajder")]
            public string? InstitutionProvider { get; set; }

            [Description("Kod")]
            public string? InstitutionCode { get; set; }


            [Description("URL poziva")]
            public string? InstitutionRequest { get; set; }


            [Description("Status kod odgovora")]
            public string? InstitutionResponseStatusCode { get; set; }


            [Description("Status kod TestJson odgovora")]
            public string? TestJsonStatusCode { get; set; }

            [Description("TestJson vracena greska")]
            public string? TestJsonErrorMessage { get; set; }

            [Description("Vrednost iz ReferralResponseUrl")]
            public string? ReferralResponseUrl { get; set; }

            [Description("Status kod odgovora referral link poziva")]
            public string? ReferralResponseStatusCode { get; set; }


            [Description("Test koji nije prosao")]
            public string? TestFailed { get; set; }

        }
    }
}
