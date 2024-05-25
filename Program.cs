using CVAPI.Data;
using CVAPI.Data.Seeder;
using CVAPI.Interfaces;
using CVAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using CVAPI.Services;
using CVAPI.Middlewares;


var builder=WebApplication.CreateBuilder(args);
bool isDevEnv=builder.Environment.IsDevelopment();
bool useSwagger=builder.Configuration.GetValue("useSwagger",true);

//Services
builder.Services.AddScoped<ICVRep,CVRep>(); 
builder.Services.AddScoped<UserRep>();
builder.Services.AddScoped<ICVVersionRep,CVVersionRep>();
builder.Services.AddScoped<ICVExportRep,CVExportRep>();
builder.Services.AddScoped<ICVModifRep,CVModifRep>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddDbContext<DataContext>(options=>{
    options.UseNpgsql(builder.Configuration.GetConnectionString((isDevEnv?"Dev":"Prod")+"ConnectionString"));
});
if(isDevEnv&&useSwagger) builder.Services.AddSwaggerGen();
AuthService.addAuthentication(builder);
builder.Services.AddControllers();
var corsPolicyName=SecurityService.addCors(builder);
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Create App
var app=builder.Build();

// Configure the HTTP request pipeline.
if(isDevEnv){
    var seeder=new Seeder(app);
    seeder.seedDatabase();
    if(useSwagger){
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
app.UseMiddleware<ErrorMiddleware>();
app.UseCors(corsPolicyName);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
