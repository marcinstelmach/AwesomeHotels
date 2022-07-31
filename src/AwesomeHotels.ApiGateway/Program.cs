using Ocelot.DependencyInjection;
using Ocelot.Middleware;

new WebHostBuilder()
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsetting.json", true, true)
            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
            .AddJsonFile("ocelot.json")
            .AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        services.AddOcelot();
    })
    .ConfigureLogging((hostingContext, config) =>
    {
        config.AddConsole();
    })
    .UseIISIntegration()
    .Configure(app =>
    {
        app.UseOcelot().Wait();
    })
    .Build()
    .Run();
