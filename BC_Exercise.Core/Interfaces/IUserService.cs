using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using BC_Exercise.Models.User;
using Raven.Client;

namespace BC_Exercise.Core.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers(IDocumentSession session);
    }
}