using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_X.Data;
using Project_X.Mappings;
using Project_X.Models.DTO;
using Project_X.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder
        /*.WithOrigins("*")*/
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
         //options.JsonSerializerOptions.MaxDepth = 64; // Increase the depth if necessary
     });
builder.Services.AddHttpContextAccessor();
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
builder.Services.AddScoped<IBImageRepository,BImageRepository>();

builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=>options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();


app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
