using PipelineNS;

var pipeline = new Pipeline();
pipeline.Steps.Add(Wrap(new ChangeNamePipelineStep
{
    Name = "Lucy",
}));
pipeline.Steps.Add(Wrap(new PrintDogPipelineStep
{
}));

IPipelineStep Wrap(IPipelineStep step)
{
    return new PrintStep_AndExecute_PipelineStep(step);
}

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
