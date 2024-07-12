using System.Collections.Concurrent;
using static VendorTesting.Models.Models;
namespace VendorTesting.Validations
{
    public static class Validations
    {
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
