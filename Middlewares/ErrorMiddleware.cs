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
            catch (Exception ex){
                logger.LogError($"An error occurred: {ex}");
                await HandleExceptionAsync(httpContext,ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context,Exception exception){
            context.Response.ContentType="application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(new Error(){
                statusCode=context.Response.StatusCode,
                message=exception.Message,
            }.ToString());
        }
    }

    public class Error {
        public int statusCode {get;set;}
        public string? message {get;set;}

        public override string ToString(){
            var json= new SerializableError(){
                {"satusCode",statusCode},
                {"message",message??""},
            };
            return JsonConvert.SerializeObject(json);
        }
    }
}
