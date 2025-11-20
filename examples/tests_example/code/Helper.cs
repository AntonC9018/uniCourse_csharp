using System.Diagnostics;

public static class Helper
{
    // правильная функция (дает правильный ответ, но работает не так как надо)
    // функция (???, работает так как надо)
    // тест (проверить если функция дает правильный ответ)
    // правильную функцию (дает правильный ответ, внутри работает так как надо)
    public static int GetHumanAge(int catAge)
    {
        // Debug.Assert(catAge
        const int firstAgeCoef = 15;
        const int secondAgeCoef = 15;
        const int _3to7intervalCoef = 3;
        const int thirdIntervalStart = 3;
        const int thirdIntervalEnd = 7;
        if (catAge == 0)
        {
            return 0;
        }
        if (catAge == 1)
        {
            return firstAgeCoef;
        }
        if (catAge == 2)
        {
            return firstAgeCoef + secondAgeCoef;
        }
        if (catAge >= thirdIntervalStart && catAge <= thirdIntervalEnd)
        {
            return firstAgeCoef + secondAgeCoef + (catAge - thirdIntervalStart + 1) * _3to7intervalCoef;
        }
        const int intLen = thirdIntervalEnd - thirdIntervalStart + 1;
        if (catAge >= 8 && catAge <= 10)
        {
            return firstAgeCoef + secondAgeCoef + intLen * _3to7intervalCoef + (catAge - 8 + 1) * 4;
        }

        return firstAgeCoef + secondAgeCoef + intLen * _3to7intervalCoef + (catAge - 8 + 1) * 4;
    }

    public readonly record struct Interval(int Length, int Coefficient);
    public readonly record struct Config(Interval[] Intervals);

    public static int GetHumanAge_New(
        Config config,
        int catAge)
    {
        int ret = 0;
        int start = 0;
        foreach (var interval in config.Intervals)
        {
            int end = start + interval.Length;
            if (catAge > start)
            {
                int depth = catAge - start;
                ret += Math.Min(interval.Length, depth) * interval.Coefficient;
            }
            start = end;
        }
        return ret;
    }

}