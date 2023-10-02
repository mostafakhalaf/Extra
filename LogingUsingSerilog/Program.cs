using Serilog;


try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
    logger.Information("Application Starting");
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{

    Log.Fatal(ex, "The application failed to start!");
}
finally
{
    Log.CloseAndFlush();
}