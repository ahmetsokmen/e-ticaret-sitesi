using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(OrnekSite.App_Start.Startup1))]

namespace OrnekSite.App_Start
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)//owinde cookie yönetimi owin cookieauthentication ile sağlanır.
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions { 

                AuthenticationType="ApplicationCookie",
                LoginPath=new PathString("/Account/Login")//kullanıcı yetkisi olmayan bir sayfaya gitmek istediğinde onu login sayfasına yönlendirir.
            
            });
        }
    }
}
