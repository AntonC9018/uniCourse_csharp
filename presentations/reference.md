---
theme: default
title: 'Reference implementations'
info: 'Software design lessons with C# examples'
layout: cover
class: cover
colorSchema: light
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

# Reference implementations

Software design · C#

<!--
Basic C# is assumed. Read the concept, then use the examples to explain it. Sections spanning several slides share their original teaching-time allocation.
-->

---

# Complete interval implementation · 1/5

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

# Complete interval implementation · 2/5

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

# Complete interval implementation · 3/5

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

# Complete interval implementation · 4/5

Boundary enums are construction inputs only. All empty intervals normalize to [0,0), including `default(Interval)`. Internal `long` bounds support every `int` endpoint without overflow.

---

# Complete interval implementation · 5/5

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

# Parser used in the call-order example

```csharp
sealed class TextParser
{
    public char Separator { get; set; } = ',';
    public string[] Parse(string text) => text.Split(Separator);
}

static string[] Parse(string text, char separator) => text.Split(separator);
```

This splits a single string by one separator; it is not a CSV implementation.

---

# Statistics and the restricted writing view · 1/4

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

# Statistics and the restricted writing view · 2/4

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

# Statistics and the restricted writing view · 3/4

```csharp
    public void AddWritten(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        Written = checked(Written + count);
    }

    // Only the orchestrator should reset between runs.
    // Pass IRemovalCounter to code that should only report removals.
    public void Reset()
    {
        Read = 0;
        Removed = 0;
        Written = 0;
    }
}
```

---

# Statistics and the restricted writing view · 4/4

The operations reject negative increments. Checked addition prevents integer overflow from silently turning an increment into a negative count. Through IRemovalCounter, a step is not offered Reset, AddRead, or AddWritten. The orchestrator may retain the concrete instance to manage its lifetime. This example is single-threaded; it makes no concurrency guarantee.
