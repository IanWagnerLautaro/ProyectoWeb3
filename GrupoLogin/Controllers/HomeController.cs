using GrupoLogin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using GrupoLogin.Web.Models;

namespace GrupoLogin.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly GraphServiceClient _graphServiceClient;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, /*GraphServiceClient graphServiceClient,*/ IConfiguration config)
        {
            _logger = logger;
            //_graphServiceClient = graphServiceClient;
            _config = config;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }

        //[Authorize(Roles = ("Usuario ,Admin"))]
        //[AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        //public async Task<IActionResult> PrivacyAsync()
        //{
        //    if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
        //    {
        //        //var me = await _graphServiceClient.Me.Request().GetAsync();
        //        ViewData["Me"] = me;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        public ActionResult Autorizar()
        {
            var Authorization_Endpoint = _config.GetValue<string>("AzureAd:Authorization_Endpoint");
            var Response_Type = "code";
            var Client_Id = _config.GetValue<string>("AzureAd:ClientId");
            var Redirect_Uri = "https://localhost:7156/Home/Autorizado";
            var Scope = "User.Read";
            var State = "ThisIsMyStateValue";
            var url = $"{Authorization_Endpoint}?response_type ={Response_Type}&client_id ={Client_Id}&redirect_uri ={Redirect_Uri}&scope ={Scope}&state ={State}";
            Console.WriteLine(url);
            return Redirect(url);
        }

        public async Task<ActionResult> AutorizadoAsync(string code, string state, string session_state)
        {
            JsonResponseModel Result = new JsonResponseModel();

            //var code1 = HttpContext.Request.Cookies[".AspNetCore.Cookies"];
            using (var httpClient = new HttpClient())
            {
                var Token_Endpoint = _config.GetValue<string>("AzureAd:Token_Endpoint");
                var Grant_Type = "authorization_code";
                var Redirect_Uri = "https://localhost:7156/Home/Autorizado";
                var Client_Id = _config.GetValue<string>("AzureAd:ClientId");
                var Client_Secret = _config.GetValue<string>("AzureAd:ClientSecret");
                var Scope = "User.Read";

                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = Grant_Type,
                    ["code"] = code,
                    ["redirect_uri"] = Redirect_Uri,
                    ["client_id"] = Client_Id,
                    ["client_secret"] = Client_Secret,
                    ["scope"] = Scope
                });

                using HttpResponseMessage response = await httpClient.PostAsync(
                        Token_Endpoint,
                        content);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                Result = JsonConvert.DeserializeObject<JsonResponseModel>(jsonResponse);
                Console.WriteLine(Result);
                Console.WriteLine($"{jsonResponse}\n");

            }

            using (var httpClient = new HttpClient())
            {
                var Graph_Endpoint = "https://graph.microsoft.com/beta/me";
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Result.access_token);

                using HttpResponseMessage response = await httpClient.GetAsync(Graph_Endpoint);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> LlamarApiAsync(string accesToken)
        {

            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}