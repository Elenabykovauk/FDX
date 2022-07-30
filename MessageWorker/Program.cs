using MessageWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        WorkerOptions options = configuration.GetSection("MessageQueue").Get<WorkerOptions>();

        services.AddSingleton(options);

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
