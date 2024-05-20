using CVAPI.Models;
using CVAPI.Schemas;


namespace CVAPI.Interfaces {


    public interface ICVExportRep {
        Task<User> GetCVExportUser(string cvExportId);
        Task<CVVersion> GetCVExportVersion(string cvVersionId);
    }
}