using RestSharp;
using CVAPI.Middlewares;


namespace CVAPI.Data.CVAnalysis {
    public class CVAnalyser {

        public static readonly List<string> supportedTypes=["application/pdf"];
        private static readonly RestClient client=new RestClient("http://127.0.0.1:5000");

        public static async Task<CVData> analyseCV(IFormFile formFile){
            Dictionary<string,string> fileProps=await saveFormFile(formFile);
            var fileName=fileProps["fileName"];
            var filePath=fileProps["filePath"];
            //var client=new RestClient("http://127.0.0.1:5000");
            var request=new RestRequest("/analyse",Method.Post){
                AlwaysMultipartFormData=true,
            };
            request.AddHeader("Content-Type","multipart/form-data");
            request.AddFile("file",filePath,ContentType.Binary,new FileParameterOptions{
                DisableFilenameEncoding=true,
                DisableFileNameStar=true,
            });
            var response=await client.PostAsync(request);
            if(response.IsSuccessStatusCode){
                return new CVData(){profileName=fileName,profileEmail=filePath};
            }
            else throw new Error("could not upload file "+response.StatusCode+".");
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
            else throw new Error("unsupported file format. CV needs to be in "+string.Join(",",supportedTypes)+".");
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
