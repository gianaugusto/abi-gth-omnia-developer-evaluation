namespace Ambev.DeveloperEvaluation.WebApi
{
    using Ambev.DeveloperEvaluation.Application;
    using Ambev.DeveloperEvaluation.Common.HealthChecks;
    using Ambev.DeveloperEvaluation.Common.Logging;
    using Ambev.DeveloperEvaluation.Common.Security;
    using Ambev.DeveloperEvaluation.Common.Validation;
    using Ambev.DeveloperEvaluation.IoC;
    using Ambev.DeveloperEvaluation.ORM;
    using Ambev.DeveloperEvaluation.WebApi.Middleware;
    using Asp.Versioning;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("Starting web application");

                WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
                builder.AddDefaultLogging();

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();

                builder.AddBasicHealthChecks();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.CustomSchemaIds(type => type.FullName);
                });

                builder.Services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true; 
                });

                builder.Services.AddDbContext<DefaultContext>(options =>
                    options.UseNpgsql(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                    )
                );

                builder.Services.AddJwtAuthentication(builder.Configuration);

                builder.RegisterDependencies();

                builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

                builder.Services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssemblies(
                        typeof(ApplicationLayer).Assembly,
                        typeof(Program).Assembly
                    );
                });

                builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

                var app = builder.Build();
                //app.UseMiddleware<ErrorHandlingMiddleware>();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();

                    using var scope = app.Services.CreateScope();
                    var services = scope.ServiceProvider;

                    try
                    {
                        var context = services.GetRequiredService<DefaultContext>();
                        context.Database.Migrate(); // apply database migrations
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error on apply database migrations.");
                    }
                }

                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseBasicHealthChecks();
                app.MapControllers();
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
