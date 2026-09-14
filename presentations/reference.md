---
theme: default
title: 'Эталонные реализации'
info: 'Уроки проектирования ПО с примерами на C#'
layout: cover
class: cover
colorSchema: dark
aspectRatio: 16/9
canvasWidth: 1280
fonts:
  sans: Arial
  mono: Consolas
  provider: none
drawings:
  persist: false
transition: none
mdc: false
---

# Эталонные реализации

Проектирование ПО · C#

<!--
Базовое знание C# предполагается. Сначала изложите понятие, затем объясните его на примерах. Если раздел занимает несколько слайдов, отведённое на него учебное время распределяется между ними.
-->

---

# Полная реализация интервала · 1/5

```csharp
public enum Boundary { Inclusive, Exclusive }

public readonly struct Interval
{
    public long StartInclusive { get; }
    public long EndExclusive { get; }

    private Interval(long startInclusive, long endExclusive)
    {
        StartInclusive = startInclusive;
        EndExclusive = endExclusive;
    }

```

---

# Полная реализация интервала · 2/5

```csharp
    public static Interval Create(
        int min, int max, Boundary minBoundary, Boundary maxBoundary)
    {
        if (min > max)
            throw new ArgumentException("Bounds are reversed.");
        if (minBoundary is not (Boundary.Inclusive or Boundary.Exclusive))
            throw new ArgumentOutOfRangeException(nameof(minBoundary));
        if (maxBoundary is not (Boundary.Inclusive or Boundary.Exclusive))
            throw new ArgumentOutOfRangeException(nameof(maxBoundary));

        long startInclusive = (long)min
            + (minBoundary == Boundary.Exclusive ? 1 : 0);
        long endExclusive = (long)max
            + (maxBoundary == Boundary.Inclusive ? 1 : 0);

```

---

# Полная реализация интервала · 3/5

```csharp
        if (startInclusive >= endExclusive)
            return default;

        var result = new Interval(startInclusive, endExclusive);
        return result;
    }

    public bool Contains(int value)
    {
        bool result = value >= StartInclusive && value < EndExclusive;
        return result;
    }
}
```

---

# Полная реализация интервала · 4/5

Значения перечисления Boundary — только входные данные для создания интервала. Все пустые интервалы приводятся к виду [0,0), включая `default(Interval)`. Внутренние границы типа `long` позволяют хранить любые границы типа `int` без переполнения.

---

# Полная реализация интервала · 5/5

```csharp
var interval = Interval.Create(
    min: 10, max: 20,
    minBoundary: Boundary.Inclusive, maxBoundary: Boundary.Exclusive);
var equivalent = Interval.Create(
    min: 9, max: 19,
    minBoundary: Boundary.Exclusive, maxBoundary: Boundary.Inclusive);

Debug.Assert(interval.StartInclusive == equivalent.StartInclusive);
Debug.Assert(interval.EndExclusive == equivalent.EndExclusive);
Debug.Assert(interval.Contains(15));
Debug.Assert(!interval.Contains(25));
Debug.Assert(interval.Contains(10));
Debug.Assert(!interval.Contains(20));
```

---

# Парсер из примера про порядок вызовов

```csharp
sealed class TextParser
{
    public char Separator { get; set; } = ',';
    public string[] Parse(string text) => text.Split(Separator);
}

static string[] Parse(string text, char separator) => text.Split(separator);
```

Метод разбивает одну строку по заданному разделителю; это не реализация CSV.

---

# Статистика и ограниченный вид для записи · 1/4

```csharp
interface IRemovalCounter
{
    void AddRemoved(int count);
}

sealed class ImportStatistics : IRemovalCounter
{
    public int Read { get; private set; }
    public int Removed { get; private set; }
    public int Written { get; private set; }

```

---

# Статистика и ограниченный вид для записи · 2/4

```csharp
    public void AddRead(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        Read = checked(Read + count);
    }

    public void AddRemoved(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        Removed = checked(Removed + count);
    }

```

---

# Статистика и ограниченный вид для записи · 3/4

```csharp
    public void AddWritten(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        Written = checked(Written + count);
    }

    // Сбрасывать между запусками должен только оркестратор.
    // Коду, который лишь сообщает об удалениях,
    // передавайте IRemovalCounter.
    public void Reset()
    {
        Read = 0;
        Removed = 0;
        Written = 0;
    }
}
```

---

# Статистика и ограниченный вид для записи · 4/4

Операции отклоняют отрицательные приращения. Контролируемое сложение не даёт переполнению незаметно превратить приращение в отрицательный счётчик. При доступе через IRemovalCounter шагу недоступны методы Reset, AddRead и AddWritten. Оркестратор может хранить конкретный экземпляр, чтобы управлять его жизненным циклом. Пример — однопоточный; потокобезопасность не гарантируется.
