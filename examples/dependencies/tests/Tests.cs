using System.Runtime.InteropServices;

public sealed class Tests
{
    [Fact]
    public async Task SnapshotTest()
    {
        var database = Helper.CreateDefaultEventDb();
        var allEventIds = Enumerable.Range(0, database.Events.Count).Select(x => new EventId(x)).ToList();
        var plannedEvents = Helper.GetPlannedEvents(
            database,
            allEventIds,
            startDate: new DateOnly(year: 2025, month: 9, day: 1),
            endDateExclusive: new DateOnly(year: 2025, month: 11, day: 1));
        var verifyModels = plannedEvents.Select(x =>
        {
            var ev = database.Get(x.Event);
            return new
            {
                IsFixedDate = ev.FixedDate != null,
                x.DateTime,
                ev.Name,
                LocationName = database.Get(ev.Location).PrimaryName,
            };
        });
        await Verify(verifyModels)
            .DontScrubDateTimes();
    }

    [Fact]
    public async Task Test1()
    {
        var database = Helper.CreateDefaultEventDb();

        var eventsByLocation = new EventIdsByLocation(new());
        {
            for (int eventIndex = 0; eventIndex < database.Events.Count; eventIndex++)
            {
                var eventId = new EventId(eventIndex);
                var ev = database.Get(eventId);
                var locationId = ev.Location;
                var location = database.Get(locationId);
                foreach (var name in location.Names)
                {
                    ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(eventsByLocation.Dict, name, out bool exists);
                    if (!exists)
                    {
                        list = new();
                    }
                    // if (!eventsByLocation.TryGetValue(name, out var list))
                    // {
                    //     list = new();
                    //     eventsByLocation.Add(name, list);
                    // }
                    list!.Add(eventId);
                }
            }
        }

        var plannedEvents = Helper.GetPlannedEvents(
            database,
            eventsByLocation,
            "Arena",
            startDate: new DateOnly(year: 2025, month: 9, day: 1),
            endDateExclusive: new DateOnly(year: 2025, month: 11, day: 1));

        ScheduleAtLocation locationSchedule;
        {
            var firstEvent = database.Get(plannedEvents.List[0].Event);
            var firstLocation = database.Get(firstEvent.Location);

            locationSchedule = new ScheduleAtLocation
            {
                LocationName = firstLocation.PrimaryName,
            };
            foreach (var x in plannedEvents.List)
            {
                var ev = database.Get(x.Event);
                var loc = database.Get(ev.Location);
                if (loc.PrimaryName != firstLocation.PrimaryName)
                {
                    continue;
                }
                locationSchedule.Events.Add(new()
                {
                    DateTime = x.DateTime,
                    EventName = ev.Name,
                });
            }

            locationSchedule.Events.RemoveAt(0);
            locationSchedule.Events.RemoveAt(7);
            locationSchedule.Events[1].EventName = "HELLO";
        }

        var differences = new List<string>();
        var before = "{";
        var after = "}";
        var eventNameMappings = new Dictionary<string, string>();
        var converter = new DifferenceToStringConverter(
            database,
            before,
            after,
            new(eventNameMappings));

        Helper.PrintMissingEvents(
            database,
            plannedEvents,
            locationSchedule,
            differences,
            converter);
        await Verify(differences);
    }
}
