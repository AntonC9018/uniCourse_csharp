using PipelineNS;

var pipeline = new Pipeline();
// pipeline.Steps.Add(new AddDogPipelineStep());
pipeline.Steps.Add(new MaliciousPipelineStep());
pipeline.Steps.Add(new ShowDogPipelineStep());
pipeline.Execute(new GeneralContext());

public sealed class GeneralContext
{
    private readonly Dictionary<string, object> Items = new();

    public void Add<T>(Key<T> key, T value)
    {
        Items.Add(key.Value, value!);
    }

    public T Get<T>(Key<T> key)
    {
        return (T) Items[key.Value];
    }
}

public static class Keys
{
    public static readonly Key<Dog> DogKey = new("dog");
}

public readonly struct Key<T>
{
    public readonly string Value;

    public Key(string value)
    {
        Value = value;
    }
}

public sealed class AddDogPipelineStep : IPipelineStep
{
    public void Execute(GeneralContext context)
    {
        context.Add(Keys.DogKey, new ()
        {
            Name = "Luke",
        });
    }

    public void Introspect() => Console.WriteLine("AddDog");
}

public sealed class MaliciousThing{}

public sealed class MaliciousPipelineStep : IPipelineStep
{
    public void Execute(GeneralContext context)
    {
        var myKey = new Key<MaliciousThing>(Keys.DogKey.Value);
        context.Add(myKey, new MaliciousThing());
    }

    public void Introspect() => Console.WriteLine("AddDog");
}

public sealed class ShowDogPipelineStep : IPipelineStep
{
    public void Execute(GeneralContext context)
    {
        var dog = context.Get(Keys.DogKey);
        Console.WriteLine(dog.Name);
    }

    public void Introspect() => Console.WriteLine("ShowDog");
}

public interface IPipelineStep
{
    void Execute(GeneralContext context);
    void Introspect();
}

public sealed class Pipeline
{
    public List<IPipelineStep> Steps = new();

    public void Execute(GeneralContext context)
    {
        foreach (var s in Steps)
        {
            s.Execute(context);
        }
    }
}

public static class AssociativeArrayDemo
{
    public static void Demo()
    {
        const string HealthKey = "health";
        const string DamageKey = "damage";

        var player = new Dictionary<string, object>();
        player.Add(HealthKey, 5);
        player.Add(DamageKey, 10);

        var enemy = new Dictionary<string, object>();
        enemy.Add(HealthKey, 10);
        enemy.Add(DamageKey, 15);

        var mage = new Dictionary<string, object>();
        mage.Add(HealthKey, new HealthComponent
        {
            Health = 10,
        });
        mage.Add(DamageKey, 15);
        mage.Add("intelligence", 15);

        var list = new List<Dictionary<string, object>>();
        list.Add(player);
        list.Add(enemy);
        list.Add(mage);

    }
    public static Dictionary<string, object>? FindMages(List<Dictionary<string, object>> entities)
    {
        foreach (var e in entities)
        {
            if (e.ContainsKey("intelligence"))
            {
                return e;
            }
        }
        return null;
    }
}

