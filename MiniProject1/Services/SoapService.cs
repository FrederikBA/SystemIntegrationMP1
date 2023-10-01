using System.Net.Http.Headers;
using System.Xml.Linq;
using MiniProject1.Models;

namespace MiniProject1.Services
{
    public class SoapService
    {
        public async Task<Country> FindCountryByIpAsync(string ip)
        {
            const string url = "http://wsgeoip.lavasoft.com/ipservice.asmx";

            var payload =
                $@"<?xml version=""1.0"" encoding=""utf-8""?>
    <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
    <soap12:Body>
        <GetIpLocation xmlns=""http://lavasoft.com/"">
        <sIp>{ip}</sIp>
        </GetIpLocation>
    </soap12:Body>
    </soap12:Envelope>";

            var httpClient = new HttpClient();
            var httpContent = new StringContent(payload);

            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Method = HttpMethod.Post;
            req.Content = httpContent;
            req.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml; charset=utf-8");

            // Here you will get the Reponse from service
            var response = await httpClient.SendAsync(req);
            // Converting the response into text format
            var responseBodyAsText = await response.Content.ReadAsStringAsync();
            var xmlDoc = XDocument.Parse(responseBodyAsText);
            var country = xmlDoc.Descendants().ToList()[2].Value;
            var element = XElement.Parse(country);
            var countryElement = element.Element("Country");
            
            
            return new Country(countryElement.Value);
        }
    }
}
