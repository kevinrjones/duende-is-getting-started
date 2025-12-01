using System.Diagnostics;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherMVC.Models;
using WeatherMVC.Services;

namespace WeatherMVC.Controllers;

public class HomeController(ITokenService tokenService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Weather()
    {
        using var client = new HttpClient();
        var token = await tokenService.GetToken("weatherapi.read");
        
        client.SetBearerToken(token.AccessToken);
        
        var result = await client.GetAsync("https://localhost:5445/weatherforecast");

        if (result.IsSuccessStatusCode)
        {
            var model = await result.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<WeatherData>>(model);
            
            return View(data);
        }
        
        throw new Exception();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}