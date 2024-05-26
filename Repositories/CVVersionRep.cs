using CVAPI.Data;
using CVAPI.Interfaces;
using CVAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace CVAPI.Repositories {
    public class CVVersionRep {

        private DataContext context;
        //private CVRep cvRep;

        public CVVersionRep(DataContext context){
            this.context=context;
            //this.cvRep=cvRep;
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
