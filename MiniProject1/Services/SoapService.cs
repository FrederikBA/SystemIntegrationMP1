using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
namespace MiniProject1.Services;

public class SoapService
{
    public async Task<string> FindCountryByIpAsync(string ip)
    {
        string url = "http://wsgeoip.lavasoft.com/ipservice.asmx";
        
        string payload = $@"<?xml version=""1.0"" encoding=""utf-8""?>
        <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
        <soap12:Body>
            <GetIpLocation xmlns=""http://lavasoft.com/"">
                <sIp>{ip}</sIp>
            </GetIpLocation>
        </soap12:Body>
        </soap12:Envelope>";

        var client = new HttpClient();
        {
           var response = await client.PostAsync(url, new StringContent(payload, Encoding.UTF8, "text/xml"));

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                XDocument xmlDoc = XDocument.Parse(content);

                // Find the GetIpLocationResult element and extract its text
                XElement resultElement = xmlDoc.Descendants("GetIpLocationResult").FirstOrDefault();
                if (resultElement != null)
                {
                    string resultString = resultElement.Value;

                    // Use regular expressions to extract the country
                    var tag = "Country";
                    var regEx = $"<{tag}>(.*?)</{tag}>";
                    Match match = Regex.Match(resultString, regEx);
                    if (match.Success)
                    {
                        return match.Groups[1].Value;
                    }
                }
            }
        }
        return "No country found";
    }
}