public sealed class Context
{
    public List<Owner> PreviousOwners = [];
    public required Dog Dog;
}

public interface IPipelineStep
{
    void Execute(Context context);
    void Introspect();
}

// Wrapper / Proxy
public sealed class PrintStep_AndExecute_PipelineStep : IPipelineStep
{
    public readonly IPipelineStep Step;

    public PrintStep_AndExecute_PipelineStep(IPipelineStep step)
    {
        Step = step;
    }

    public void Execute(Context context)
    {
        Step.Introspect();
        Step.Execute(context);
    }

    public void Introspect()
    {
        Console.WriteLine("PrintName_AndExecute_ThingBelowMe");
        Step.Introspect();
    }
}

public sealed class ChangeOwnerPipelineStep : IPipelineStep
{
    public required Owner? Owner;

    public void Execute(Context context)
    {
        Functions.ChangeOwner_Adapter(context, Owner);
    }

    public void Introspect()
    {
        Console.WriteLine($"ChangeOwner({Owner?.Name})");
    }
}

public sealed class ChangeNamePipelineStep : IPipelineStep
{
    public required string Name;

    public void Execute(Context context)
    {
        Functions.ChangeName_Adapter(context, Name);
    }

    public void Introspect()
    {
        Console.WriteLine($"ChangeName({Name})");
    }
}

public sealed class PrintOwnersPipelineStep : IPipelineStep
{
    public void Execute(Context context)
    {
        Functions.PrintOwners_Adapter(context);
    }

    public void Introspect()
    {
        Console.WriteLine("PrintOwners");
    }
}

public sealed class PrintDogPipelineStep : IPipelineStep
{
    public void Execute(Context context)
    {
        Functions.PrintDog_Adapter(context);
    }
    public void Introspect()
    {
        Console.WriteLine("PrintDog");
    }
}

public sealed class Pipeline
{
    public List<IPipelineStep> Steps = new();

    public void Execute(Context context)
    {
        foreach (var s in Steps)
        {
            s.Execute(context);
        }
    }
}

public static class Functions
{
    public static Pipeline CreatePipeline()
    {
        var pipeline = new Pipeline();
        var owner1 = new Owner
        {
            Name = "Anton",
        };
        pipeline.Steps.Add(new ChangeOwnerPipelineStep
        {
            Owner = owner1,
        });

        pipeline.Steps.Add(new ChangeNamePipelineStep
        {
            Name = "Lucy",
        });
        var owner2 = new Owner
        {
            Name = "Alex",
        };
        pipeline.Steps.Add(new ChangeOwnerPipelineStep
        {
            Owner = owner2,
        });
        pipeline.Steps.Add(new PrintOwnersPipelineStep());
        pipeline.Steps.Add(new PrintDogPipelineStep());
        return pipeline;
    }

    public static void Pipeline1(Context context)
    {
        ChangeName_Adapter(context, "Lucy");

        {
            var owner1 = new Owner
            {
                Name = "Anton",
            };
            ChangeOwner_Adapter(context, owner1);
        }
        {
            var owner2 = new Owner
            {
                Name = "Alex",
            };
            ChangeOwner_Adapter(context, owner2);
        }

        PrintDog_Adapter(context);
        PrintOwners_Adapter(context);
    }

    public static void ChangeName_Adapter(Context context, string name)
    {
        ChangeName(context.Dog, name);
    }

    public static void ChangeName(Dog dog, string name)
    {
        Console.WriteLine("Changing Name");
        dog.Name = name;
    }

    public static void ChangeOwner_Adapter(
        Context context,
        Owner? newOwner)
    {
        ChangeOwner(context.Dog, context.PreviousOwners, newOwner);
    }

    // Adapter
    public static void ChangeOwner(
        Dog dog,
        List<Owner> previousOwners,
        Owner? newOwner)
    {
        Console.WriteLine("Changing Owner");
        if (dog.Owner != null)
        {
            previousOwners.Add(dog.Owner);
        }
        dog.Owner = newOwner;
    }

    public static void PrintOwners_Adapter(Context context)
    {
        PrintOwners(context.PreviousOwners);
    }

    public static void PrintOwners(List<Owner> previousOwners)
    {
        Console.WriteLine("Previous Owners:");
        foreach (var o in previousOwners)
        {
            Console.WriteLine(o.Name);
        }
    }

    public static void PrintDog_Adapter(Context context)
    {
        PrintDog(context.Dog);
    }

    public static void PrintDog(Dog dog)
    {
        Console.WriteLine("Dog Information:");
        Console.WriteLine($"Name: {dog.Name}");
        if (dog.Owner != null)
        {
            Console.WriteLine($"Owner: {dog.Owner.Name}");
        }
    }
}

public sealed class Dog
{
    public required string Name;
    public Owner? Owner = null;
}
public sealed class Owner
{
    public required string Name;
}
