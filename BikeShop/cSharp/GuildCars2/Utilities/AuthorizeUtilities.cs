using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildBikes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GuildBikes.Utilities
{
    public class AuthorizeUtilities
    {
        public static string GetUserId(Controller controller)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userMgr.FindByName(controller.User.Identity.Name);
            return user.Id;
        }
        public static string GetUserName(Controller controller)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userMgr.FindByName(controller.User.Identity.Name);
            return user.firstName + " " + user.lastName;
        }
    }
}