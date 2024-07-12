using static VendorTesting.Models.Models;
using System.Collections.Concurrent;
using VendorTesting.DBConnections;

namespace VendorTesting.Service
{
    public class OutterService
    {
        private readonly ProvidersContext _providersConext;
        private readonly CasesContext _casesConext;
        private readonly InstitutionService _institutionService;
        public OutterService(ProvidersContext providersConext, CasesContext casesContext, InstitutionService institutionService)
        {
            _providersConext = providersConext;
            _casesConext = casesContext;
            _institutionService = institutionService;
        }

        public async Task<Test> Execute()
        {
            var test = new Test();
            var institutions = await GetProviders();
            var result = await GetCasesForEachInstitution(institutions);

            test.TestPassed = result;
            Validations.Validations.ValidateCaseExists(ref test);
            await GetResponsesFromInstitutions(test);
            Validations.Validations.ValidateVendorResponseExists(ref test);

            return test;

        }

        private async Task<List<InstitutionModel>> GetProviders()
        {
            var providers = await _providersConext.GetProviders();
            var institutions = new List<InstitutionModel>();

            providers.ForEach(p =>
            {
                institutions.
                Add(new InstitutionModel()
                {
                    InstitutionCode = p.InstitutionCode,
                    InstitutionName = p.InstitutionName,
                    Provides = p.Provides,
                    ProviderBaseAddress = p.ProviderBaseAddress,
                    ProviderName = p.ProviderName,
                    PatientCasesProviderAction = p.PatientCasesProviderAction
                });
            });

            //return institutions.Take(20).ToList();
            return institutions;
        }

        private async Task<List<CaseModel>> GetCasesForEachInstitution(List<InstitutionModel> institutions)
        {

            var institutionCases = new ConcurrentBag<CaseModel>();


            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 50
            };

            await Parallel.ForEachAsync(institutions, parallelOptions, async (provider, token) =>
            {
                PatientModel patient = null;

                var cases = await _casesConext.GetNumberOfCases(provider.InstitutionCode, 1);

                var oneCase = cases.FirstOrDefault();

                if (oneCase != null)
                {
                    patient = new PatientModel() { PatientNpi = oneCase.PatientNpi, PatientSsn = oneCase.PatientSsn };
                }

                var caseModel = new CaseModel() { Patient = patient, Institution = provider };

                institutionCases.Add(caseModel);

            });

            return institutionCases.ToList();
        }

        private async Task<Test> GetResponsesFromInstitutions(Test tests)
        {

            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 100
            };

            await Parallel.ForEachAsync(tests.TestPassed, parallelOptions, async (institution, token) =>
            {
                institution.VendorResponse = await _institutionService.CallInstitution(institution);
            });

            return tests;
        }

    }

}
