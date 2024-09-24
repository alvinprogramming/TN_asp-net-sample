
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sample.Business.IServices.Admin;
using Sample.Data.Entities;
using Sample.Validator.IValidators.Admin;
using Sample.Validator.Validators.Admin;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //cors

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            // add services to the container
            builder.Services.AddHttpContextAccessor();

            // NOTE : For each service to be used, add .Services to the program.cs
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IDepartmentService, DepartmentService>();

            builder.Services.AddTransient<IUserValidator, UserValidator>(); // need to declare service (validator) to be used 
            builder.Services.AddTransient<IDepartmentValidator, DepartmentValidator>();

            //Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();


            // NOTE : Pre-requisite for Login Authentication/Authorization (1/2).
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            // NOTE : Pre-requisite for Login Authentication/Authorization (2/2).
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateLifetime = true
                };
            });


            // NOTE : Pre-requisite for database connection. 
            builder.Services.AddDbContext<SampleDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Sample_DBConn"));
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (!app.Environment.IsDevelopment()) // can this be else instead?
            {
                app.UseHttpsRedirection();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
