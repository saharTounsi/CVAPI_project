using CVAPI.Models;
using RestSharp;


namespace CVAPI.Data {
    public class CVAnalyser {

        public static readonly List<string> supportedTypes=["application/pdf"];

        public static async Task<CVData> analyseCV(IFormFile formFile){
            string filePath=await CVAnalyser.saveFormFile(formFile);
            /* var client=new RestClient("http://127.0.0.1:5000");
            var request=new RestRequest("/analyse",Method.Post){
                AlwaysMultipartFormData=true,
            };
            var options=new FileParameterOptions{
                DisableFilenameEncoding=true,
                DisableFileNameStar=true,
            };
            request.AddHeader("Content-Type","multipart/form-data");
            //request.AddFile("file", file., refdoc.Title, "application/pdf");
            request.AddObject<IFormFile>(file);
            request.AddParameter("Project", refdoc.Title);  */
            //code to analyse cv
            return new CVData(){profileName=filePath};
        }

        private static async Task<string> saveFormFile(IFormFile formFile){
            if(checkFormFile(formFile)){
                string filePath=Path.Combine("public",formFile.FileName);
                using (Stream fileStream=new FileStream(filePath,FileMode.Create)){
                    await formFile.CopyToAsync(fileStream);
                }
                return filePath;
            }
            else throw new Exception("unsupported file format. CV needs to be in "+string.Join(",",supportedTypes)+".");
        }

        private static bool checkFormFile(IFormFile formFile){
            return supportedTypes.Contains(formFile.ContentType);
        }
    }

    public class CVData {
        public string? profileName {get;set;}
        public string? profileEmail {get;set;}
        public string? profileTel {get;set;}
        //email compétences expériences tel 
    }
};
