using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BC_Exercise.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User/User
        public ActionResult Users()
        {
            return View();
        }

        // GET: User/add
        public ActionResult Add()
        {
            return View();
        }

        // GET: User/edit/{id}
        public ActionResult Edit(string id = "")
        {
            return View();
        }
    }
}