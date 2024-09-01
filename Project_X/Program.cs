using Microsoft.EntityFrameworkCore;
using Project_X.Data;
using Project_X.Mappings;
using Project_X.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
         //options.JsonSerializerOptions.MaxDepth = 64; // Increase the depth if necessary
     });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DSEMLDbContext>(
    options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DSEMLConnectionString")));


builder.Services.AddScoped<IFaqRepository, SQLFaqRepository>();
builder.Services.AddScoped<ISponsorRepository, SQLSponsorRepository>();
builder.Services.AddScoped<IBCategoryRepository, SQLBCategoryRepository>();
builder.Services.AddScoped<IBlogRepository, SQLBlogRepository>();
builder.Services.AddScoped<IContactRepository, SQLContactRepository>();

builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
