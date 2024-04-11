using System.Net.Http.Headers;


namespace VendorTesting
{
    public class TestJsonHttpClientManager
    {
        private static TestJsonHttpClientManager? _instance;
        private HttpClient _client;
        private static string url = "https://localhost:7187/TestJson";

        private TestJsonHttpClientManager() { }
        
        public static TestJsonHttpClientManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TestJsonHttpClientManager();
                }
                return _instance;
            }
        }

        public HttpClient httpClient
        {
            get { return _client; }
        }

        public void SetTestJsonHttpClient(string token)
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(102);

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            _client = client;
        }
    }
}
