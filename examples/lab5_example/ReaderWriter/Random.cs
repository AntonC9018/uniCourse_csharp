using Microsoft.Extensions.DependencyInjection;

public interface IObjectGenerator<T>
{
    public IEnumerable<T> Generate(int count);
}

public interface IObjectGeneratorFactory
{
    public IObjectGenerator<T> Create<T>();
}

public sealed class ItemGenerator : IObjectGenerator<Item>
{
    public IEnumerable<Item> Generate(int count)
    {
        var rand = Random.Shared;
        for (int i = 0; i < count; i++)
        {
            yield return new Item
            {
                Name = "Item" + rand.Next(1, 1000),
                Price = rand.Next(10, 500),
                Delivery = (DeliveryMode) rand.Next(0, 2),
                DeliveryDate = DateTime.Now.AddDays(rand.Next(1, 30)),
            };
        }
    }
}

public sealed class ItemGeneratorFactory : IObjectGeneratorFactory
{
    private readonly IServiceProvider _sp;

    public ItemGeneratorFactory(IServiceProvider sp)
    {
        _sp = sp;
    }

    public IObjectGenerator<T> Create<T>()
    {
        return _sp.GetRequiredService<IObjectGenerator<T>>();
    }
}

