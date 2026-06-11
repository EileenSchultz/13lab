using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using lab13.Data;
namespace lab13.Tests;

public class ApiFixture : IAsyncLifetime
{ 
    public ApiFactory Api { get; } = new();
    
    public Task InitializeAsync()
    {
        return Task.Run(async () =>
        { 
            await using var scope = Api.Services.CreateAsyncScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            await dataContext.Database.EnsureDeletedAsync();
            await dataContext.Database.MigrateAsync();
        });
    }

    public Task DisposeAsync()
    {
        return Task.Run(async () =>
        { 
            await using var scope = Api.Services.CreateAsyncScope();
            await scope.ServiceProvider.GetRequiredService<DataContext>().Database.EnsureDeletedAsync();
        });
    }
}