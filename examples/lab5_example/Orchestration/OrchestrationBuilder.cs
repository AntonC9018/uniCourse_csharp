using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public sealed class OrchestrationBuilderModel
{
    public IReaderFactory? Reader { get; set; }
    public IWriterFactory? Writer { get; set; }
    // Could do the same system with factories,
    // or make a builder for these.
    public IItemTransformationPipeline? Transformation { get; set; }
}

public sealed class OrchestrationBuilder
{
    public required IServiceProvider ServiceProvider { get; init; }
    public OrchestrationBuilderModel Model { get; } = new();

    public OrchestrationExecutor Build()
    {
        if (Model.Reader == null)
        {
            throw new InvalidOperationException("Reader factory is not set.");
        }
        if (Model.Writer == null)
        {
            throw new InvalidOperationException("Writer factory is not set.");
        }

        var reader = Model.Reader.Create();
        var writer = Model.Writer.Create();
        var transformation = Model.Transformation ?? DoNothingItemTransformationPipeline.Instance;

        return new OrchestrationExecutor(reader, writer, transformation);
    }

    public Task Execute()
    {
        var executor = Build();
        return executor.Execute();
    }
}

public static class OrchestratorBuilderExtensions
{
    public static OrchestrationBuilder BuildReadWritePipeline(this IServiceScope scope)
    {
        return new OrchestrationBuilder
        {
            ServiceProvider = scope.ServiceProvider,
        };
    }

    public static void UseFileReader(this OrchestrationBuilder b, Action<FormatFileOptions> configure)
    {
        if (b.Model.Reader != null)
        {
            throw new InvalidOperationException("Reader factory is already set.");
        }

        b.Model.Reader = b.ServiceProvider
            .GetRequiredService<FileReaderFactory>();
        var snapshot = b.ServiceProvider
            .GetRequiredService<IOptionsSnapshot<FormatFileOptions>>();
        var options = snapshot.Get(FormatFileOptions.ReaderKey);
        configure(options);
    }

    public static void UseFileWriter(this OrchestrationBuilder b, Action<FormatFileOptions> configure)
    {
        if (b.Model.Writer != null)
        {
            throw new InvalidOperationException("Writer factory is already set.");
        }

        b.Model.Writer = b.ServiceProvider
            .GetRequiredService<FileWriterFactory>();
        var snapshot = b.ServiceProvider
            .GetRequiredService<IOptionsSnapshot<FormatFileOptions>>();
        var options = snapshot.Get(FormatFileOptions.WriterKey);
        configure(options);
    }

    // Potentially specified in a different user project.
    public static void UseRandomReader(this OrchestrationBuilder b, Action<RandomReaderFactoryOptions> configure)
    {
        if (b.Model.Reader != null)
        {
            throw new InvalidOperationException("Reader factory is already set.");
        }

        b.Model.Reader = b.ServiceProvider
            .GetRequiredService<RandomReaderFactory>();
        var snapshot = b.ServiceProvider
            .GetRequiredService<IOptionsSnapshot<RandomReaderFactoryOptions>>();
        var options = snapshot.Value;
        configure(options);
    }

    public static void UseLoggingWriter(this OrchestrationBuilder b)
    {
        if (b.Model.Writer != null)
        {
            throw new InvalidOperationException("Writer factory is already set.");
        }

        b.Model.Writer = new LoggingWriterFactory();
    }

    public static void UsePipeline<TPipeline>(this OrchestrationBuilder b)
        where TPipeline : IItemTransformationPipeline
    {
        b.Model.Transformation = b.ServiceProvider.GetRequiredService<TPipeline>();
    }
}