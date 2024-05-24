using Microsoft.EntityFrameworkCore;


namespace CVAPI.Data.Seeder {
    public class Seeder {

        DataContext context;
        bool dropDatabase=false;

        public Seeder(WebApplication app){
            var scope=app.Services.CreateScope();
            dropDatabase=app.Configuration.GetValue("dropDatabase",dropDatabase);
            context=scope.ServiceProvider.GetService<DataContext>()!;
        } 
        public void seedDatabase(){ 
            if(dropDatabase){
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            if(!context.users.Any()) seedUsers();
            if(!context.cvs.Any()) seedCVs(); 
            if(!context.cvExports.Any()) seedCVExports();
            if(!context.cvModifs.Any()) seedCVModifs();
            if(!context.cvVersions.Any()) seedCVVersions();
            context.SaveChanges();
        }

        private void seedUsers(){
            context.users.AddRange(SeederData.users);
        }  
        private void seedCVs(){
            context.cvs.AddRange(SeederData.cvs);
        }  
        private void seedCVExports(){
            context.cvExports.AddRange(SeederData.cvExports);
        }  
        private void seedCVModifs(){
            context.cvModifs.AddRange(SeederData.cvModifs);
        }
        private void seedCVVersions(){
            context.cvVersions.AddRange(SeederData.cvVersions);
        }
    }
}