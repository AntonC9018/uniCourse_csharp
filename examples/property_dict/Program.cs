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

    public static void Demo()
    {
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


        // Array of properties (component, tag)
        // Fat struct

        var a = new object[]
        {
            new IntStat
            {
                Type = IntStatType.Health,
                Value = 10,
            },
            new Position(1, 2),
            new ExperienceComponent
            {
                Experience = 18,
            },
        };
        var b = new object[]
        {
            new ExperienceComponent
            {
                Experience = 18,
            },
        };
        List<object[]> entities = [a, b];
        Hurricane(entities);
    }


    static void Hurricane(List<object[]> entities)
    {
        foreach (var e in entities)
        {
            for (int i = 0; i < e.Length; i++)
            {
                if (e[i] is Position p)
                {
                    e[i] = new Position(p.X + 1, p.Y);
                }
            }
        }
    }



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


record struct Position(float X, float Y);


sealed class HealthComponent
{
    public int Health;
}

sealed class ExperienceComponent
{
    public int Experience;
}

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
