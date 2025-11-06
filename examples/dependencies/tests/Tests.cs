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
}
