public interface IReader
{
    public Task<IEnumerable<T>> Read<T>();
}

public interface IWriter
{
    public Task Write<T>(IEnumerable<T> values);
}

public sealed class FileReader : IReader
{
    private readonly string _fileName;
    private readonly Format _format;

    public FileReader(string fileName, Format format)
    {
        _fileName = fileName;
        _format = format;
    }

    public async Task<IEnumerable<T>> Read<T>()
    {
        await using var stream = File.OpenRead(_fileName);
        var items = await _format.Read<T>(stream);
        return items;
    }
}

public sealed class FileWriter : IWriter
{
    private readonly string _fileName;
    private readonly Format _format;

    public FileWriter(string fileName, Format format)
    {
        _fileName = fileName;
        _format = format;
    }

    public async Task Write<T>(IEnumerable<T> values)
    {
        await using var stream = new FileStream(_fileName, FileMode.Create);
        await _format.Write(stream, values);
    }
}

public sealed class RandomReader : IReader
{
    private readonly int _count;
    private readonly IObjectGeneratorFactory _generators;

    public RandomReader(int count, IObjectGeneratorFactory generators)
    {
        _count = count;
        _generators = generators;
    }

    public Task<IEnumerable<T>> Read<T>()
    {
        var generator = _generators.Create<T>();
        var items = generator.Generate(_count);
        return Task.FromResult(items);
    }
}

public sealed class LoggingWriter : IWriter
{
    public Task Write<T>(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            Console.WriteLine(value);
        }
        return Task.CompletedTask;
    }
}
