namespace D2WeaponReportWeb
{
    using System.IO;
    using System.Reflection;

    using DestinyLib;
    using DestinyLib.Database;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
#if DEBUG
            this.InitializeEnvironment();
#endif

            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            this.ConfigureDestinyLib(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink(); // TODO: SETUP BROWSER LINK
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseDefaultFiles(); // added to compile Typescript?
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureDestinyLib(IServiceCollection services)
        {
            var dbPath = new FileInfo(LibEnvironment.GetDatabaseFilePath("world_sql_content"));
            var worldSqlContent = new WorldSqlContent(connectionString: Database.MakeConnectionString(dbPath));
            var worldSqlContentProvider = new WorldSqlContentProvider(worldSqlContent, new ProviderOptions { EnableCaching = true });

            services.AddSingleton<WorldSqlContentProvider>(worldSqlContentProvider);
        }

#if DEBUG
        public void InitializeEnvironment()
        {
            // TODO: This was causing too many problems so i disabled the self-init.

            //var dllDirectoryInfo = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            //var runTask = DestinyLib.Scenarios.InitializeEnvironmentScenario.Run(rootDirectoryInfo: dllDirectoryInfo, deleteDirectory: false);
            //runTask.Wait();
        }
#endif
    }
}
