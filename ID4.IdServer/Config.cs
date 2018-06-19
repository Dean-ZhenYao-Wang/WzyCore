using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
namespace ID4.IdServer
{
    public class Config
    {
        /// <summary>
        /// 返回应用列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            List<ApiResource> resources = new List<ApiResource>();
            //ApiResource第一个参数是应用的名字，第二个参数是显示名字 
            resources.Add(new ApiResource("MsgAPI", "消息服务API"));
            return resources;
        }
        /// <summary>
        /// 返回账号列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            // 若多个用户可考虑数据及封装来实现下面的初始化，不然用户多的话累死。
            clients.Add(new Client
            {
                ClientId = "yzk",//用户名 客户端ID
                //AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("w.s-bp-2019".Sha256())//秘钥 
                },
                AllowedScopes = {
                    "MsgAPI",
                    IdentityServerConstants.StandardScopes.OpenId,//解决报forbidden错误
                    IdentityServerConstants.StandardScopes.Profile
                }//这个账号支持访问哪些应用
            });
            return clients;
        }
    }
}
