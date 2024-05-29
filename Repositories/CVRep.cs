using CVAPI.Data;
using CVAPI.Models;
using CVAPI.Schemas;


namespace CVAPI.Repositories {
    public class CVRep {

        private readonly DataContext context;
        private CVVersionRep cvVersionRep;
        public UserRep userRep;

        public CVRep(DataContext dataContext,CVVersionRep cvVersionRep,UserRep userRep){
            context=dataContext;
            this.cvVersionRep=cvVersionRep;
            this.userRep=userRep;
        }

        public async Task<User> FindUserByEmail(string email){
            return await userRep.FindUserByEmail(email);
        }

        public async Task<CV> AddCV(string userId,string cvName,CVData data){
            var cv=new CV(){name=cvName,userId=userId};
            context.Add(cv);
            await cvVersionRep.AddVersion(cv,data);
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
            var user=await userRep.FindById(cv.userId);
            var version=await cvVersionRep.findOne(cv.currentVersionId);
            return new CVSchema(cv,version,user.firstName+" "+user.lastName);
        }
    }
}
