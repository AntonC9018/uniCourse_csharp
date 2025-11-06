using System.Diagnostics;

public static class Helper
{
    public static Database CreateDefaultEventDb()
    {
        var database = new Database();
        var squareId = database.Add(new Location
        {
            Names =
            [
                "Center Square",
                "Square",
            ],
        });
        var arenaId = database.Add(new Location
        {
            Names =
            [
                "Arena Moldova",
                "Arena",
            ],
        });

        database.Add(new Event
        {
            Name = "Concert 1",
            Location = squareId,
            FixedDate = new DateTime(year: 2025, month: 10, 5),
            RegularDate = null,
        });

        database.Add(new Event
        {
            Name = "Concert Stuff",
            Location = arenaId,
            FixedDate = new DateTime(year: 2026, month: 10, 5),
            RegularDate = null,
        });

        database.Add(new Event
        {
            Name = "Regular Concert 1",
            Location = arenaId,
            FixedDate = null,
            RegularDate = new()
            {
                Time = new TimeOnly(hour: 22, minute: 0),
                DayOfWeek = DayOfWeek.Monday,
            },
        });

        database.Add(new Event
        {
            Name = "Regular Concert 2",
            Location = arenaId,
            FixedDate = null,
            RegularDate = new()
            {
                Time = new TimeOnly(hour: 20, minute: 0),
                DayOfWeek = DayOfWeek.Tuesday,
            },
        });
        return database;
    }

    public static List<PlannedEvent> GetPlannedEvents(
        Database database,
        DateOnly startDate,
        DateOnly endDateExclusive)
    {
        var ret = new List<PlannedEvent>();
        for (int i = 0; i < database.Events.Count; i++)
        {
            var id = new EventId(i);
            var ev = database.Get(id);
            Debug.Assert((ev.FixedDate == null) != (ev.RegularDate == null));

            if (ev.FixedDate is { } fixedDate)
            {
                var dateOnlyFixed = new DateOnly(
                    year: fixedDate.Year,
                    month: fixedDate.Month,
                    day: fixedDate.Day);
                if (dateOnlyFixed >= startDate && dateOnlyFixed < endDateExclusive)
                {
                    ret.Add(new(id, fixedDate));
                }

                continue;
            }

            if (ev.RegularDate is { } regularDate)
            {
                var startWeekCorrectDay = startDate.GetDayOfThisWeek(regularDate.DayOfWeek);
                if (startWeekCorrectDay < startDate)
                {
                    startWeekCorrectDay = startWeekCorrectDay.AddDays(7);
                }

                var currentDay = startWeekCorrectDay;
                while (currentDay < endDateExclusive)
                {
                    var dateTime = new DateTime(currentDay, regularDate.Time);
                    var plannedEvent = new PlannedEvent(id, dateTime);
                    ret.Add(plannedEvent);

                    currentDay = currentDay.AddDays(7);
                }

                continue;
            }

            Debug.Fail("No date in DB");
        }

        ret.Sort((a, b) =>
        {
            var da = a.DateTime;
            var db = b.DateTime;
            return (int) (da - db).TotalSeconds;
        });

        return ret;
    }
}

internal static class DayHelper
{
    public static DateOnly GetDayOfThisWeek(this DateOnly d, DayOfWeek day)
    {
        var monday = d.AddDays(-(int)d.DayOfWeek + (int)DayOfWeek.Monday);
        return GetDayOfWeekFromMonday(monday, day);
    }

    private const int weekdayCount = 7;

    private static DateOnly GetDayOfWeekFromMonday(DateOnly monday, DayOfWeek day)
    {
        var offset = (day - DayOfWeek.Monday + weekdayCount) % weekdayCount;
        var ret = monday.AddDays(offset);
        return ret;
    }
}