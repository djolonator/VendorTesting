using System.Net.Http.Headers;

using static VendorTesting.Models.Models;

namespace VendorTesting.Service
{
    public  class InstitutionService
    {

        public async Task<HttpResponseMessage?> CallInstitution(CaseModel caseObj)
        {
            HttpResponseMessage? response = null;
            

            var value = caseObj.Patient!.PatientSsn ?? caseObj.Patient.PatientNpi;
            var type = caseObj.Patient.PatientSsn != null ? "LBO" : "NPI";

            string action = string.IsNullOrEmpty(caseObj.Institution.PatientCasesProviderAction) ? "api/PatientCases" : caseObj.Institution.PatientCasesProviderAction;
            string parameters = "?patientIdentificationValue=" + value + "&patientIdentificationType=" + type;

            if (caseObj.Institution.ProviderName.ToLower().StartsWith("zipsoft"))
                action = "api/patientcases";
            else if (caseObj.Institution.ProviderName.ToLower().StartsWith("sorsix"))
                parameters = parameters + "&token=" + "";

            if (caseObj.Institution.ProviderBaseAddress.EndsWith("/"))
                caseObj.Institution.ProviderBaseAddress = caseObj.Institution.ProviderBaseAddress.Remove(caseObj.Institution.ProviderBaseAddress.Length - 1, 1);

            if (action.StartsWith("/"))
                action = action.Remove(0, 1);

            var client = CreateHttpClient(TokenManager.Instance.Token!);
            client.BaseAddress = new Uri(caseObj.Institution.ProviderBaseAddress);

            try
            {
                using (client)
                {
                    response = await client.GetAsync("/" + action + parameters);
                }
            }

            catch (Exception ex)
            {

            }

            return response;

        }

        public async Task<HttpResponseMessage?> CallInstitutionReferalLink(CaseModel casee)
        {
            HttpResponseMessage? response = null;
            var url = casee.CompleteReferralLinkValue;

            bool isMojDoctor = false;

            if (url.ToLower().Contains("mojdoktor"))
            {
                url = url + "token=";
                isMojDoctor = true;
            } 


            var client = CreateHttpClient(TokenManager.Instance.Token!, !isMojDoctor, true);


            client.BaseAddress = new Uri(url);

            try
            {

                using (client)
                {
                    response = await client.GetAsync("");
                }
            }

            catch (Exception ex)
            {

            }

            return response;
        }


        private HttpClient CreateHttpClient(string token, bool withBearer = true, bool isForReferal = false)
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(102);

            client.DefaultRequestHeaders.Accept.Clear();

            if (!isForReferal)
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (withBearer) 
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }
    }
}
