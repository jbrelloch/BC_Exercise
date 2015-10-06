using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using BC_Exercise.Models.User;
using Raven.Client;

namespace BC_Exercise.Core.Interfaces
{
    public interface IUserService
    {
        User GetUser(IDocumentSession ravenSession, string id);
        List<User> GetAllUsers(IDocumentSession session);
        string AddUser(IDocumentSession ravenSession, User model);
        User UpdateUser(IDocumentSession ravenSession, User model);
        string DeleteUser(IDocumentSession ravenSession, string id);
    }
}