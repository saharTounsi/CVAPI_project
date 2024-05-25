using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace CVAPI.Middlewares {
    public class ErrorMiddleware {

        private readonly RequestDelegate next;
        private readonly ILogger<ErrorMiddleware> logger;

        public ErrorMiddleware(RequestDelegate next,ILogger<ErrorMiddleware> logger){
            this.next=next;
            this.logger=logger;
        }

        public async Task InvokeAsync(HttpContext httpContext){
            try{await next(httpContext);}
            catch (Error error){
                await HandleExceptionAsync(httpContext,error);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context,Error error){
            context.Response.ContentType="application/json";
            context.Response.StatusCode=error.statusCode;
            await context.Response.WriteAsync(error.ToString());
        }
    }

    public class Error:Exception {
        public int statusCode {get;set;}=(int)HttpStatusCode.InternalServerError;
        public int? code {get;set;}
        public string? message {get;set;}
        public string? error {get;set;}

        public Error(){}
        public Error(string message,int code=-1){
            this.code=code;
            this.message=message;
        }

        public override string ToString(){
            var json= new SerializableError(){
                {"satusCode",statusCode},
                {"code",code??-1},
                {"error",error??"true"},
                {"message",message??""},
            };
            return JsonConvert.SerializeObject(json);
        }
    }
}
