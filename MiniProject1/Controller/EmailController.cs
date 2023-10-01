using Microsoft.AspNetCore.Mvc;
using MiniProject1.Services;

namespace MiniProject1.Controller;

[Route("[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly SoapService _soapService;

    public EmailController(SoapService soapService)
    {
        _soapService = soapService;
    }

    [HttpGet]
    [Route("GetCountry")]
    public async Task<IActionResult> GetCountry()
    {
        string ip = "192.168.1.1";
        var country = await _soapService.FindCountryByIpAsync(ip);
        return Ok(country);
    }
}