using bookystufflocal.api.Helpers.Filters;
using bookystufflocal.domain.Helpers;
using bookystufflocal.domain.Queries.Library;
using bookystufflocal.domain.RepositoryLayer.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace bookystufflocal
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        private readonly ConfigurationManager _configurationManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _configurationManager = ConfigurationManager.CreateForWebAndService(hostingEnvironment.ContentRootPath, hostingEnvironment.EnvironmentName);
            _configuration = _configurationManager.Configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddDb(services);
            AddMediator(services);
            services.AddControllers();
            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<BookyStuffLocalDbContaxt>().Database.Migrate();
        }

        private void AddDb(IServiceCollection services)
        {
            services.AddSingleton(x => _configurationManager.ConnectionString);
            services.AddDbContext<BookyStuffLocalDbContaxt>(
                options => options.UseNpgsql(_configurationManager.ConnectionString.Db, b => b.MigrationsAssembly("bookystufflocal.api")));
        }

        private void AddMediator(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            services.AddMediatR(typeof(AuthorListPagedQuery));
        }
    }
}
