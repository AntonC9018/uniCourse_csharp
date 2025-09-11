var pipeline = new Pipeline();

while (true)
{
    Console.Write("Enter step:");
    var input = Console.ReadLine();
    switch (input)
    {
        case "ChangeName":
        {
            var name = Console.ReadLine();
            pipeline.Steps.Add(new ChangeNamePipelineStep
            {
                Name = name ?? "",
            });
            break;
        }
        case "ChangeOwner":
        {
            var ownerName = Console.ReadLine();
            var owner = new Owner
            {
                Name = ownerName ?? "",
            };
            pipeline.Steps.Add(new ChangeOwnerPipelineStep
            {
                Owner = owner,
            });
            break;
        }
        case "PrintOwners":
        {
            pipeline.Steps.Add(new PrintOwnersPipelineStep());
            break;
        }
        case "PrintDog":
        {
            pipeline.Steps.Add(new PrintDogPipelineStep());
            break;
        }
        case "Introspect":
        {
            foreach (var s in pipeline.Steps)
            {
                s.Introspect();
            }
            break;
        }
        case "Execute":
        {
            pipeline.Execute(new Context
            {
                Dog = new()
                {
                    Name = "Luke",
                },
            });
            break;
        }
        default:
        {
            return;
        }
    }
}

// pipeline.Steps.RemoveAll(x => x is PrintOwnersPipelineStep);
//

// var context = new Context
// {
//     Dog = new Dog
//     {
//         Name = "Luke",
//     },
// };
// pipeline.Execute(context);

// Functions.Pipeline1(new Context
// {
//     Dog = new Dog
//     {
//         Name = "Lucy",
//     },
// });
//

return;

sealed class Context
{
    public List<Owner> PreviousOwners = [];
    public required Dog Dog;
}

interface IPipelineStep
{
    void Execute(Context context);
    void Introspect();
}

sealed class ChangeOwnerPipelineStep : IPipelineStep
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

sealed class ChangeNamePipelineStep : IPipelineStep
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

sealed class PrintOwnersPipelineStep : IPipelineStep
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

sealed class PrintDogPipelineStep : IPipelineStep
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

sealed class Pipeline
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

static class Functions
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

sealed class Dog
{
    public required string Name;
    public Owner? Owner = null;
}
sealed class Owner
{
    public required string Name;
}

// Context
// sealed class Context
// {
// }