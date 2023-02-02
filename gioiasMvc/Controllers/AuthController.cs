using gioiasMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace gioiasMvc.Controllers
{
    public class AuthController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        UserAccount _oUserAccount = new UserAccount();
        public IActionResult Index()
        {
            HttpContext.Session.SetString("SessionKey", "Any"); // Isso trava a geração de um novo SessionID a cada requisição na mesma instância de navegador
            ViewData["SessionId"] = HttpContext.Session.Id;
            return View();
        }

        [HttpPost]
        public async Task<UserAccount> AddUserAccount(UserAccount userAccount)
        {
            _oUserAccount = new UserAccount();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(userAccount), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7200/api/Student?apiKey=secretKey", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oUserAccount = JsonConvert.DeserializeObject<UserAccount>(apiResponse);
                }
            }
            return _oUserAccount;
        }
    }
}