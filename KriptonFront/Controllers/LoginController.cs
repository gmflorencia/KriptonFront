using Microsoft.AspNetCore.Mvc;
using Data.DTOs;
using Data.Base;
using Newtonsoft.Json;



namespace KriptonFront.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public LoginController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Ingresar(LoginDto login)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = await baseApi.PostToApi("Login", login);
            var resultadoLogin = token as OkObjectResult;
            var resultadoObjeto = JsonConvert.DeserializeObject<Models.Login>(resultadoLogin.Value.ToString());
            ViewBag.Nombre = resultadoObjeto.Nombre;
            return View("~/Views/Home/Index.cshtml", resultadoObjeto);
        }
    }
}
