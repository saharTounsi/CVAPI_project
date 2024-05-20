using CVAPI.Data;
using CVAPI.Interfaces;
using CVAPI.Models;
using CVAPI.Schemas;
using Microsoft.EntityFrameworkCore;
//using AutoMapper;


namespace CVAPI.Repositories {
    public class CVExportRep: ICVExportRep {
    private readonly DataContext context;

        public CVExportRep(DataContext dataContext) {

            context=dataContext;
        }

        public async Task<User> GetCVExportUser(string cvExportId)
        {
            var cvexport=await context.FindAsync<CVExport>(cvExportId);
            if(cvexport!=null){
                var exporterId=cvexport.exporterId;
                //var user=await context.FindAsync<User>(exporterId);
                var user=await context.users.FindAsync(exporterId);
                return user;
            }
            else return null;
        }

        public async Task<CVVersion> GetCVExportVersion(string cvVersionId)
        {
            var cvexport=await context.FindAsync<CVExport>(cvVersionId);
            if(cvexport!=null){
                var versionId=cvexport.versionId;
                var version=await context.cvVersions.FirstAsync(version=>version.id==versionId);
                return version;
            }
            else return null;
        } 
    }
}
