using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace OrnekSite.Entity.Identity
{
    public class IdentityInitializer:DropCreateDatabaseIfModelChanges<IdentityContext>
    {
        protected override void Seed(IdentityContext context)
        {
            if (!context.Roles.Any(i=>i.Name=="admin"))//admin isimli rol yoksa aşağıdaki işlemleri yap.
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole()
                {
                    Name = "admin",
                    Description = "admin rolü",

                };
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "user"))//admin isimli rol yoksa aşağıdaki işlemleri yap.
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole()
                {
                    Name = "user",
                    Description = "user rolü",

                };
                manager.Create(role);
            }

            if (!context.Users.Any(i=>i.Name=="ahmetsokmen"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Ahmet",
                    Surname = "Sokmen",
                    UserName = "ahmetsokmen",
                    Email = "ahmet-sokmen@outlook.com"
                   
                };
                manager.Create(user, "123456");
                manager.AddToRole(user.Id, "admin");//lkullanıcıya admin rolü verilir.
                manager.AddToRole(user.Id, "user");//lkullanıcıya user rolü verilir.

            }
            if (!context.Users.Any(i => i.Name == "enessokmen"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Enes",
                    Surname = "Sökmen",
                    UserName = "enessokmen",
                    Email = "enes-sokmen@hotmail.com"

                };
                manager.Create(user, "123456");
                manager.AddToRole(user.Id, "user");//lkullanıcıya user rolü verilir.
             
            }

            base.Seed(context);
        }
    }
}