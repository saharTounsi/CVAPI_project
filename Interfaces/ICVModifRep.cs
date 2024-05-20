using CVAPI.Models;
using CVAPI.Schemas;


namespace CVAPI.Interfaces {
    public interface ICVModifRep {

        Task<User> GetCVModifEditor(string cvModifId); 
    }
}
   