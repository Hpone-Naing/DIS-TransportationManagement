using TransportationManagement.Models;

namespace TransportationManagement.Services
{
    public interface UserService
    {
        User FindUserById(int id);
        User FindUserByUserName(string userName);
        User FindUserByUserNameEgerLoad(string userName);

    }
}
