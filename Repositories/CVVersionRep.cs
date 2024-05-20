using CVAPI.Data;
using CVAPI.Interfaces;
using CVAPI.Models;


namespace CVAPI.Repositories {
    public class CVVersionRep:ICVVersionRep {

        private DataContext _Context;

        public CVVersionRep(DataContext Context){

            _Context = Context;
        }

        public bool CVVersionExists(string id)
        {
             return _Context.cvVersions.Any(c => c.id == id);
        }

        public CVVersion GetCVVersion(string id)
        {
            return _Context.cvVersions.Where(e => e.id == id).FirstOrDefault();
        }


        public ICollection<CVVersion> GetCVVersions()
        {
            return _Context.cvVersions.ToList();
        }

        public bool save()
        {
             var saved = _Context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool updateCVVersion(CVVersion cVVersion)
        {
            _Context.Update(cVVersion);
            return save();
        }

    }
}
