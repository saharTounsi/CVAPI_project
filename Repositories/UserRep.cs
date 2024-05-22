using CVAPI.Data;
using CVAPI.Interfaces;
using CVAPI.Models;
using CVAPI.Schemas;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace CVAPI.Repositories 
{
    public class UserRep:IUserRep {

        private readonly DataContext context;

        public UserRep(DataContext dataContext) {
            context=dataContext;
        }
        
        public List<UserSchema> GetUsers(string adminid){
            throw new NotImplementedException();
        }

        public async Task<User?> FindByCredentials(UserCredentials credentials){
            string userEmail=credentials.email;
            string userPassword=credentials.password;
            try{
                var user=await context.users.FirstAsync(user=>(user.email==userEmail)&&(user.password==userPassword));
                return user;
            }
            catch(Exception exception){
                return null;
            }
        } 

        public async Task<User> CreateUser(UserSignUpSchema data){
            var user=new User(data);
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteUser(string userId){
            var user=await context.FindAsync<User>(userId);
            if(user!=null){
                context.Remove(user);
                await context.SaveChangesAsync();
            }
            return user;
        }


        public async Task<User> UpdateUser(string userId,UserUpdateSchema data){
           var user=await context.FindAsync<User>(userId);
            if(user!=null){
                var entry=context.Update<User>(user);
                entry.Entity.name=data.name;
                entry.Entity.password=data.password;
                await context.SaveChangesAsync();   
            }
            return user; 
        }

    }
}
