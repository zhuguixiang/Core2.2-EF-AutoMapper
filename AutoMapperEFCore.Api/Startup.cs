using AutoMapper;
using AutoMapperEFCore.Common;
using AutoMapperEFCore.Core;
using AutoMapperEFCore.Dto;
using AutoMapperEFCore.Model;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;

namespace AutoMapperEFCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //允许跨域调用
            services.AddCors();
            //注册Core 2.2
            services
                .AddMvc(options =>
                {
                    options.Filters.Add<HttpGlobalExceptionFilter>();
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //log4net
            //必须先注册，如果被其他服务提前调用会报错
            var repository = LogManager.CreateRepository("NETCoreLogRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            LogService.Instance.SetRepository(repository);

            //redis
            var redisConnection = Configuration.GetSection("RedisCache").GetValue(typeof(string), "ConnectionString").ToString();
            RedisHelper.Instance.Connect(redisConnection);
            services.AddSingleton<RedisHelper>(RedisHelper.Instance);

            //注册服务
            services.AddSingleton<StudentManage>();

            //注册AutoMapper
            services.AddAutoMapper(typeof(StudentProfile));

            //注册数据库上下文
            //方法1：需要在manage中注入SqlDbContext
            //var optionsBuilder = new DbContextOptionsBuilder<SqlDbContext>()
            //    .UseSqlServer(Configuration.GetConnectionString("default"))
            //    .Options;
            //services.AddSingleton(options => new SqlDbContext(optionsBuilder));

            //方法2：需要在SqlDbContext提前创建连接，并且不需要在manage中注入SqlDbContext
            SqlDbContext.ConnectionString = Configuration.GetConnectionString("default");
            services.AddDbContextPool<SqlDbContext>(options => options.UseSqlServer(SqlDbContext.ConnectionString));

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                new Info
                {
                    Title = "API Doc",
                    Version = "v1",
                    Description = ""
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "AutoMapperEFCore.Api.xml");
                c.IncludeXmlComments(xmlPath);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });
        }
    }
}
