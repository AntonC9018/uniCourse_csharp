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

        var cutoffService = new PriceCutoffService(
            minPrice: 5.0f,
            maxPrice: 50.0f);
        var remap = new RemapNameService(
            new()
            {
                ["Anton"] = "Mark",
            });
        var config = new ProcessItemConfig(
            cutoffService,
            remap,
            ["ignored"]);
        var processItemService = new ProcessItemService(config);
        var result = Helper.ProcessItems(
            items,
            processItemService);

        await Verify(result);
    }
}
