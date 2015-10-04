using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BC_Exercise.Entities.Interfaces;

namespace BC_Exercise.Web.Controllers
{
    public class UserApiController : ApiController
    {
        public IUserService UserService;
    }
}
