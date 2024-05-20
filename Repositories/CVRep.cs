using CVAPI.Data;
using CVAPI.Models;
using CVAPI.Schemas;
using CVAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;


namespace CVAPI.Repositories {
    public class CVRep:ICVRep {

        private readonly DataContext context;

        public CVRep(DataContext dataContext){
            context=dataContext;    
        }

        public List<CVSchema> GetUserCVs(string id){
            var cvs=context.cvs.Where<CV>(cv=>cv.userId==id).ToList<CVSchema>();
            return cvs;
        }
        /* public bool createCV(CV cV)
        {
            _Context.Add(cV);
            return save();
        }

        public bool CVExists(string id)
        {
            return _Context.cvs.Any(c => c.id == id);
        }

        public Task<(FileStream, string)> GetCV(string id)
         {
             var fileEntity = await _Context.cvs.FindAsync(id);
             if (fileEntity == null)
                 return (null,null);

             string tempFilePath = Path.GetTempFileName();

             System.IO.File.WriteAllBytes(tempFilePath, fileEntity.referenceDocument);

             FileStream stream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

             return (stream,fileEntity.Title);
         }


        public ICollection<CV> GetCVs()
        {
             return _Context.cvs.ToList();
        }

        public bool save()
        {
           var saved = _Context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool updateCV(CV cV)
        {
            _Context.Update(cV);
            return save();
        } */
    }
}
