using CVAPI.Data;
using CVAPI.Models;
using CVAPI.Schemas;


namespace CVAPI.Repositories {
    public class CVRep {

        private readonly DataContext context;
        private CVVersionRep cvVersionRep;

        public CVRep(DataContext dataContext,CVVersionRep cvVersionRep){
            context=dataContext;
            this.cvVersionRep=cvVersionRep;
        }

        public async Task<CV> AddCV(string userId,string cvName,CVData data){
            var cv=new CV(){name=cvName,userId=userId};
            context.Add(cv);
            var cvversion=new CVVersion(){
                cvId=cv.id,
                fileName=data.fileName,
                profileName=data.profileName,
                profileEmail=data.profileEmail,
                profileTel=data.profileTel,
                profileSkills=data.profileSkills,
                profileExperience=data.profileExperience,
            };
            context.Add(cvversion);
            cv.currentVersionId=cvversion.id;
            await context.SaveChangesAsync();
            return cv;
        } 

        public Task<List<CV>> GetUserCVs(string id){
            var cvs=context.cvs.Where(cv=>cv.userId==id).ToList();
            return Task.FromResult(cvs);
        }

        public Task<List<CV>> GetAll(){
            var cvs=context.cvs.ToList();
            return Task.FromResult(cvs);
        }

        public async Task<CV?> GetCV(string id){
            return await context.cvs.FindAsync(id);
        }

        public async Task<List<CVSchema>> toCVSchema(List<CV> cvs){
            var list=new List<CVSchema>();
            foreach(CV cv in cvs){
                var cvschema=await this.toCVSchema(cv);
                list.Add(cvschema);
            }
            return list;
        }

        public async Task<CVSchema> toCVSchema(CV cv){
            var version=await cvVersionRep.findOne(cv.currentVersionId);
            return new CVSchema(cv,version);
        }
    }
}
