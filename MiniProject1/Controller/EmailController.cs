using Microsoft.AspNetCore.Mvc;
using MiniProject1.Models;
using MiniProject1.Services;

namespace MiniProject1.Controller;

[Route("[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly CountryService _soapService;
    private readonly GenderService _genderService;

    public EmailController(CountryService soapService, GenderService genderService)
    {
        _soapService = soapService;
        _genderService = genderService;
    }

    [HttpGet]
    [Route("GetCountry")]
    public async Task<IActionResult> GetCountry()
    {
        string ip = "87.52.111.60";
        var country = await _soapService.FindCountryByIpAsync(ip);
        return Ok(country);
    }
    
    [HttpGet]
    [Route("GetGender")]
    public async Task<IActionResult> GetGender()
    {
        var gender = await _genderService.FindGenderByNameAndCountryAsync(new Recipient("Donald", "f@mail.dk", "87.52.111.60"));
        return Ok(gender);
    }
}