using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BC_Exercise.Core.Infrastructure;
using BC_Exercise.Core.Interfaces;
using BC_Exercise.Models.User;
using BC_Exercise.Web.Models.BindingModels;
using BC_Exercise.Web.Models.ViewModels;
using Raven.Client;

namespace BC_Exercise.Web.ApiControllers
{
    [RoutePrefix("api/user")]
    public class UserApiController : ApiController
    {
        private IUserService UserService;

        public UserApiController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        [Route("getuser/{id}")]
        public IHttpActionResult GetUser(string id)
        {
            string convertedId = "users/" + id;
            User result = new User();

            using (var ravenSession = DataDocumentStore.Instance.OpenSession())
            {
                result = UserService.GetUser(ravenSession, convertedId);
            }

            if (result == null)
            {
                return BadRequest("User not found!");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("getallusers")]
        public IHttpActionResult GetAllUsers()
        {
            List<User> users = new List<User>();

            using (var ravenSession = DataDocumentStore.Instance.OpenSession())
            {
                users = UserService.GetAllUsers(ravenSession);
            }

            return Ok(users);
        }

        [HttpPost]
        [Route("adduser")]
        public IHttpActionResult AddUser(AddUserBindingModel model)
        {
            using (var ravenSession = DataDocumentStore.Instance.OpenSession())
            {
                var userToAdd = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    StreetAddress = model.StreetAddress,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip
                };

                UserService.AddUser(ravenSession, userToAdd);
            }

            return Ok();
        }

        [HttpPost]
        [Route("updateuser")]
        public IHttpActionResult UpdateUser(UserViewModel model)
        {
            User result = new User();

            using (var ravenSession = DataDocumentStore.Instance.OpenSession())
            {
                var userToUpdate = new User()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    StreetAddress = model.StreetAddress,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip
                };

                result = UserService.UpdateUser(ravenSession, userToUpdate);
            }

            if (result == null)
            {
                return BadRequest("User not found!");
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("deleteuser/{id}")]
        public IHttpActionResult DeleteUser(string id)
        {
            string convertedId = "users/" + id;
            string result = null;

            using (var ravenSession = DataDocumentStore.Instance.OpenSession())
            {
                result = UserService.DeleteUser(ravenSession, convertedId);
            }

            if (result == null)
            {
                return BadRequest("User not found!");
            }

            return Ok();
        }
    }
}
