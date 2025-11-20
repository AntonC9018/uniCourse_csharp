public static class Orchestration
{
    public static async Task WholeProcess(
        IReader reader,
        IWriter writer,
        IItemTransformationPipeline pipeline)
    {
        var items = await reader.Read<Item>();
        var processedItems = pipeline.Run(items);
        await writer.Write(processedItems);
    }
}

public readonly struct OrchestrationExecutor
{
    private readonly IReader _reader;
    private readonly IWriter _writer;
    private readonly IItemTransformationPipeline _transformation;

    public OrchestrationExecutor(
        IReader reader,
        IWriter writer,
        IItemTransformationPipeline transformation)
    {
        _reader = reader;
        _writer = writer;
        _transformation = transformation;
    }

    public Task Execute()
    {
        return Orchestration.WholeProcess(_reader, _writer, _transformation);
    }
}

