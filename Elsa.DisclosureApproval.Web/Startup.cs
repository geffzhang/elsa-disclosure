using Elsa;
using Elsa.Activities.UserTask.Extensions;
using Elsa.DisclosureApproval.Web.Activities;
using Elsa.DisclosureApproval.Web.Bookmarks;
using Elsa.DisclosureApproval.Web.Invokers;
using Elsa.DisclosureApproval.Web.Invokers.Interfaces;
using Elsa.DisclosureApproval.Web.Providers.WorkflowContexts;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;

namespace Elsa.DisclosureApproval.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        private IWebHostEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var elsaSection = Configuration.GetSection("Elsa");

            services
                .AddElsa(elsa => elsa
                    .UseEntityFrameworkPersistence(ef =>
                        //ef.Options.
                        ef.UseSqlServer("Server=localhost;Database=EP.Conport;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EP_Elsa_Web"))
                    .AddConsoleActivities()
                    .AddUserTaskActivities()
                    //.AddHttpActivities(elsaSection.GetSection("Server").Bind)
                    //.AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
                    //.AddQuartzTemporalActivities()
                    .AddWorkflowsFrom<Startup>()
                    
                    .AddActivity<DisclosureCreated>()
                    .AddActivity<ContractSigned>()
                    .AddActivity<DisclosureSigned>()
                    .AddActivity<DisclosureSignedBlocking>()
                );
            services.AddWorkflowContextProvider<DisclosureSigningWorkflowContextProvider>();
            services.AddBookmarkProvider<DisclosureSignedBookmarkProvider>();

            services.AddScoped<IDisclosureSignedInvoker, DisclosureSignedInvoker>();

            services.AddElsaApiEndpoints();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                .UseStaticFiles()
                //.UseHttpActivities()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapFallbackToPage("/_Host");
                });
        }
    }
}
