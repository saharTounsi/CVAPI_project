using CVAPI.Data;
using CVAPI.Interfaces;
using CVAPI.Models;
using CVAPI.Schemas;
using Microsoft.EntityFrameworkCore;
//using AutoMapper;


namespace CVAPI.Repositories {
    public class CVModifRep : ICVModifRep {
    private readonly DataContext context;

        public CVModifRep(DataContext dataContext) {   

            context=dataContext;
        }

        public async Task<CV> GetcvModifCV(string CVId)
        {
            var cvmodif=await context.FindAsync<CVModif>(CVId);
            if(cvmodif!=null){
                var cvId=cvmodif.cvId;
                var cv=await context.cvs.FindAsync(cvId);
                return cv;
            }
            else return null;
        }

        public async Task<User> GetCVModifEditor(string cvModifId){
            var cvModif=await context.FindAsync<CVModif>(cvModifId);
            var editorId=cvModif.editorId;
            var editor=await context.users.FirstAsync(user=>user.id==editorId);
            return editor;
        }
    }
}