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

namespace GrupoLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GraphServiceClient _graphServiceClient;

        public HomeController(ILogger<HomeController> logger, GraphServiceClient graphServiceClient)
        {
            _logger = logger;
            _graphServiceClient = graphServiceClient;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }

        //[Authorize(Roles = ("Usuario ,Admin"))]
        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> PrivacyAsync()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                var me = await _graphServiceClient.Me.Request().GetAsync();
                ViewData["Me"] = me;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Autorizar()
        {
           var Authorization_Endpoint = "https://login.microsoftonline.com/d4275aeb-3928-4ad7-956f-695cd0bbf3f1/oauth2/v2.0/authorize";
            var Response_Type = "code";
            var Client_Id = "3223742f-f7c0-448f-80d2-8c31f9c788a6";
            var Redirect_Uri = "https://localhost:7156/Home/Autorizado";
            var Scope = "User.Read";
            var State = "ThisIsMyStateValue";
            var url = $"{Authorization_Endpoint}?response_type ={Response_Type}&client_id ={ Client_Id}&redirect_uri ={ Redirect_Uri}&scope ={ Scope}&state ={ State}";
            Console.WriteLine(url);
            return Redirect(url);
        }

        public async Task<ActionResult> AutorizadoAsync(string code, string state, string session_state) {
            JsonResponseModel Result = new JsonResponseModel();

            //var code1 = HttpContext.Request.Cookies[".AspNetCore.Cookies"];
            using (var httpClient = new HttpClient())
            {
                var Token_Endpoint = "https://login.microsoftonline.com/d4275aeb-3928-4ad7-956f-695cd0bbf3f1/oauth2/v2.0/token";
                var Grant_Type = "authorization_code";
                var Redirect_Uri = "https://localhost:7156/Home/Autorizado";
                var Client_Id = "3223742f-f7c0-448f-80d2-8c31f9c788a6";
                var Client_Secret = "Af88Q~q5EC8Htr6Xl6dO.1iK4ZZSdL~Kudk51c5k";
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
                var Grant_Type = "authorization_code";
                var Redirect_Uri = "https://localhost:7156/Home/Autorizado";
                var Client_Secret = "Af88Q~q5EC8Htr6Xl6dO.1iK4ZZSdL~Kudk51c5k";
                var Scope = "User.Read";
                var Client_Id = "3223742f-f7c0-448f-80d2-8c31f9c788a6";
                var tenantId = "d4275aeb-3928-4ad7-956f-695cd0bbf3f1";

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer "+Result.access_token);

                using HttpResponseMessage response = await httpClient.GetAsync(Graph_Endpoint);

                Console.WriteLine(response);

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