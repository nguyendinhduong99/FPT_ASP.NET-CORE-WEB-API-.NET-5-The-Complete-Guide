using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using my_book.Data;
using my_book.Data.Services;
using my_book.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book
{
    public class Startup
    {
        public string ConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefautConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //Configure DBContext  with SQL 
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

            //Add đường lối: Swagger -> API -> View Models -> DB
            services.AddTransient<BooksService>();
            services.AddTransient<PublishersService>();
            services.AddTransient<AuthorsService>();
            services.AddTransient<LogsService>();

            //Config API Version
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true; //cho giả định khi version ko xác định

                //config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header"); 
                //config.ApiVersionReader = new MediaTypeApiVersionReader();
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "my_book_update title", Version = "v2" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "my_book v2_update_ui"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Exception Handling
            //app.ConfigureBuildInExceptionHandler();
            //app.CustomConfigureBuildInExceptionHandler();
            app.ConfigureBuildInExceptionHandler(loggerFactory);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            //chỉ dành cho test mới ban đầu, tạo data test thử thôi, sau có các quan hệ các bảng thì disable nó đi
            //AppDbInitializer.Seed(app);
        }
    }
}
