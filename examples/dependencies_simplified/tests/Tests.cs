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
        };

        var result = Helper.ProcessItems(items,
            minPriceCutoff: 5.0f,
            maxPriceCutoff: 50.0f,
            nameRemap: new()
            {
                ["Anton"] = "Mark",
            });

        await Verify(result);
    }
}
