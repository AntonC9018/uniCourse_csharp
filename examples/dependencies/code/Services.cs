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

    public static void PrintMissingEvents(
        Database database,
        ScheduleAtLocation schedule,
        List<PlannedEvent> actualEvents,
        Action<string>? print = null)
    {
        print ??= Console.WriteLine;

        var knownEvents = schedule.Events;
        LocalHelper.AssertOrdered(knownEvents, x => x.DateTime);
        LocalHelper.AssertOrdered(actualEvents, x => x.DateTime);

        int indexKnown = 0;
        int indexActual = 0;

        while (true)
        {
            if (indexKnown >= knownEvents.Count)
            {
                break;
            }
            if (indexActual >= actualEvents.Count)
            {
                break;
            }

            var knownEvent = knownEvents[indexKnown];
            var actualEvent = actualEvents[indexActual];

            var databaseEvent = database.Get(actualEvent.Event);
            var databaseLocation = database.Get(databaseEvent.Location);
            if (databaseLocation.PrimaryName != schedule.LocationName)
            {
                indexActual++;
                continue;
            }
            if (knownEvent.DateTime == actualEvent.DateTime
                && knownEvent.EventName == databaseEvent.Name)
            {
                indexKnown++;
                indexActual++;
                print("Exact match");
                continue;
            }

            if (knownEvent.DateTime > actualEvent.DateTime)
            {
                print($"Missing event: {databaseEvent.Name}");
                indexActual++;
                continue;
            }
            if (actualEvent.DateTime > knownEvent.DateTime)
            {
                print($"Superfluous event: {knownEvent.EventName}");
                indexKnown++;
                continue;
            }
            if (actualEvent.DateTime == knownEvent.DateTime)
            {
                print($"Updated event: {knownEvent.EventName}");
                indexActual++;
                indexKnown++;
                continue;
            }

            Debug.Fail("Something is wrong!!!");
        }
    }
}

public sealed class KnownEventAtLocation
{
    public string EventName;
    public DateTimeOffset DateTime;
}

public sealed class ScheduleAtLocation
{
    public List<KnownEventAtLocation> Events = new();
    public required string LocationName;
}


file static class LocalHelper
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

    [Conditional("DEBUG")]
    internal static void AssertOrdered<T, U>(IEnumerable<T> items, Func<T, U> selector)
    {
        items = items.ToArray();
        var sorted = items.OrderBy(selector);
        Debug.Assert(items.SequenceEqual(sorted));
    }
}