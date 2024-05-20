using CVAPI.Schemas;


namespace CVAPI.Interfaces {
    public interface ICVRep {
        List<CVSchema> GetUserCVs(string id);
       // Task<(FileStream,string)> GetCV(string id);
        /* bool createCV(CV cV);
        bool updateCV(CV cV);
        bool CVExists(string id);
        bool save(); */
    }
}
