using MiniProject1.Models;
using Newtonsoft.Json.Linq;

namespace MiniProject1.Services;

public class GenderService
{
    private readonly CountryService _countryService;

    public GenderService(CountryService countryService)
    {
        _countryService = countryService;
    }

    public async Task<string> FindGenderByNameAndCountryAsync(Recipient recipient)
    {
        const string apiUrl = "https://gender-api.com/get";
        const string apiKey = "api-key";

        var country = await _countryService.FindCountryByIpAsync(recipient.Ip);

        var url = $"{apiUrl}?name={recipient.Name}&country={country}&key={apiKey}";

        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(url);

        var jsonResponse = JObject.Parse(response);

        var gender = jsonResponse["gender"].ToString();
        
        return gender;
    }
}