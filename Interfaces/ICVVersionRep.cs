using CVAPI.Models;

namespace CVAPI.Interfaces {
    public interface ICVVersionRep {
        ICollection<CVVersion> GetCVVersions();
        CVVersion GetCVVersion(string id);
        bool CVVersionExists(string id);
        bool updateCVVersion(CVVersion cVVersion);
        bool save();
    }
}
