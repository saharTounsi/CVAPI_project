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

        public Task<List<UserSchema>> GetUsers(){
            var users=context.users.Where(user=>user.id!=null).Select(user=>new UserSchema(user)).ToList();
            return Task.FromResult(users);
        }

        public async Task<User?> FindByCredentials(UserCredentials credentials){
            string userEmail=credentials.email;
            string userPassword=credentials.password;
            try{
                var user=await context.users.FirstAsync(user=>(user.email==userEmail)&&(user.hash==userPassword));
                return user;
            }
            catch(Exception){
                return null;
            }
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
                var firstName=data.firstName;
                var lasttName=data.lasttName;
                var password=data.password;;
                var entry=context.Update<User>(user);
                if(firstName!=null) entry.Entity.firstName=firstName;
                if(lasttName!=null) entry.Entity.lastName=lasttName;
                if(password!=null) entry.Entity.hash=password;
                await context.SaveChangesAsync();
                return user;  
            }
            else throw new Error("no user to update"); 
        }

    }
}
