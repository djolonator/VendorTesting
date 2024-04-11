using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using System;
using VendorTesting.Models;
using static VendorTesting.Models.Models;

namespace VendorTesting.Service
{
    public class ValidateRefferalLink
    {
        private readonly InstitutionService _institutionService;
        public ValidateRefferalLink(InstitutionService institutionService) 
        {
            _institutionService = institutionService;
        }

        public async Task<Test> Execute(Test test)
        {
            GetReferalLink(test);
            Validations.Validations.ValidateReferralLinkExists(ref test);
            await GetResponsesFromReferalURL(test);
            Validations.Validations.ValidateReferralLinkReposnseExists(ref test);
            Validations.Validations.ValidateReferralLinkReponseIs200(ref test);
            Validations.Validations.ValidateReferralLinkReposnseHasCorrectContentType(ref test);
            
            return test;
        }

        private Test GetReferalLink(Test test)
        {
            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 100
            };
            Parallel.ForEach(test.TestPassed, parallelOptions, (casee, token) =>
            {
                var vendorUrl = casee.Institution.ProviderBaseAddress;

                var nonEmptyReponses = casee.TestJsonSucessContent!.PatientCases
                                .SelectMany(caseRecord => caseRecord.Requests)
                                .SelectMany(request => request.Responses)
                                .ToList();

                var referralLinkFromCase = nonEmptyReponses.Where(r => r.ReferralResponseUrl != null)?.FirstOrDefault()?.ReferralResponseUrl;

                string completeReferralLink = null;

                if (!string.IsNullOrEmpty(referralLinkFromCase))
                {
                    if (referralLinkFromCase!.ToLower().StartsWith("ht"))
                    {
                        completeReferralLink = referralLinkFromCase;
                    }
                    else if (referralLinkFromCase.ToLower().StartsWith("/"))
                    {
                        completeReferralLink = string.Join("/", vendorUrl.Split('/').Take(3)) + referralLinkFromCase;
                    }
                    else
                    {
                        completeReferralLink = string.Join("/", vendorUrl.Split('/').Take(3)) + "/" + referralLinkFromCase;
                    }
                }
               
                casee.CompleteReferralLinkValue = completeReferralLink;

            });

            return test;

        }

        private async Task<Test> GetResponsesFromReferalURL(Test test)
        {
            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 100
            };

            await Parallel.ForEachAsync(test.TestPassed, parallelOptions, async (casee, token) =>
            {
                casee.ReferralLinkResponse = await _institutionService.CallInstitutionReferalLink(casee);
            });

            return test;
            
        }
    }
}
