using Microsoft.Extensions.Options;

public interface IWriterFactory
{
    public IWriter Create();
}
public interface IReaderFactory
{
    public IReader Create();
}

public sealed class FormatFileOptions
{
    public const string ReaderKey = "Reader";
    public const string WriterKey = "Writer";
    public string FileName { get; set; } = string.Empty;
    public required Format Format { get; set; }
}

public sealed class FileReaderFactory : IReaderFactory
{
    private readonly IOptionsSnapshot<FormatFileOptions> _options;

    public FileReaderFactory(IOptionsSnapshot<FormatFileOptions> options)
    {
        _options = options;
    }

    public IReader Create()
    {
        var val = _options.Get(FormatFileOptions.ReaderKey);
        var format = val.Format;
        if (format == null)
        {
            throw new ArgumentException("Format must be provided for FileReader.", nameof(format));
        }

        return new FileReader(val.FileName, val.Format);
    }
}

public sealed class FileWriterFactory : IWriterFactory
{
    private readonly IOptionsSnapshot<FormatFileOptions> _options;

    public FileWriterFactory(IOptionsSnapshot<FormatFileOptions> options)
    {
        _options = options;
    }

    public IWriter Create()
    {
        var val = _options.Get(FormatFileOptions.WriterKey);
        var format = val.Format;
        if (format == null)
        {
            throw new ArgumentException("Format must be provided for FileWriter.", nameof(format));
        }

        return new FileWriter(val.FileName, val.Format);
    }
}

public sealed class RandomReaderFactoryOptions
{
    public int GeneratedCount { get; set; }
}

public sealed class RandomReaderFactory : IReaderFactory
{
    // How to configure this??????
    private readonly IOptionsSnapshot<RandomReaderFactoryOptions> _count;
    private readonly IObjectGeneratorFactory _generators;

    public RandomReaderFactory(
        IOptionsSnapshot<RandomReaderFactoryOptions> options,
        IObjectGeneratorFactory generators)
    {
        _count = options;
        _generators = generators;
    }

    public IReader Create()
    {
        var count = _count.Value.GeneratedCount;
        return new RandomReader(count, _generators);
    }
}

public sealed class LoggingWriterFactory : IWriterFactory
{
    public IWriter Create()
    {
        return new LoggingWriter();
    }
}
