using System.Collections.Immutable;

public readonly struct ChangeNameByLookupConfig
{
    public required Dictionary<string, string> Map { get; init; }
}

public readonly struct PriceLargerThanConfig
{
    public required int PriceThreshold { get; init; }
}

public interface IItemTransformer
{
    Item Transform(Item item);
}

public sealed class PrefixItemTransformer : IItemTransformer
{
    private readonly string _prefix;

    public PrefixItemTransformer(string prefix)
    {
        _prefix = prefix;
    }

    public Item Transform(Item item)
    {
        item.Name = _prefix + item.Name;
        return item;
    }
}

public readonly struct ComplexTransformationConfig
{
    public required List<IItemTransformer> Transformers { get; init; }
}

public static class Processing
{
    public static IEnumerable<Item> ChangeNameByLookup(
        IEnumerable<Item> item,
        ChangeNameByLookupConfig config)
    {
        foreach (var it in item)
        {
            if (config.Map.TryGetValue(it.Name, out var newName))
            {
                it.Name = newName;
            }
            yield return it;
        }
    }

    public static IEnumerable<Item> PriceLargerThan(
        IEnumerable<Item> items,
        PriceLargerThanConfig config)
    {
        foreach (var it in items)
        {
            if (it.Price > config.PriceThreshold)
            {
                yield return it;
            }
        }
    }

    public static IEnumerable<Item> ComplexTransformation(
        IEnumerable<Item> items,
        ComplexTransformationConfig config)
    {
        foreach (var item in items)
        {
            var transformedItem = item;
            foreach (var transformer in config.Transformers)
            {
                transformedItem = transformer.Transform(transformedItem);
            }
            yield return transformedItem;
        }
    }
}

public interface ITransformationStep<T>
{
    IEnumerable<T> Apply(IEnumerable<T> items);
}

public sealed class ChangeByNameLookupAdapter : ITransformationStep<Item>
{
    private readonly ChangeNameByLookupConfig _config;

    public ChangeByNameLookupAdapter(ChangeNameByLookupConfig config)
    {
        _config = config;
    }

    public IEnumerable<Item> Apply(IEnumerable<Item> items)
    {
        return Processing.ChangeNameByLookup(items, _config);
    }
}

public sealed class PriceLargerThanAdapter : ITransformationStep<Item>
{
    private readonly PriceLargerThanConfig _config;

    public PriceLargerThanAdapter(PriceLargerThanConfig config)
    {
        _config = config;
    }

    public IEnumerable<Item> Apply(IEnumerable<Item> items)
    {
        return Processing.PriceLargerThan(items, _config);
    }
}

public sealed class ComplexTransformationAdapter : ITransformationStep<Item>
{
    private readonly ComplexTransformationConfig _config;

    public ComplexTransformationAdapter(ComplexTransformationConfig config)
    {
        _config = config;
    }

    public IEnumerable<Item> Apply(IEnumerable<Item> items)
    {
        return Processing.ComplexTransformation(items, _config);
    }
}

public interface IItemTransformationPipeline
{
    public IEnumerable<Item> Run(IEnumerable<Item> items);
}

public sealed class SingleStepTransformationPipeline : IItemTransformationPipeline
{
    private readonly PriceLargerThanAdapter _transformation;

    public SingleStepTransformationPipeline(PriceLargerThanAdapter transformation)
    {
        _transformation = transformation;
    }

    public IEnumerable<Item> Run(IEnumerable<Item> items)
    {
        items = _transformation.Apply(items);
        return items;
    }
}

public sealed class ListItemTransformationPipeline : IItemTransformationPipeline
{
    private readonly ImmutableArray<ITransformationStep<Item>> _steps;

    public ListItemTransformationPipeline(IEnumerable<ITransformationStep<Item>> steps)
    {
        _steps = [.. steps];
    }

    public IEnumerable<Item> Run(IEnumerable<Item> items)
    {
        foreach (var step in _steps)
        {
            items = step.Apply(items);
        }
        return items;
    }
}

public sealed class DoNothingItemTransformationPipeline : IItemTransformationPipeline
{
    public static readonly DoNothingItemTransformationPipeline Instance = new();
    public IEnumerable<Item> Run(IEnumerable<Item> items)
    {
        return items;
    }
}