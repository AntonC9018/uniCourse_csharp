using Microsoft.Extensions.DependencyInjection;

public sealed class Tests
{
    [Fact]
    public async Task Test1()
    {
        var items = new List<Item>
        {
            new()
            {
                Name = "Anton",
                Price = 40.0f,
            },
            new()
            {
                Name = "Game",
                Price = 1.0f,
            },
            new()
            {
                Name = "Fork",
                Price = 26.0f,
            },
            new()
            {
                Name = "Yellow",
                Price = 90.0f,
            },
            new()
            {
                Name = "lower-case",
                Price = 40.0f,
            },
            new()
            {
                Name = "ignored",
                Price = 30.0f,
            },
        };

        // config -> container
        // mutable -> immutable
        var services = new ServiceCollection();
        var priceCutoffConfig = new PriceCutoffConfig
        {
            MaxPrice = 50.0f,
            MinPrice = 5.0f,
        };
        services.AddSingleton(priceCutoffConfig);
        services.AddSingleton<PriceCutoffService>();
        services.AddSingleton<IRemapNameService>(sp =>
        {
            _ = sp;
            var remap = new RemapNameService(
                new()
                {
                    ["Anton"] = "Mark",
                });
            // var remap = new RemapNameService_RemovePrefix("A");
            return remap;
        });

        services.AddSingleton(sp =>
        {
            var ret = ActivatorUtilities.CreateInstance<ProcessItemService>(
                sp,
                new HashSet<string>
                {
                    "ignored",
                });
            return ret;

        });
        var serviceProvider = services.BuildServiceProvider();

        var processItemService = serviceProvider.GetRequiredService<ProcessItemService>();
        var result = Helper.ProcessItems(
            items,
            processItemService);

        await Verify(result);
    }
}
