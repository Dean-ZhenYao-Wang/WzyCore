using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LoginService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> RequestToken(LoginRequest model)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["client_id"] = "clientPC1";
            dict["client_secret"] = "123321";
            dict["grant_type"] = "password";

            dict["username"] = model.UserName;
            dict["password"] = model.Password;
            //Console.WriteLine($"用户名：{model.UserName}，密码:{model.Password}");
            //由登录服务器向IdentityServer发请求获取Token
            using (HttpClient http = new HttpClient())
            using (var content = new FormUrlEncodedContent(dict))
            {
                var msg = await http.PostAsync("http://localhost:9500/connect/token", content);
                string result = await msg.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
        }
    }
}
