using System.Collections.Concurrent;
using static VendorTesting.Models.Models;
namespace VendorTesting.Validations
{
    public static class Validations
    {

        //public static void ValidateCaseExists(ref Test testCases)
        //{
        //    var failedTests = new List<CaseModel>();

        //    testCases.TestPassed.ForEach(casee =>
        //    {
        //        if (casee.Patient == null)
        //        {
        //            casee.TestFailed = TestNamesConstants.VendorCaseIsFound;
        //            failedTests.Add(casee);
        //        }
        //    });

        //    testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
        //    testCases.TestFailed.AddRange(failedTests);

        //}

        public static void ValidateCaseExists(ref Test testCases)
        {

            var failedTests = new ConcurrentBag<CaseModel>();

            Parallel.ForEach(testCases.TestPassed, GetParalellOptions(), (casee, token) =>
               {
                   if (casee.Patient == null)
                   {
                       casee.TestFailed = TestNamesConstants.VendorCaseIsFound;
                       failedTests.Add(casee);
                   }
               });
            testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
            testCases.TestFailed.AddRange(failedTests.ToList());

        }

        public static void ValidateVendorResponseExists(ref Test testCases)
        {

            var failedTests = new ConcurrentBag<CaseModel>();

            Parallel.ForEach(testCases.TestPassed, GetParalellOptions(), (casee, token) =>
             {
                 if (casee.VendorResponse == null)
                 {
                     casee.TestFailed = TestNamesConstants.VendorResponseExists;
                     failedTests.Add(casee);
                 }
             });
            testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
            testCases.TestFailed.AddRange(failedTests.ToList());

        }

        public static void ValidateVendorResponseIs200(ref Test testCases)
        {
            var failedTests = new ConcurrentBag<CaseModel>();

            Parallel.ForEach(testCases.TestPassed, GetParalellOptions(), (casee, token) =>
            {
                if ((int)casee.VendorResponse!.StatusCode != 200)
                {
                    casee.TestFailed = TestNamesConstants.VendorResponseIsValid;
                    failedTests.Add(casee);
                }

            });
            testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
            testCases.TestFailed.AddRange(failedTests.ToList());

        }

        public static void ValidateVendorResponseHasContent(ref Test testCases)
        {
            var failedTests = new ConcurrentBag<CaseModel>();


            Parallel.ForEach(testCases.TestPassed, GetParalellOptions(), (casee, token) =>
            {
                if (casee.VendorResponseContent == string.Empty)
                {
                    casee.TestFailed = TestNamesConstants.VendorResponseHasContent;
                    failedTests.Add(casee);
                }
            });
            testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
            testCases.TestFailed.AddRange(failedTests.ToList());
        }

        public static void ValidateTestJsonResponseIs200(ref Test testCases)
        {
            var failedTests = new ConcurrentBag<CaseModel>();


            Parallel.ForEach(testCases.TestPassed, GetParalellOptions(), (casee, token) =>
            {
                if (!casee.TestJsonResponse.IsSuccessStatusCode)
                {
                    casee.TestFailed = TestNamesConstants.TestJsonResponseIsValid;
                    failedTests.Add(casee);
                }
            });
            testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
            var passTests = testCases.TestPassed;
            var failedTestsList = failedTests.ToList();

            Utility.ExtractErrorMessageFromTestJsonResponse(ref failedTestsList);
            Utility.ExtractCaseFromSuccessTestJsonResponse(ref passTests);

            testCases.TestPassed = passTests;
            testCases.TestFailed.AddRange(failedTestsList);
        }

        public static void ValidateReferralLinkExists(ref Test testCases)
        {
            var failedTests = new ConcurrentBag<CaseModel>();


            Parallel.ForEach(testCases.TestPassed, GetParalellOptions(), (casee, token) =>
            {
                if (casee.CompleteReferralLinkValue == null)
                {
                    casee.TestFailed = TestNamesConstants.ReferrallinkIsFound;
                    failedTests.Add(casee);
                }
            });
            testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
            testCases.TestFailed.AddRange(failedTests.ToList());
        }

        public static void ValidateReferralLinkReposnseExists(ref Test testCases)
        {
            var failedTests = new ConcurrentBag<CaseModel>();


            Parallel.ForEach(testCases.TestPassed, GetParalellOptions(), (casee, token) =>
            {
                if (casee.ReferralLinkResponse == null)
                {
                    casee.TestFailed = TestNamesConstants.ReferalLinkResponseExists;
                    failedTests.Add(casee);
                }
            });
            testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
            testCases.TestFailed.AddRange(failedTests.ToList());
        }

        public static void ValidateReferralLinkReponseIs200(ref Test testCases)
        {
            var failedTests = new ConcurrentBag<CaseModel>();


            Parallel.ForEach(testCases.TestPassed, GetParalellOptions(), (casee, token) =>
            {
                if ((int)casee.ReferralLinkResponse!.StatusCode != 200)
                {
                    casee.TestFailed = TestNamesConstants.ReferalLinkResponseIsValid;
                    failedTests.Add(casee);
                }
            });
            testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
            testCases.TestFailed.AddRange(failedTests.ToList());
        }

        public static void ValidateReferralLinkReposnseHasCorrectContentType(ref Test testCases)
        {
            var failedTests = new ConcurrentBag<CaseModel>();


            Parallel.ForEach(testCases.TestPassed, GetParalellOptions(), (casee, token) =>
            {
                if (casee.ReferralLinkResponse!.Content.Headers.ContentType == null || 
                    !casee.ReferralLinkResponse!.Content!.Headers!.ContentType!.MediaType!.ToLower().Contains("application/pdf"))
                {
                    casee.TestFailed = TestNamesConstants.ReferalLinkResponseContenttypeIsValid;
                    failedTests.Add(casee);
                }
            });
            testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
            testCases.TestFailed.AddRange(failedTests.ToList());
        }

        //public static void ValidateVendorCase(ref Test testCases)
        //{
        //    var failedTests = new ConcurrentBag<CaseModel>();

        //    Parallel.ForEach(testCases.TestPassed, GetParalellOptions(), (casee, token) =>
        //    {
        //        var patientCases = casee.TestJsonSucessContent!.PatientCases;

        //        if (!ValidatePatientCasesIsNotEmpty(patientCases))
        //            casee.TestFailed = TestNamesConstants.PatientCasesExists;
        //        else
        //        {
        //            var randomCase = GetRandomCaseFromList(patientCases);
        //            //if (!ValidatePatientCaseHasRequests(randomCase))
        //            //    casee.TestFailed = TestNamesConstants.PatientCasesExistsRequestsExists;
        //            //else if (!ValidatePatientCasesHasRequestsHasReposnses(randomCase))
        //            //    casee.TestFailed = TestNamesConstants.PatientCasesRequestsResponsesExists;
        //            //else if (!ValidatePatientCasesHasRequestsHasReposnsesIsNotEmpty(randomCase))
        //            //    casee.TestFailed = TestNamesConstants.PatientCasesRequestsResponsesIsNotEmpty;
        //        }
        //    });
        //    testCases.TestPassed.RemoveAll(c => c.TestFailed != null);
        //    testCases.TestFailed.AddRange(failedTests.ToList());
        //}

        //private static bool ValidatePatientCasesHasRequestsHasReposnsesIsNotEmpty(PatientCaseRecordsVM randomCase)
        //{
        //    bool isValid = false;

        //    if (randomCase.Requests.FirstOrDefault()!.Responses.Count > 0)
        //        isValid = true;

        //    return isValid;
        //}

        //private static bool ValidatePatientCasesHasRequestsHasReposnses(PatientCaseRecordsVM randomCase)
        //{
        //    bool isValid = false;

        //    if (randomCase.Requests.FirstOrDefault()!.Responses != null)
        //        isValid = true;

        //    return isValid;
        //}

        //private static bool ValidatePatientCaseHasRequests(PatientCaseRecordsVM randomCase)
        //{
        //    bool isValid = false;

        //    if (randomCase.Requests != null && randomCase.Requests.Count > 0)
        //        isValid = true;

        //    return isValid;
        //}

        //private static PatientCaseRecordsVM GetRandomCaseFromList(List<PatientCaseRecordsVM> patientCases)
        //{
        //    Random random = new Random();
        //    int index = random.Next(patientCases.Count);
        //    var oneCase = patientCases[index];

        //    return oneCase;

        //}

        //private static bool ValidatePatientCasesIsNotEmpty(List<PatientCaseRecordsVM> patientCases)
        //{
        //    bool isValid = false;

        //    if (patientCases != null && patientCases.Count > 0)
        //        isValid = true;

        //    return isValid;
        //}

        private static ParallelOptions GetParalellOptions()
        {
            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 50
            };

            return parallelOptions;
        }
    }
}
