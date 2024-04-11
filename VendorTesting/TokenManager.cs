using System.Globalization;
using System.Text;
using Newtonsoft.Json;


public class TokenManager
{
    private static TokenManager? _instance;
    private string? _token;
    private static string CLIENT_ID = "";
    private static string CLIENT_SECRET = "";
    private static string _edoktorApiServiceUrl = "https://oauth.vuz.rs/";

    private TokenManager() { }

    public static TokenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TokenManager();
            }
            return _instance;
        }
    }

    public string? Token
    {
        get { return _token; }
    }

    public async Task SetToken()
    {
        if (TokenIsGood())
        {
            _token = GetTokenFromStone();
        }
        else
        {
            _token = await GetTokenFromOAuth();
            SetTokenInStone(_token);
        }
    }

    private async Task<string> GetTokenFromOAuth()
    {
        string token = "";

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_edoktorApiServiceUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            StringContent content = new StringContent(
            JsonConvert.SerializeObject(CreateClientCredentials()),
            UTF8Encoding.UTF8,
            "application/json");
            try
            {
                var tokenResponse = await client.PostAsync("/api/OAuth/GetAccessToken", content);

                if (tokenResponse.IsSuccessStatusCode)
                {
                    var tokenString = await tokenResponse.Content.ReadAsStringAsync();
                    var tokenObj = JsonConvert.DeserializeObject<AccessTokenResponseModel>(tokenString);
                    token = tokenObj.AccessToken.ToString();
                }
                else
                {

                }
            }
            catch (Exception e)
            {

            }
        }

        return token;
    }

    private void SetTokenInStone(string token)
    {
        string solutionRoot = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(solutionRoot, "TokenTime.txt");
        string data = DateTime.Now.ToString() + "plus" + token;

        File.WriteAllText(filePath, data);

    }
    private bool TokenIsGood()
    {
        bool tokenIsGood = false;
        string solutionRoot = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(solutionRoot, "TokenTime.txt");
        if (!File.Exists(filePath))
            File.Create(filePath);

        try
        {
            string data = File.ReadAllText(filePath);
            var dateString = data.Split("plus")[0];
            var timeTokenSet = DateTime.Parse(dateString);
            TimeSpan difference = DateTime.Now - timeTokenSet;

            if (difference.TotalHours <= 24)
                tokenIsGood = true;
        }
        catch (Exception ex)
        {

        }

        return tokenIsGood;
    }

    private string GetTokenFromStone()
    {
        string solutionRoot = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(solutionRoot, "TokenTime.txt");
        string data = File.ReadAllText(filePath);

        var token = data.Split("plus")[1];

        return token;
    }

    private Credentials CreateClientCredentials()
    {
        return new Credentials()
        {
            ClientID = CLIENT_ID,
            ClientSecret = CLIENT_SECRET
        };
    }
}

public class Credentials
{
    public string ClientID { get; set; }
    public string ClientSecret { get; set; }
}

public class TokenValidation
{
    [JsonProperty("valid")]
    public bool Valid { get; set; }
    [JsonProperty("message")]
    public string Message { get; set; }
    [JsonProperty("user")]
    public string User { get; set; }
}

public class AccessTokenResponseModel
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    [JsonProperty("token_type")]
    public string TokenType { get; set; }
    [JsonProperty("expires_in")]
    public string ExpiresIn { get; set; }
    [JsonProperty(".expires")]
    public string ExpiresAt { get; set; }
    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }
    [JsonIgnore]
    public string ObtainedAt { get; set; }

    public bool IsValid()
    {
        DateTime obtainedAt = DateTime.ParseExact(this.ObtainedAt, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        DateTime now = DateTime.Now;
        if (obtainedAt.AddSeconds(Convert.ToInt32(this.ExpiresIn)) > now.AddMinutes(1))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}


