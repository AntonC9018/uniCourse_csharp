using System.Globalization;
using System.Text.Json;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var services = new ServiceCollection();

services.AddOptions<FormatFileOptions>(FormatFileOptions.ReaderKey);
services.AddOptions<FormatFileOptions>(FormatFileOptions.WriterKey);
services
    .AddOptions<RandomReaderFactoryOptions>()
    .Configure(x =>
    {
        x.GeneratedCount = 3;
    });
services.AddSingleton<IItemTransformer>(new PrefixItemTransformer("my-prefix-"));
services.AddSingleton(new PriceLargerThanAdapter(new()
{
    PriceThreshold = 100,
}));
services.AddSingleton<IObjectGenerator<Item>, ItemGenerator>();
services.AddSingleton<IObjectGeneratorFactory, ItemGeneratorFactory>();
services.AddSingleton<ITransformationStep<Item>>(sp => sp.GetRequiredService<PriceLargerThanAdapter>());
services.AddSingleton<ITransformationStep<Item>>(new ChangeByNameLookupAdapter(new()
{
    Map = new()
    {
        ["Peter"] = "Mark",
    },
}));
services.AddSingleton<ITransformationStep<Item>>(sp =>
{
    var config = new ComplexTransformationConfig
    {
        Transformers = sp.GetServices<IItemTransformer>().ToList(),
    };
    return new ComplexTransformationAdapter(config);
});
services.AddSingleton<SingleStepTransformationPipeline>();

// If there are multiple options for these in the program,
// should make a builder that adds the steps as keyed services.
// Then, make this keyed as well.
// Then, make an "add keyed" method in the orchestrator.
// Currently, this gets all the steps in the system.
services.AddSingleton<ListItemTransformationPipeline>();

await using var serviceProvider = services.BuildServiceProvider();

await Run(builder =>
{
    builder.UsePipeline<SingleStepTransformationPipeline>();
    builder.UseRandomReader(opts =>
    {
        opts.GeneratedCount = 10;
    });
    builder.UseFileWriter(ConfigureCsv);
});
await Run(builder =>
{
    builder.UsePipeline<ListItemTransformationPipeline>();
    builder.UseFileReader(ConfigureCsv);
    builder.UseFileWriter(ConfigureJson);
});
await Run(builder =>
{
    builder.UseFileReader(ConfigureJson);
    builder.UseLoggingWriter();
});

async Task Run(Action<OrchestrationBuilder> configure)
{
    // ReSharper disable once AccessToDisposedClosure
    await using var scope = serviceProvider.CreateAsyncScope();
    var builder = scope.CreateReadWritePipelineBuilder();
    configure(builder);
    var executor = builder.Build();
    await executor.Execute();
}

// These could also be in the di container if needed.
void ConfigureCsv(FormatFileOptions opts)
{
    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
    const string csvFileName = "output.csv";
    opts.Format = Format.CreateCsv(csvConfig);
    opts.FileName = csvFileName;
}
void ConfigureJson(FormatFileOptions opts)
{
    var jsonConfig = new JsonSerializerOptions
    {
        WriteIndented = true,
    };
    const string jsonFileName = "output.json";
    opts.Format = Format.CreateJson(jsonConfig);
    opts.FileName = jsonFileName;
}
