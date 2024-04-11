
using static VendorTesting.Models.Models;
using VendorTesting.Models;
using Newtonsoft.Json;
using static VendorTesting.Models.VendorCaseModel;

namespace VendorTesting
{
    public static class Utility
    {
        public static void ExtractErrorMessageFromTestJsonResponse(ref List<CaseModel> failedTests)
        {

            Parallel.ForEach(failedTests, GetParalellOptions(), (casee, token) =>
            {
                string responseString = null;
                var testJsonFailResult = new TestJsonResponseModel();
                try
                {
                    responseString = casee.TestJsonResponse.Content.ReadAsStringAsync().Result;
                    casee.TestJsonErrorMessage = responseString.Split("--->")[1];
                }
                catch (Exception ex)
                {

                }
            });
        }

        public static void ExtractCaseFromSuccessTestJsonResponse(ref List<CaseModel> passTests)
        {

            Parallel.ForEach(passTests, GetParalellOptions(), (casee, token) =>
            {
                var testJsonSuccerssResult = JsonConvert.DeserializeObject<PatientCaseRecordsWrapperWithoutPatient>(casee.TestJsonResponse.Content.ReadAsStringAsync().Result);
                casee.TestJsonSucessContent = testJsonSuccerssResult;
            });
        }

       

        private static ParallelOptions GetParalellOptions()
        {
            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 50
            };

            return parallelOptions;
        }

        public static TestResult ConvertTestToExcelModel(Test test)
        {
            var testResult = new TestResult();

            test.TestPassed.ForEach(test => {

                var excelModel = new ExcelModel()
                {
                    InstitutionName = test.Institution.InstitutionName,
                    InstitutionProvider = test.Institution.ProviderName,
                    InstitutionCode = test.Institution.InstitutionCode,
                    InstitutionRequest = test.VendorResponse.RequestMessage.RequestUri.AbsoluteUri,
                    InstitutionResponseStatusCode = test.VendorResponse.StatusCode.ToString(),
                    TestJsonStatusCode = test.TestJsonResponse.StatusCode.ToString(),
                    TestJsonErrorMessage = test.TestJsonErrorMessage,
                    ReferralResponseUrl = test.CompleteReferralLinkValue,
                    ReferralResponseStatusCode = test.ReferralLinkResponse.StatusCode.ToString(),
                    TestFailed = test.TestFailed
                };  

                testResult.TestPassed.Add(excelModel);

            });

            test.TestFailed.ForEach(test => {

                var excelModel = new ExcelModel()
                {
                    InstitutionName = test.Institution.InstitutionName,
                    InstitutionProvider = test.Institution.ProviderName,
                    InstitutionCode = test.Institution.InstitutionCode,
                    InstitutionRequest = test.VendorResponse?.RequestMessage!.RequestUri!.AbsoluteUri ?? null,
                    InstitutionResponseStatusCode = test.VendorResponse?.StatusCode.ToString() ?? null,
                    TestJsonStatusCode = test.TestJsonResponse?.StatusCode.ToString() ?? null,
                    TestJsonErrorMessage = test.TestJsonErrorMessage,
                    ReferralResponseUrl = test.CompleteReferralLinkValue,
                    ReferralResponseStatusCode = test.ReferralLinkResponse?.StatusCode.ToString() ?? null,
                    TestFailed = test.TestFailed
                };

                testResult.TestFailed.Add(excelModel);

            });

            return testResult;
        }
    }
}
