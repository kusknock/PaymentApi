using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaymentApi.Configuration;
using PaymentApi.DbLogger;
using PaymentApi.Middlewares;
using PaymentApi.Models;
using PaymentApi.Services;
using PaymentClassLibrary.Transport;
using System;
using System.IO;
using System.Reflection;
using PaymentApi.DbLogger.Extensions;

namespace PaymentApi
{
    /// <summary>
    /// ������ ����������� ��� ������� ����������
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="configuration">��������� appsettings.json</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ������ ��� ���������������� ���������� �� appsettings.json
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();

            // ���������� ��������� �� application.json
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<TinkoffSettings>(Configuration.GetSection("TinkoffSettings"));
            services.Configure<AlfaBankSettings>(Configuration.GetSection("AlfaBankSettings"));

            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddControllers();

            // DI ��������� � UserService � RegisterService
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILogRepository, LogRepository>();

            services.AddLogging(log =>
            {
                log.AddDbLogger();
            });

            services.AddTransient<GatewayClient>();

            services.AddSwaggerGen(swagger =>
            {
                // swagger ��������� ������ � ��������
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Payment API with JWT",
                    Description = "API ��� ������ � ���������� ���������"
                });
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "������� 'Bearer' [������] � ����� token �������� ��������.\r\n\r\n������: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                swagger.IncludeXmlComments(xmlPath);
            });

            //�������� �������� ���������� ����������� JwtBearer
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings").Get<AppSettings>().Secret)),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentApi v1"));
            }
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<RequestLogMiddleware>(); // ����������� �������� � �������

            app.UseMiddleware<JwtMiddleware>(); // �������� JWT ����� ���������� ������� �����������

            app.UseMiddleware<CheckIpMiddleware>(); // �������� ip � ����� ������

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
