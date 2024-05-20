using Microsoft.EntityFrameworkCore;


namespace CVAPI.Data.Seeder {
    public class Seeder {

        DataContext context;
        bool dropDatabase;

        public Seeder(WebApplication app){
            var scope=app.Services.CreateScope();
            dropDatabase=app.Configuration.GetValue<bool>("dropDatabase");
            context=scope.ServiceProvider.GetService<DataContext>()!;
        } 
        public void seedDatabase(){ 
            if(dropDatabase){
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            
            if(!context.users.Any()) seedUsers();
            if(!context.cvs.Any()) seedCVs(); 
            if(!context.cvExports.Any()) seedCVEXPORTs();
            if(!context.cvModifs.Any()) seedCVMODIFs();
            if(!context.cvVersions.Any()) seedCVVersions();
            context.SaveChanges();
        }

        private void seedUsers(string tableName="users"){
            context.users.AddRange(SeederData.users);
            
        }  
        private void seedCVs(string tableName="cvs"){
            context.cvs.AddRange(SeederData.cvs);
            //context.SaveChanges();
        }  
        private void seedCVEXPORTs(string tableName="CVExports"){
            context.cvExports.AddRange(SeederData.cvEports);
            //context.SaveChanges();
        }  
        private void seedCVMODIFs(string tableName="CVMODIFs"){
            context.cvModifs.AddRange(SeederData.cvModifs);
            //context.SaveChanges();
        }
         private void seedCVVersions(string tableName="CVVersions"){
            context.cvVersions.AddRange(SeederData.cvVersions);
            //context.SaveChanges();
         }
            

        
        

    }
}