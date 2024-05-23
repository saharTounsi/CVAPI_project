using CVAPI.Data;
using CVAPI.Data.Seeder;
using CVAPI.Interfaces;
using CVAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using CVAPI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using CVAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors.Infrastructure;


var builder=WebApplication.CreateBuilder(args);
bool isDevEnv=builder.Environment.IsDevelopment();

//Services
builder.Services.AddControllers();
builder.Services.AddScoped<ICVRep,CVRep>(); 
builder.Services.AddScoped<IUserRep,UserRep>();
builder.Services.AddScoped<ICVVersionRep,CVVersionRep>();
builder.Services.AddScoped<ICVExportRep,CVExportRep>();
builder.Services.AddScoped<ICVModifRep,CVModifRep>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DataContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString((isDevEnv?"Dev":"Prod")+"ConnectionString"));
});
/* builder.Services.AddDataProtection();
builder.Services.AddScoped<AuthService>(); */
var AllowSpecificOrigin="AllowSpecificOrigin";
builder.Services.AddCors(options=>{
    options.AddPolicy(AllowSpecificOrigin,policy=>{
        policy.WithOrigins("*");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});
const string AuthenticationSchema=CookieAuthenticationDefaults.AuthenticationScheme;
builder.Services.AddAuthentication(options=>{
    options.DefaultAuthenticateScheme=AuthenticationSchema;
    options.DefaultSignInScheme=AuthenticationSchema;
    options.DefaultChallengeScheme=AuthenticationSchema;
}).AddCookie();
builder.Services.AddAuthorization();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



if(isDevEnv){
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddSwaggerGen();
}

//Create App
var app=builder.Build();

// Configure the HTTP request pipeline.
if(isDevEnv){
    var seeder=new Seeder(app);
    seeder.seedDatabase();
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors(AllowSpecificOrigin);
app.Run();
