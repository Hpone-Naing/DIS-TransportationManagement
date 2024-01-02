using TransportationManagement.Data;
using Microsoft.EntityFrameworkCore;
using TransportationManagement.Models;

namespace TransportationManagement.Services.Impl
{
    public class UserServiceImpl : AbstractServiceImpl<User>, UserService
    {
        public UserServiceImpl(HumanResourceManagementDBContext context) : base(context)
        {
            
        }

        public User FindUserById(int id)
        {
            return FindById(id);
        }

        public User FindUserByUserName(string userName)
        {
            return FindByString("UserID", userName);
        }

        public User FindUserByUserNameEgerLoad(string userName)
        {
            return  _context.Users
                           .Include(user => user.UserType)
                           .FirstOrDefault(user => user.UserID == userName);
        }
    }
}
