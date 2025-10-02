var characters = new List<Character>();
characters.Add(new Hero
{
    Experience = 0,
    Health = 100,
    Level = 1,
    Position = new(0, 0),
});
characters.Add(new Mage
{
    Experience = 0,
    Health = 100,
    Intelligence = 8,
    Position = new(1, 0),
});
characters.Add(new Zombie
{
    Damage = 50,
    Health = 100,
    AggroRadius = 10,
    Position = new(1, 1),
});
characters.Add(new Warrior
{
    Experience = 10,
    Health = 150,
    Weapon = WeaponType.Axe,
    Position = new(1, 2),
});

SimulateEnemyAttack(characters);
Stuff(characters[0]);


static void Stuff(Character character)
{
    if (character is not IMage mage)
    {
        return;
    }

    var mageTrait = mage.Mage;
    mageTrait.Intelligence = 10;
}


static void SimulateEnemyAttack(List<Character> characters)
{
    foreach (var c in characters)
    {
        if (c is not Enemy enemy)
        {
            continue;
        }

        var targets = GetTargetsFor(characters, enemy);

        Ally? minDistanceTarget = null;
        float minDistance = float.PositiveInfinity;
        foreach (var (target, dist) in targets)
        {
            if (minDistanceTarget is null
                || minDistance < dist)
            {
                minDistanceTarget = target;
                minDistance = dist;
            }
        }

        if (minDistanceTarget == null)
        {
            continue;
        }

        enemy.Attack(minDistanceTarget);
    }
}

static IEnumerable<(Ally Target, float Dist)> GetTargetsFor(
    List<Character> characters,
    Enemy enemy)
{
    foreach (var c in characters)
    {
        if (c is not Ally ally)
        {
            continue;
        }
        var dist = Helper.Distance(enemy.Position, ally.Position);
        if (dist <= enemy.AggroRadius)
        {
            yield return (ally, dist);
        }
    }
}


static class Helper
{
    public static float Distance(Position a, Position b)
    {
        var diff = new Position(
            a.X - b.X,
            a.Y - b.Y);
        var dist = (float) Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y);
        return dist;
    }
}

record struct Position(float X, float Y);

abstract class Character
{
    public int Health;
    public Position Position;
}

sealed class Shrub : Character
{
}

abstract class Enemy : Character
{
    public float AggroRadius;

    public abstract void Attack(Ally ally);
}

abstract class Ally : Character
{
    public int Experience;
}

class Zombie : Enemy
{
    public int Damage;

    public override void Attack(Ally ally)
    {
        ally.Health -= Damage;
    }
}

sealed class Lich : Zombie, IMage
{
    public MageTrait Mage { get; set; }
}

sealed class Skeleton : Enemy
{
    public int Dexterity;

    public override void Attack(Ally ally)
    {
        ally.Health -= Dexterity / 2;
    }
}

sealed class Hero : Ally
{
    public int Level;
}

sealed class Mage : Ally, IMage
{
    public MageTrait Mage { get; set; }
}

sealed class Warrior : Ally
{
    public WeaponType Weapon;
}

public interface IMage
{
    public MageTrait Mage { get; }
}

public sealed class MageTrait
{
    public int Intelligence;
    public int WandPower;
    public int Spirit;
}

public enum WeaponType
{
    Axe,
    Sword,
}
