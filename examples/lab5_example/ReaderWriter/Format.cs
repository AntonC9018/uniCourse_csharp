using System.Diagnostics;
using System.Text.Json;
using CsvHelper.Configuration;

public enum FormatType
{
    Json,
    Csv,
    Text,
}

public sealed class Format
{
    private Format(FormatType type)
    {
        Type = type;
    }
    public FormatType Type { get; }
    public JsonSerializerOptions? JsonOptions { get; private init; }
    public CsvConfiguration? CsvConfig { get; private init; }

    public static Format CreateJson(JsonSerializerOptions options)
    {
        return new Format(FormatType.Json)
        {
            JsonOptions = options,
        };
    }
    public static Format CreateCsv(CsvConfiguration config)
    {
        return new Format(FormatType.Csv)
        {
            CsvConfig = config,
        };
    }
    public static Format CreateText()
    {
        return new Format(FormatType.Text);
    }

    public async Task Write<T>(Stream stream, IEnumerable<T> items)
    {
        switch (Type)
        {
            case FormatType.Json:
            {
                Debug.Assert(JsonOptions is not null);
                await JsonSerializer.SerializeAsync(stream, items, JsonOptions);
                break;
            }
            case FormatType.Csv:
            {
                Debug.Assert(CsvConfig is not null);
                await using var writer = new StreamWriter(stream);
                await using var csv = new CsvHelper.CsvWriter(writer, CsvConfig);
                await csv.WriteRecordsAsync(items);
                break;
            }
            case FormatType.Text:
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item?.ToString());
                }
                break;
            }
            default:
            {
                throw new NotSupportedException($"Format type {Type} is not supported.");
            }
        }
    }

    public async Task<IEnumerable<T>> Read<T>(Stream stream)
    {
        switch (Type)
        {
            case FormatType.Json:
            {
                Debug.Assert(JsonOptions is not null);
                return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(stream, JsonOptions)
                       ?? Array.Empty<T>();
            }
            case FormatType.Csv:
            {
                Debug.Assert(CsvConfig is not null);
                using var reader = new StreamReader(stream);
                using var csv = new CsvHelper.CsvReader(reader, CsvConfig);
                return csv.GetRecords<T>().ToList();
            }
            case FormatType.Text:
            {
                throw new NotSupportedException("Reading from Text format is not supported.");
            }
            default:
            {
                throw new NotSupportedException($"Format type {Type} is not supported.");
            }
        }
    }
}