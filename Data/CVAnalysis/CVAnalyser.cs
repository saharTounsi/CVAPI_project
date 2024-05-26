using RestSharp;
using CVAPI.Middlewares;
using CVAPI.Schemas;
using System.Text.Json;


namespace CVAPI.Data.CVAnalysis {
    public class CVAnalyser {

        public static readonly List<string> supportedTypes=["application/pdf"];

        private static readonly string url="https://1d75-34-148-70-7.ngrok-free.app";
        private static readonly RestClient client=new RestClient(url);
        private static readonly string tmpFolderName="tmp";
        public static readonly string CVFolderName="wwwroot";

        public static async Task<CVData> analyseCV(IFormFile formFile){
            Dictionary<string,string> fileProps=await saveTmpFormFile(formFile);
            var filePath=fileProps["filePath"];
            var request=new RestRequest("/upload",Method.Post){
                AlwaysMultipartFormData=true,
            };
            request.AddHeader("Content-Type","multipart/form-data");
            request.AddFile("file",filePath,ContentType.Binary,new FileParameterOptions{
                DisableFilenameEncoding=true,
                DisableFileNameStar=true,
            });
            var response=await client.PostAsync(request);
            if(response.IsSuccessStatusCode){
                var data=JsonSerializer.Deserialize<Dictionary<string,string>>(response.Content??"{}");
                var fileName=fileProps["fileName"];
                moveTmpFile(fileName);
                return new CVData(){
                    profileName=data["nom"],
                    profileEmail=data["email"],
                    profileTel=data["telephone"],
                    profileSkills=data["competences"],
                    profileExperience=data["experience"],
                    fileName=fileName,
                };
            }
            else{
                File.Delete(filePath);
                throw new Error("could not upload file "+response.StatusCode+".");
            };
        }

        private static void moveTmpFile(string fileName){
            if(!Directory.Exists(CVFolderName)) Directory.CreateDirectory(CVFolderName);
            string sourceFile=Path.Combine(tmpFolderName,fileName);
            string destinationFile=Path.Combine(CVFolderName,fileName);;
            File.Move(sourceFile,destinationFile);
        }

        private static async Task<Dictionary<string,string>> saveTmpFormFile(IFormFile formFile){
            if(checkFormFile(formFile)){
                if(!Directory.Exists(tmpFolderName)) Directory.CreateDirectory(tmpFolderName);
                var props=new Dictionary<string,string>();
                var slices=formFile.FileName.Split(".").ToList();
                var fileExt=slices.Last();
                slices.RemoveAt(slices.Count-1);
                //var fileBasename=string.Join(".",slices).ToLower();
                var fileName=getFileId()+"."+fileExt;
                string filePath=Path.Combine(tmpFolderName,fileName);
                using (Stream fileStream=new FileStream(filePath,FileMode.Create)){
                    await formFile.CopyToAsync(fileStream);
                }
                props.Add("fileName",fileName);
                props.Add("filePath",filePath);
                return props;
            }
            else throw new Error("unsupported file format. CV needs to be in "+string.Join(",",supportedTypes)+".");
        }

        private static bool checkFormFile(IFormFile formFile){
            return supportedTypes.Contains(formFile.ContentType);
        }

        static private string getFileId(){
            return new Random().Next(100000000,999999999)+""+new Random().Next(100000000,999999999);
        } 
    }
};
