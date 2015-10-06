using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Exercise.Models.User;

namespace BC_Exercise.Core.Interfaces
{
    public interface IExternalService
    {
        string GetAddressStrFromObject(User model);
        string GetCoordinatesFromAddress(string id);
    }
}
