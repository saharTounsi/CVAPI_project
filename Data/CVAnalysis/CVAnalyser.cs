using System.Text.Json;
using RestSharp;


namespace CVAPI.Data.CVAnalysis {
    public class CVAnalyser {

        public static readonly List<string> supportedTypes=["application/pdf"];
        private static readonly RestClient client=new RestClient("http://7b0a-34-125-43-98.ngrok-free.app");

        public static async Task<CVData> analyseCV(IFormFile formFile){
            Dictionary<string,string> fileProps=await saveFormFile(formFile);
            var fileName=fileProps["fileName"];
            var filePath=fileProps["filePath"];
            //var client=new RestClient("http://127.0.0.1:5000");
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
               var responseBody = response.Content;
               var cvData = JsonSerializer.Deserialize<CVData>(responseBody);
               return cvData;
            }
            else throw new Exception("could not upload file "+response.StatusCode+".");
            //request.AddParameter("Project", refdoc.Title); 
            //code to analyse cv
            
        }

        private static async Task<Dictionary<string,string>> saveFormFile(IFormFile formFile){
            if(checkFormFile(formFile)){
                var props=new Dictionary<string,string>();
                var slices=formFile.FileName.Split(".").ToList();
                var fileExt=slices.Last();
                slices.RemoveAt(slices.Count-1);
                var fileBasename=string.Join(".",slices);
                var fileName=fileBasename+"_"+new Random().Next(100000000,999999999)+"."+fileExt;
                string filePath=Path.Combine("tmp",fileName);
                using (Stream fileStream=new FileStream(filePath,FileMode.Create)){
                    await formFile.CopyToAsync(fileStream);
                }
                props.Add("fileName",fileName);
                props.Add("filePath",filePath);
                return props;
            }
            else throw new Exception("unsupported file format. CV needs to be in "+string.Join(",",supportedTypes)+".");
        }

        private static bool checkFormFile(IFormFile formFile){
            return supportedTypes.Contains(formFile.ContentType);
        }
    }

    public class CVData {
        public string? email {get;set;}
        public string? competences {get;set;}
        public string? experience {get;set;}
        public string? nom {get;set;}
        public string? telephone {get;set;}

    }
};
