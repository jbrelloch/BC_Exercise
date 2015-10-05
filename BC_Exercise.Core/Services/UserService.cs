using System.Collections.Generic;
using System.Linq;
using BC_Exercise.Core.Interfaces;
using BC_Exercise.Models.User;
using Raven.Client;

namespace BC_Exercise.Core.Services
{
    public class UserService : IUserService
    {
        public List<User> GetAllUsers(IDocumentSession ravenSession)
        {
            var result = ravenSession.Query<User>().ToList();

            return result;
        }

        public string AddUser(IDocumentSession ravenSession, User model)
        {
            ravenSession.Store(model);
            ravenSession.SaveChanges();

            return model.Id;
        }
    }
}