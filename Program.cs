using CVAPI.Data;
using CVAPI.Data.Seeder;
using CVAPI.Interfaces;
using CVAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using CVAPI.Services;


var builder=WebApplication.CreateBuilder(args);
bool isDevEnv=builder.Environment.IsDevelopment();
builder.Services.AddControllers();

//Services
builder.Services.AddScoped<ICVRep,CVRep>(); 
builder.Services.AddScoped<IUserRep,UserRep>();
builder.Services.AddScoped<ICVVersionRep,CVVersionRep>();
builder.Services.AddScoped<ICVExportRep,CVExportRep>();
builder.Services.AddScoped<ICVModifRep,CVModifRep>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection();
builder.Services.AddScoped<AuthService>();
builder.Services.AddDbContext<DataContext>(options=>{
    options.UseNpgsql(builder.Configuration.GetConnectionString((isDevEnv?"Dev":"Prod")+"ConnectionString"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
