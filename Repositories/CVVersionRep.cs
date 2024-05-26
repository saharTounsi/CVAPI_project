using CVAPI.Data;
using CVAPI.Interfaces;
using CVAPI.Models;
using CVAPI.Schemas;
using Microsoft.EntityFrameworkCore;


namespace CVAPI.Repositories {
    public class CVVersionRep {

        private DataContext context;
        //private CVRep cvRep;

        public CVVersionRep(DataContext context){
            this.context=context;
            //this.cvRep=cvRep;
        }

        public async Task<CVVersion> AddVersion(CV cv,CVData data){
            var version=new CVVersion(){
                cvId=cv.id,
                fileName=data.fileName,
                profileName=data.profileName,
                profileEmail=data.profileEmail,
                profileTel=data.profileTel,
                profileSkills=data.profileSkills,
                profileExperience=data.profileExperience,
            };
            context.Add(version);
            cv.currentVersionId=version.id;
            await context.SaveChangesAsync();
            return version;
        }

        public async Task<CV?> FindCV(string cvId){
            return await context.cvs.FindAsync(cvId);
        }

        public Task<List<CVVersion>> FindAllByCVId(string cvId){
            var versions=context.cvVersions.Where(version=>version.cvId==cvId).ToList();
            return Task.FromResult(versions);
        }

        public async Task<CVVersion?> findOne(string id){
            var version=await context.FindAsync<CVVersion>(id);
            return version;
        }
    }
}
