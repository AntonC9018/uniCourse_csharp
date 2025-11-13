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
        List<EventId> eventIds,
        DateOnly startDate,
        DateOnly endDateExclusive)
    {
        var ret = new List<PlannedEvent>();
        foreach (var eventId in eventIds)
        {
            var ev = database.Get(eventId);
            Debug.Assert((ev.FixedDate == null) != (ev.RegularDate == null));

            if (ev.FixedDate is { } fixedDate)
            {
                var dateOnlyFixed = new DateOnly(
                    year: fixedDate.Year,
                    month: fixedDate.Month,
                    day: fixedDate.Day);
                if (dateOnlyFixed >= startDate && dateOnlyFixed < endDateExclusive)
                {
                    ret.Add(new(eventId, fixedDate));
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
                    var plannedEvent = new PlannedEvent(eventId, dateTime);
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

    public static PlannedEventsAtLocation GetPlannedEvents(
        Database database,
        EventIdsByLocation eventsByLocation,
        string locationName,
        DateOnly startDate,
        DateOnly endDateExclusive)
    {
        var actualEvents = GetPlannedEvents(
            database,
            eventsByLocation.Dict[locationName],
            startDate,
            endDateExclusive);
        LocalHelper.AssertOrdered(actualEvents, x => x.DateTime);
        return new(actualEvents);
    }

    public static void PrintMissingEvents(
        Database database,
        PlannedEventsAtLocation plannedEvents,
        ScheduleAtLocation schedule,
        List<string> difference,
        DifferenceToStringConverter converter)
    {
        var knownEvents = schedule.Events;
        LocalHelper.AssertOrdered(knownEvents, x => x.DateTime);
        var actualEvents = plannedEvents.List;
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
            Debug.Assert(databaseLocation.PrimaryName == schedule.LocationName);

            void AddDiff(IDifference diff)
            {
                var s = converter.DifferenceToString(diff);
                difference.Add(s);
            }

            if (knownEvent.DateTime == actualEvent.DateTime
                && knownEvent.EventName == databaseEvent.Name)
            {
                var match = new ExactMatch(actualEvent, knownEvent);
                AddDiff(match);

                indexKnown++;
                indexActual++;
                continue;
            }

            if (knownEvent.DateTime > actualEvent.DateTime)
            {
                var diff = new MissingDifference(actualEvent);
                AddDiff(diff);

                indexActual++;
                continue;
            }
            if (actualEvent.DateTime > knownEvent.DateTime)
            {
                var diff = new SuperfluousDifference(knownEvent);
                AddDiff(diff);

                indexKnown++;
                continue;
            }
            if (actualEvent.DateTime == knownEvent.DateTime)
            {
                var diff = new UpdateDifference(actualEvent, knownEvent);
                AddDiff(diff);

                indexActual++;
                indexKnown++;
                continue;
            }

            Debug.Fail("Something is wrong!!!");
        }
    }
}

public interface IDifference
{
}

public sealed record class ExactMatch(PlannedEvent Actual, KnownEventAtLocation Known) : IDifference;
public sealed record class MissingDifference(PlannedEvent Actual) : IDifference;
public sealed record class SuperfluousDifference(KnownEventAtLocation Known) : IDifference;
public sealed record class UpdateDifference(PlannedEvent Actual, KnownEventAtLocation Known) : IDifference;

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

public readonly record struct EventIdsByLocation(Dictionary<string, List<EventId>> Dict);

public readonly record struct PlannedEventsAtLocation(List<PlannedEvent> List);

public readonly struct EventNameMap
{
    private readonly Dictionary<string, string> _map;

    public EventNameMap(Dictionary<string, string> map)
    {
        _map = map;
    }

    public string Remap(string eventName)
    {
        return _map.GetValueOrDefault(eventName, eventName);
    }
}

public sealed record class DifferenceToStringConverter(
    Database Database,
    string Before,
    string After,
    EventNameMap EventNameMappings)
{
    public string DifferenceToString(
        IDifference diff)
    {
        var ret = GetMainString();
        return $"{Before}{ret}{After}";

        string GetMappedEventName(string name)
        {
            var ret1 = EventNameMappings.Remap(name);
            return ret1;
        }

        string GetMainString()
        {
            switch (diff)
            {
                case ExactMatch _:
                {
                    return "Exact match";
                }
                case MissingDifference missing:
                {
                    var databaseEvent = Database.Get(missing.Actual.Event);
                    return $"Missing event: {GetMappedEventName(databaseEvent.Name)}";
                }
                case UpdateDifference update:
                {
                    return $"Update event: {GetMappedEventName(update.Known.EventName)}";
                }
                case SuperfluousDifference super:
                {
                    return $"Superfluous event: {GetMappedEventName(super.Known.EventName)}";
                }
                default:
                {
                    Debug.Fail("Not possible");
                    throw null!;
                }
            }
        }
    }

}