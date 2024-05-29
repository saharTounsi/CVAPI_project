using CVAPI.Data;
using CVAPI.Models;
using CVAPI.Schemas;
using CVAPI.Middlewares;
using Microsoft.EntityFrameworkCore;


namespace CVAPI.Repositories 
{
    public class UserRep {

        private readonly DataContext context;

        public UserRep(DataContext dataContext) {
            context=dataContext;
        }

        public async Task SetUserLoginOTP(User user,string otp){
            context.Update(user);
            user.loginOTP=otp;
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateUser(UserUpdateFields data){
            var user=await context.FindAsync<User>(data.userId);
            if(user!=null){
                context.Update(user);
                if(data.firstName!=null) user.firstName=data.firstName;
                if(data.lastName!=null) user.lastName=data.lastName;
                if(data.role!=null) user.role=(User.Role)data.role;
                if(data.isActive!=null) user.isActive=(bool)data.isActive;
                var password=data.password;
                if(password!=null) user.hash=User.getHash(user,password);
                await context.SaveChangesAsync();
                return true;
            }
            else throw new Error("no such user to update");
        }

        public async Task<User> AddUser(NewUserSchema data){
            var userEmail=data.email;
            var exists=await context.users.AnyAsync(user=>user.email==userEmail);
            if(!exists){
                var user=new User(data);
                user.hash=User.getHash(user);
                context.Add(user);
                await context.SaveChangesAsync();
                return user;
            } 
            else throw new Error("email taken");
        } 

        public async Task<User?> GetUser(string userId){
            try{
                var user=await context.users.FirstAsync(user=>user.id==userId);
                return user;
            }
            catch(Exception){
                return null;
            }
        }

        public Task<List<UserSchema>> GetUsers(string userId){
            var users=context.users.Where(user=>user.id!=userId).Select(user=>new UserSchema(user)).ToList();
            return Task.FromResult(users);
        }

        public async Task<User> FindUserByEmail(string email){
            try{
                var user=await context.users.FirstAsync(user=>user.email==email);
                return user;
            }
            catch{
                throw new Error("no user with such email");
            } 
        }

        public async Task<User> FindByCredentials(UserCredentials credentials){
            string userEmail=credentials.email;
            var user=await context.users.FirstAsync(user=>user.email==userEmail);
            if((user!=null)&&User.verifyPassword(user,credentials.password)) return user;
            else throw new Error("incorrect credentials");
        }

        public async Task<User> FindById(string userId){
            var user=await context.FindAsync<User>(userId);
            if(user!=null) return user;
            else throw new Error("no such user");
        }

        public async Task<User?> DeleteUser(string userId){
            var user=await context.FindAsync<User>(userId);
            if(user!=null){
                context.Remove(user);
                await context.SaveChangesAsync();
            }
            return user;
        }
    }
}
