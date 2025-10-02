struct PropertyArray
{
    public readonly object[] Properties;

    public PropertyArray(object[] properties)
    {
        Properties = properties;
    }
}

static class PropertyArrayExample
{
    public static void DemoPropertyArray()
    {
        var a = new PropertyArray(
        [
            // Tag
            new IntStat
            {
                Type = IntStatType.Health,
                Value = 10,
            },
            // Or component
            new HealthComponent
            {
                Health = 10,
            },

            new Position(1, 2),
            new ExperienceComponent
            {
                Experience = 18,
            },
        ]);
        var b = new PropertyArray(
        [
            new ExperienceComponent
            {
                Experience = 18,
            },
        ]);
        List<PropertyArray> entities = [a, b];
        Hurricane(entities);
    }

    static void Hurricane(List<PropertyArray> entities)
    {
        foreach (var e in entities)
        {
            for (int i = 0; i < e.Properties.Length; i++)
            {
                if (e.Properties[i] is Position p)
                {
                    e.Properties[i] = new Position(p.X + 1, p.Y);
                }
            }
        }
    }
}

sealed class IntStat
{
    public required IntStatType Type;
    public required int Value;
}

enum IntStatType
{
    Health,
    Experience,
    Damage,
    Intelligence,
}


