#pragma warning disable CS0414 // Field is assigned but its value is never used

static class FatStructExample
{
    public static void Demo()
    {
        var mage = CreateDefaultMage();
        mage.Experience = 0;
        mage.Damage = 10;

        List<FatStructEntity> characters = new();
        characters.Add(mage);

        DamageAll(characters);
    }

    static void DamageAll(List<FatStructEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.Health != FatStructEntity.EmptyInt)
            {
                e.Health -= 1;
            }
        }
    }

    static FatStructEntity CreateDefaultMage()
    {
        return new()
        {
            Health = 100,
            Position = new(0, 0),
        };
    }
}

sealed class FatStructEntity
{
    public const int EmptyInt = int.MinValue;
    public const float EmptyFloat = float.MinValue;

    public int Health = EmptyInt;
    public int Experience = EmptyInt;
    public int Damage = EmptyInt;
    public float AggroRadius = EmptyFloat;
    public Position Position = new(EmptyFloat, EmptyFloat);
    // ...
}