using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddlewares;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegisteration
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var DataSeedingObj = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await DataSeedingObj.DataSeed();
        }

        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddlewares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
