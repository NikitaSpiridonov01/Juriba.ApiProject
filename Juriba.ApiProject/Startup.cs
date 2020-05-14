using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;
using Juriba.ApiProject.Data;
using Juriba.ApiProject.Services;

namespace Juriba.ApiProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataAccessory, DataAccessory>();
            services.AddScoped<IArticleService, ArticleService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOwin(b => b.UseNancy());
        }
    }
}
