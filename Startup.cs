using clinicaCFP.Models;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Este método es llamado por el tiempo de ejecución. Use este método para agregar servicios al contenedor.
    public void ConfigureServices(IServiceCollection services)
    {
        // ... otras configuraciones ...

        // Registra el servicio ServicioPago
        services.AddScoped<ServicioPago>();

        // ... otras configuraciones ...
    }

    // Este método es llamado por el tiempo de ejecución. Use este método para configurar el pipeline de solicitud HTTP.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }
}