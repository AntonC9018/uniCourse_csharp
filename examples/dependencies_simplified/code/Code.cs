public static class Helper
{
    private static Item? ProcessItem(
        Item item,
        ProcessItemConfig config)
    {
        if (!config.priceCutoff.AllowsPrice(item.Price))
        {
            return null;
        }
        string name = item.Name;
        if (config.ignoredNames.Contains(name))
        {
            return null;
        }

        name = config.nameRemap.RemapName(name);
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

public sealed class RemapNameService
{
    private readonly Dictionary<string, string> _nameRemap;

    public RemapNameService(Dictionary<string, string> nameRemap)
    {
        _nameRemap = nameRemap;
    }

    public string RemapName(string itemName)
    {
        var name = _nameRemap.GetValueOrDefault(itemName, itemName);
        return name;
    }
}

public sealed class PriceCutoffService
{
    private readonly float _minPrice;
    private readonly float _maxPrice;

    public PriceCutoffService(
        float minPrice,
        float maxPrice)
    {
        _minPrice = minPrice;
        _maxPrice = maxPrice;
    }

    public bool AllowsPrice(float price)
    {
        if (price <= _minPrice)
        {
            return false;
        }
        if (price >= _maxPrice)
        {
            return false;
        }
        return true;
    }
}

public readonly record struct ProcessItemConfig(
    PriceCutoffService priceCutoff,
    RemapNameService nameRemap,
    HashSet<string> ignoredNames);

public sealed class Item
{
    public required string Name;
    public required float Price;
}