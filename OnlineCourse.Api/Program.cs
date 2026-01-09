using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Minio;
using OnlineCourse.Common.Settings;
using OnlineCourse.Data.Contexts;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Admin;
using OnlineCourse.Service.Admin.Child;
using OnlineCourse.Service.Helpers;
using OnlineCourse.Service.Infrastructure;
using OnlineCourse.Service.Instructor;
using OnlineCourse.Service.Instructor.Course.Interfaces;
using OnlineCourse.Service.Public;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped(typeof(IBaseInfoService<>), typeof(BaseInfoService<>));
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<IMinioService, MinioService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IInstructorCourseService, InstructorCourseService>();


builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connection"));
});

#region Minio Configs

builder.Services.Configure<MinioSetting>(
    builder.Configuration.GetSection("Minio"));

builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<MinioSetting>>().Value;

    return new MinioClient()
        .WithEndpoint(options.Endpoint)
        .WithCredentials(options.AccessKey, options.SecretKey)
        .Build();
});

#endregion

#region Jwt Configs
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // Default to JWT
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var jwtParam = builder.Configuration.GetSection("JwtSettings")
            .Get<JwtSetting>();
        var key = System.Text.Encoding.UTF32.GetBytes(jwtParam?.Key);
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidIssuer = jwtParam.Issuer,
            ValidateIssuer = true,
            ValidAudience = jwtParam.Audience,
            ValidateAudience = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Bearer. : \"Authorization: Bearer { token } \"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });

    c.MapType<string>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Example = new Microsoft.OpenApi.Any.OpenApiString(string.Empty)
    });

    // ?? Basic Authentication
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        Description = "Enter username and password."
    });

    // Ensure IFormFile is correctly recognized
    c.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
