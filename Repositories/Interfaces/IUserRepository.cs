using MeterReaderCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderCMS.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User, int>
    {
        void AddUserRole(UserRole item);

        User Get(string username);
    }
}
