using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using OrnekSite.Entity;
using OrnekSite.Entity.Identity;
using OrnekSite.Entity.ShippingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OrnekSite.Controllers
{
    public class AccountController : Controller
    {
        DataContext db = new DataContext();
        private UserManager<ApplicationUser> UserManager;//usermanager sınıfı applicationuser kullanıcı türünü yönetecektir.
        private RoleManager<ApplicationRole> RoleManager;//rolemanager sınıfı applicationrole kullanıcı türünü yönetecektir.

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityContext());
            UserManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<ApplicationRole>(new IdentityContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)//kullanıcı zorunlu alanları doğru bir şekilde doldurmuş mu.
            {
                var user = new ApplicationUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.Username,

                };
                var result = UserManager.Create(user,model.Password);//Yeni bir user Oluşturur.
                if (result.Succeeded)//kayıt başarılı mı ?
                {

                    if (RoleManager.RoleExists("user"))//user isimli bir rol var mı
                    {
                        UserManager.AddToRole(user.Id, "user");//kullanıcıya user rolü ekler
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturma hatası");
                }
            }
            return View(model);
        }
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var orders= db.Orders.Where(x => x.UserName == username).Select(x=>new UserOrder() { //bu kullanıcııya ait tüm siparişleri listeler.
            
            Id=x.Id,
            OrderDate=x.OrderDate,
            OrderNumber=x.OrderNumber,
            Total=x.Total,
            OrderState=x.OrderState,
            
            }).OrderByDescending(i=>i.OrderDate).ToList();//verilen siparişleri tarihe göre sıralama.
            return View(orders);
        }
      
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(model.Username, model.Password);
                if (user!=null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityClaims = UserManager.CreateIdentity(user,"ApplicationCookie");//useri cookieye atıyoruz.
                    var authProperties = new AuthenticationProperties();//rememberme için gerekli
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties, identityClaims);//giriş işlemi tamamlanır

                    if (!String.IsNullOrEmpty(returnUrl))//kullanıcı giriş yapmadan izinsiz sayfalara gitmek istediğinde logine geri yönlendirme ta ki giriş yapana kadar.
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Hatalı giriş");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index","Home");
        }

        public ActionResult UserProfil()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();//şuan bağlı olan kullanıcının idsini almaya yarar.
            var user = UserManager.FindById(id);//bu idye ait kullanıcıyı getir.
            var model = new UserProfile()
            {
                Id=user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult UserProfil(UserProfile userprofile)
        {
           
            var user = UserManager.FindById(userprofile.Id);
            user.Name = userprofile.Name;
            user.Surname = userprofile.Surname;
            user.Email = userprofile.Email;
            user.UserName = user.UserName;
            UserManager.Update(user);

           
            return View("Update");
        }
        public ActionResult ChangePassword()
        {
           
            return View();
        }

        [HttpPost]
        [Authorize]//sisteme giriş yapanlar bu sayfayı görebilsin
        public ActionResult ChangePassword(UserPassword model)
        {
            if (ModelState.IsValid)
            {
                var result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);//sisteme giriş yapan kullanıcıyı bul ve eski şifreyi yenisiyle değiştir.
                return View("Update");
            }
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = db.Orders.Where(i => i.Id == id).Select(i => new OrderDetails()//zaten bir tane sipariş elde edeceğiz niçin find kullanmadık ?
            {
                OrderId = i.Id,
                Total = i.Total,
                OrderDate = i.OrderDate,
                OrderNumber=i.OrderNumber,
                OrderState = i.OrderState,
                Adres = i.Adres,
                Sehir = i.Sehir,
                Ilce = i.Ilce,
                Mahalle = i.Mahalle,
                OrderLines = i.OrderLines.Select(x => new OrderLineModel()
                {
                    ProductId = x.ProductId,
                    Image = x.Product.Image,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()

            }).FirstOrDefault(); //bir tane gelecek

            return View(model);
        }

        public PartialViewResult UserCount()//admin panelde Kullanıcı sayısını gösterme
        {
            var u = UserManager.Users;//kayıtlı kullanıcıları döndürür
            return PartialView(u);


        }

        public ActionResult UserList()
        {
            var u = UserManager.Users;
            return View(u);
        }
    }


}