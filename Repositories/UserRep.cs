using CVAPI.Data;
using CVAPI.Models;
using CVAPI.Schemas;
using Microsoft.EntityFrameworkCore;


namespace CVAPI.Repositories 
{
    public class UserRep {

        private readonly DataContext context;

        public UserRep(DataContext dataContext) {
            context=dataContext;
        }
        
        public async Task<User?> GetUser(string userId){
            try{
                var user=await context.users.FirstAsync(user=>user.id==userId);
                return user;
            }
            catch(Exception exception){
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
            else throw new Exception("no user to update"); 
        }

    }
}
