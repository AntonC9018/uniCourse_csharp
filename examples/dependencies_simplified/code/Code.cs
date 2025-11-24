public static class Helper
{
    private static Item? ProcessItem(
        Item item,
        float minPriceCutoff,
        float maxPriceCutoff,
        Dictionary<string, string> nameRemap,
        HashSet<string> ignoredNames)
    {
        if (item.Price <= minPriceCutoff)
        {
            return null;
        }
        if (item.Price >= maxPriceCutoff)
        {
            return null;
        }
        string name = item.Name;
        if (ignoredNames.Contains(name))
        {
            return null;
        }

        name = nameRemap.GetValueOrDefault(name, name);
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
        float minPriceCutoff,
        float maxPriceCutoff,
        Dictionary<string, string> nameRemap,
        HashSet<string> ignoredNames)
    {
        var ret = new List<Item>();
        foreach (var item in items)
        {
            var newItem = ProcessItem(
                // item -> прямая зависимость - параметр
                item,
                // конфигурация
                minPriceCutoff,
                maxPriceCutoff,
                nameRemap,
                ignoredNames);
            if (newItem != null)
            {
                ret.Add(newItem);
            }
        }
        return ret;
    }
}

public sealed class Item
{
    public required string Name;
    public required float Price;
}