using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

using UkraineAssesment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));
builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"c:\temp-keys\")).SetApplicationName("Assessment"); ;
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins(
            "http://localhost:50550"            
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
