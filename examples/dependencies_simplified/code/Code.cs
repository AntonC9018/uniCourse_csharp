public static class Helper
{
    public static List<Item> ProcessItems(
        List<Item> items,
        float minPriceCutoff,
        float maxPriceCutoff,
        Dictionary<string, string> nameRemap)
    {
        var ret = new List<Item>();
        foreach (var item in items)
        {
            if (item.Price <= minPriceCutoff)
            {
                continue;
            }
            if (item.Price >= maxPriceCutoff)
            {
                continue;
            }
            string name = item.Name;
            name = nameRemap.GetValueOrDefault(name, name);
            name = name.ToUpper();

            ret.Add(new Item
            {
                Name = name,
                Price = item.Price,
            });
        }
        return ret;
    }
}

public sealed class Item
{
    public required string Name;
    public required float Price;
}