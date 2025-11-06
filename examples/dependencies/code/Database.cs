public readonly record struct PlannedEvent(
    EventId Event,
    DateTimeOffset DateTime);

public sealed class Database
{
    public List<Event> Events = new();
    public List<Location> Locations = new();

    public Location Get(LocationId id) => Locations[id.Value];
    public Event Get(EventId id) => Events[id.Value];

    public LocationId Add(Location location)
    {
        Locations.Add(location);
        return new LocationId(Locations.Count - 1);
    }

    public EventId Add(Event @event)
    {
        Events.Add(@event);
        return new EventId(Events.Count - 1);
    }
}

public sealed class Event
{
    public required string Name;
    public required LocationId Location;
    public required DateTimeOffset? FixedDate;
    public required WeeklyRegularDate? RegularDate;
}

public struct WeeklyRegularDate
{
    public required DayOfWeek DayOfWeek;
    public required TimeOnly Time;
}

public readonly record struct EventId(int Value);
public readonly record struct LocationId(int Value);

public sealed class Location
{
    public required string[] Names;
    public string PrimaryName => Names[0];
}

