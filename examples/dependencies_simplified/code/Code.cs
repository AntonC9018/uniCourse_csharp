using System.Diagnostics;

public static class Helper
{
    // Single Responsibility
    public static List<Item> ProcessItems(
        List<Item> items,
        ProcessItemService processItemService)
    {
        var ret = new List<Item>();
        foreach (var item in items)
        {
            var newItem = processItemService.Process(item);
            if (newItem != null)
            {
                ret.Add(newItem);
            }
        }
        return ret;
    }
}

// Dependency Injection
// Inversion of Control
// IoC container - DI container
public sealed class ProcessItemService
{
    private readonly PriceCutoffService _priceCutoff;
    private readonly IRemapNameService _nameRemap;
    private readonly HashSet<string> _ignoredNames;

    public ProcessItemService(
        PriceCutoffService priceCutoff,
        IRemapNameService nameRemap,
        HashSet<string> ignoredNames)
    {
        _priceCutoff = priceCutoff;
        _nameRemap = nameRemap;
        _ignoredNames = ignoredNames;
    }

    public Item? Process(Item item)
    {
        if (!_priceCutoff.AllowsPrice(item.Price))
        {
            return null;
        }
        string name = item.Name;
        if (_ignoredNames.Contains(name))
        {
            return null;
        }

        // polymorphism
        name = _nameRemap.RemapName(name);
        name = name.ToUpper();

        var newItem = new Item
        {
            Name = name,
            Price = item.Price,
        };
        return newItem;
    }
}

public readonly struct RemapNameService_TaggedUnion
{
    private readonly object _impl;

    public RemapNameService_TaggedUnion(RemapNameService remapNameService)
    {
        _impl = remapNameService;
    }
    public RemapNameService_TaggedUnion(RemapNameService_RemovePrefix removePrefix)
    {
        _impl = removePrefix;
    }

    public string RemapName(string name)
    {
        if (_impl is RemapNameService a)
        {
            return a.RemapName(name);
        }
        if (_impl is RemapNameService_RemovePrefix b)
        {
            return b.RemapName(name);
        }
        Debug.Fail("Impossible");
        return null;
    }
}

public interface IRemapNameService
{
    string RemapName(string itemName);
}

public sealed class RemapNameService : IRemapNameService
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

public sealed class RemapNameService_RemovePrefix : IRemapNameService
{
    private readonly string _removedPrefix;

    public RemapNameService_RemovePrefix(string removedPrefix)
    {
        _removedPrefix = removedPrefix;
    }

    public string RemapName(string itemName)
    {
        if (itemName.StartsWith(_removedPrefix))
        {
            return itemName[(_removedPrefix.Length) ..];
        }
        return itemName;
    }
}

public sealed class PriceCutoffConfig
{
    public required float MinPrice { get; set; }
    public required float MaxPrice { get; set; }
}
public sealed class PriceCutoffService
{
    private readonly PriceCutoffConfig _config;

    public PriceCutoffService(PriceCutoffConfig config)
    {
        _config = config;
    }

    public bool AllowsPrice(float price)
    {
        if (price <= _config.MinPrice)
        {
            return false;
        }
        if (price >= _config.MaxPrice)
        {
            return false;
        }
        return true;
    }
}

public sealed class Item
{
    public required string Name;
    public required float Price;
}