using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BC_Exercise.Web.Areas.UserPage.Controllers
{
    public class UserController : Controller
    {
        // GET: UserPage/User
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserPage/User
        public ActionResult Add()
        {
            return View();
        }
    }
}