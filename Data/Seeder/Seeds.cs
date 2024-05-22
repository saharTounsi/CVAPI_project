using CVAPI.Models;


namespace CVAPI.Data.Seeder {
    class SeederData {
        public static List<User> users=new List<User>(){
            new(){
                id="0",
                name="ahmed ayachi",
                email="aayachi032@gmail.com",
                password="1234",
                role=User.Role.Admin,
            },
            new(){
                id="1",
                adminId="0",
                name="amine ayachi",
                email="amine152@gmail.com",
                password="1234",
                role=User.Role.Employee,
            },
            new(){
                id="2",
                adminId="0",
                name="alex hunter",
                email="alexhunter7@gmail.com",
                password="1234",
                role=User.Role.HR,
            },
        };

         public static List<CV> cvs=new List<CV>(){
            new(){
                id="0",
                name="cv_0",
                userId="0",
                datetime=DateTime.UtcNow,
                currentVersionId="0",
                
            },
             new(){
                id="1",
                name="cv_1",
                userId="1",
                datetime=DateTime.UtcNow,
                currentVersionId="1",
                
            },
            new(){
                id="2",
                name="cv_2",
                userId="2",
                datetime=DateTime.UtcNow,
                currentVersionId="2",
            },

        };
       public static List<CVExport> cvExports=new List<CVExport>(){
            new(){
                id="0",
                name="sahar Tounsi",
                versionId="0",
                exporterId="0",
               datetime=DateTime.UtcNow,
            },
            new(){
               id="1",
               name="samar Tounsi",
               versionId="1",
               exporterId="1",
               datetime=DateTime.UtcNow,
            },
             new(){
                id="2",
                name="rahma omran",
                versionId="2",
                exporterId="2",
                datetime=DateTime.UtcNow,
            },
        };
        public static List<CVModif> cvModifs=new List<CVModif>(){
            new(){
                id="0",
                cvId="0",
                editorId="0",
                datetime=DateTime.UtcNow,
            },
            new(){
                id="1",
                cvId="1",
                editorId="1",
                datetime=DateTime.UtcNow,
            },
            new(){
                id="2",
                cvId="2",
                editorId="2",
                datetime=DateTime.UtcNow,
            },
        };
        public static List<CVVersion> cvVersions=new List<CVVersion>(){
            new(){
                id="0",
                fileName="file_0.pdf",
                cvId="0",
                datetime=DateTime.UtcNow,
                path="c:0"
            },
            new(){
               id="1",
               fileName="file_1.pdf",
               cvId="1",
               datetime=DateTime.UtcNow,
               path="c:1"
            },
            new(){
              id="2",
              fileName="file_2.pdf",
              cvId="2",
              datetime=DateTime.UtcNow,
              path="c:2"
            },
        };
    }
}