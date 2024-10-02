using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Plaza_DSML.Data;
using Plaza_DSML.Mapping;
using Plaza_DSML.Repositories;
using Plaza_DSML.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});


builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<ISponsorRepository, SponsorRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddAutoMapper(typeof(AutoMappingProfile));


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); ;
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Resources"
});

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
