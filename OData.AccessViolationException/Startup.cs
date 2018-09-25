using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using OData.AccessViolationException.Models;

namespace OData.AccessViolationException
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(configuration =>
            {
                // configuration.UseInMemoryDatabase("Example");
                configuration.UseSqlServer("Server=.;Database=ODataIssue;Trusted_Connection=true");
            });

            services.AddOData();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Context context)
        {
            context.Database.Migrate();

            context.People.RemoveRange(context.People);
            context.SaveChanges();

            if (!context.People.Any())
            {
                for (var i = 0; i < 100; i++)
                {
                    context.People.Add(new Person
                    {
                        TeamId = i % 5
                    });
                }

                context.People.Add(new Person
                {
                    TeamId = null
                });

                context.SaveChanges();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(config =>
            {
                config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

                config.MapODataServiceRoute("odata", "odata", GetModel());
            });
        }

        private static IEdmModel GetModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<Person>("People");

            return builder.GetEdmModel();
        }
    }
}
