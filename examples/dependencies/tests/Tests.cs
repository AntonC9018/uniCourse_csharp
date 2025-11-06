public sealed class Tests
{
    [Fact]
    public async Task SnapshotTest()
    {
        var database = Helper.CreateDefaultEventDb();
        var plannedEvents = Helper.GetPlannedEvents(
            database,
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
        var plannedEvents = Helper.GetPlannedEvents(
            database,
            startDate: new DateOnly(year: 2025, month: 9, day: 1),
            endDateExclusive: new DateOnly(year: 2025, month: 11, day: 1));

        ScheduleAtLocation locationSchedule;
        {
            var firstEvent = database.Get(plannedEvents[0].Event);
            var firstLocation = database.Get(firstEvent.Location);
            locationSchedule = new ScheduleAtLocation
            {
                LocationName = firstLocation.PrimaryName,
            };
            foreach (var x in plannedEvents)
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
        Helper.PrintMissingEvents(
            database,
            locationSchedule,
            plannedEvents,
            s => differences.Add(s));
        await Verify(differences);
    }
}
