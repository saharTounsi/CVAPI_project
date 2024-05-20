using CVAPI.Models;
using CVAPI.Schemas;

namespace CVAPI.Interfaces 
{
    public interface IUserRep {
        List<UserSchema> GetUsers(string adminid);
        Task<User> CreateUser(UserSignUpSchema data);
        Task<User?> DeleteUser(string userId);
        Task<User> UpdateUser(string userId,UserUpdateSchema data);
        //bool save(); 
    }
}
