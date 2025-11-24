public static class Helper
{
    private static Item? ProcessItem(
        Item item,
        ProcessItemConfig config)
    {
        if (item.Price <= config.minPriceCutoff)
        {
            return null;
        }
        if (item.Price >= config.maxPriceCutoff)
        {
            return null;
        }
        string name = item.Name;
        if (config.ignoredNames.Contains(name))
        {
            return null;
        }

        name = config.nameRemap.GetValueOrDefault(name, name);
        name = name.ToUpper();

        var newItem = new Item
        {
            Name = name,
            Price = item.Price,
        };
        return newItem;
    }

    // Single Responsibility
    public static List<Item> ProcessItems(
        List<Item> items,
        ProcessItemConfig processItemConfig)
    {
        var ret = new List<Item>();
        foreach (var item in items)
        {
            var newItem = ProcessItem(
                // item -> прямая зависимость - параметр
                item,
                // конфигурация
                processItemConfig);
            if (newItem != null)
            {
                ret.Add(newItem);
            }
        }
        return ret;
    }
}

public readonly record struct ProcessItemConfig(
    float minPriceCutoff,
    float maxPriceCutoff,
    Dictionary<string, string> nameRemap,
    HashSet<string> ignoredNames);

public sealed class Item
{
    public required string Name;
    public required float Price;
}