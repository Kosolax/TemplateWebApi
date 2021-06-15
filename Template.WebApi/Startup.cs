namespace Template.WebApi
{
    using System.Text.Json.Serialization;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Template.Business;
    using Template.DataAccess;
    using Template.IBusiness;
    using Template.IDataAccess;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContextPool<TemplateContext>(options => options
                .UseMySql(this.Configuration["ConnectionString"]));
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            this.RegisterDataAccess(services);
            this.RegisterBusiness(services);
        }

        private void RegisterBusiness(IServiceCollection services)
        {
            services.AddScoped<IPersonBusiness, PersonBusiness>();
        }

        private void RegisterDataAccess(IServiceCollection services)
        {
            services.AddScoped<IPersonDataAccess, PersonDataAccess>();
        }
    }
}