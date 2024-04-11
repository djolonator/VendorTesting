using System.Text;

namespace VendorTesting.Service
{
    public class TestJsonService
    {
        public TestJsonService() { }

        public async Task<HttpResponseMessage> CallTestJson(string jsonCase)
        {
            HttpResponseMessage response = null;
            try 
            {
                var stringContent = new StringContent(jsonCase, UnicodeEncoding.UTF8, "application/json");
                response = await TestJsonHttpClientManager.Instance.httpClient.PostAsync("", stringContent);

                if((int)response!.StatusCode != 200 && (int)response!.StatusCode != 400)
                {

                }
            }
            catch (Exception ex)
            {

            }

            return response;
        }
    }
}
