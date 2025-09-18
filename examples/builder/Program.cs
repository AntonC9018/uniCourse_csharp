#pragma warning disable CS8321 // Local function is declared but never used

var city = new CityBuilder();

var home = city.Home();
home.Address("123");

var s = city.Scope();
s.Home(home);
s.Age(32);

{
    var john = s.Citizen();
    john.Age(18);
    john.Name("John");
}
{
    var joe = s.Citizen();
    joe.Name("Joe");
}
{
    var j = s.Citizen();
    j.Name("Chell");
}

// Could program different operations for the builder vs the immutable object.
var c = city.Build();
Console.WriteLine(c.Citizens[0].Age);
Console.WriteLine(c.Citizens[1].Age);
Console.WriteLine(c.Citizens[2].Age);
_ = c;
return;

// Initialization without a builder
static void InitializationWithoutBuilder()
{
    var home = new Home
    {
        Address = "123",
    };
    var john = new Citizen
    {
        Age = 18,
        Name = "John",
        Home = home,
    };
    var city = new City
    {
        Citizens = [john],
        Homes = [home],
    };
    _ = city;
}

sealed class MutableCity
{
    public List<MutableCitizen> Citizens = new();
    public List<MutableHome> Homes = new();
}

sealed class CityBuilder
{
    public MutableCity Model = new();

    public CitizenBuilder Citizen()
    {
        var m = new MutableCitizen();
        Model.Citizens.Add(m);
        return new(m);
    }

    public HomeBuilder Home()
    {
        var m = new MutableHome();
        Model.Homes.Add(m);
        return new(m);
    }

    // Mutable -> Immutable
    public City Build()
    {
        var retHomes = new Home[Model.Homes.Count];
        for (int i = 0; i < retHomes.Length; i++)
        {
            var mut = Model.Homes[i];
            if (mut.Address is not { } address)
            {
                // Validation
                throw new InvalidOperationException("Validation error: address not specified");
            }

            // Conversion
            var immut = new Home
            {
                Address = address,
            };
            retHomes[i] = immut;
        }

        var mutToImmutHome = new Dictionary<MutableHome, Home>(retHomes.Length);
        for (int i = 0; i < retHomes.Length; i++)
        {
            var mut = Model.Homes[i];
            var immut = retHomes[i];
            mutToImmutHome.Add(mut, immut);
        }

        var retCitizens = new Citizen[Model.Citizens.Count];
        for (int i = 0; i < retCitizens.Length; i++)
        {
            var mut = Model.Citizens[i];
            var home = mut.Home != null ? mutToImmutHome[mut.Home] : null;
            if (mut.Name is not { } name)
            {
                throw new InvalidOperationException("Name not given for citizen");
            }
            if (mut.Age == MutableCitizen.InvalidAge)
            {
                throw new InvalidOperationException("Name not given");
            }
            var immut = new Citizen
            {
                Age = mut.Age,
                Name = name,
                Home = home,
            };
            retCitizens[i] = immut;
        }

        return new City
        {
            Citizens = retCitizens,
            Homes = retHomes,
        };
    }

    public CityScopeBuilder Scope()
    {
        return new(this);
    }
}

sealed class CityScopeBuilder
{
    public readonly CityBuilder CityBuilder;
    public readonly CitizenBuilder CitizenBuilder;

    public CityScopeBuilder(CityBuilder city)
    {
        CityBuilder = city;
        var citizen = new MutableCitizen();
        CitizenBuilder = new(citizen);
    }

    public CitizenBuilder Citizen()
    {
        var citizen = CityBuilder.Citizen();
        if (CitizenBuilder.Model.Age != MutableCitizen.InvalidAge)
        {
            citizen.Model.Age = CitizenBuilder.Model.Age;
        }
        if (CitizenBuilder.Model.Name is { } name)
        {
            citizen.Model.Name = name;
        }
        if (CitizenBuilder.Model.Home is { } home)
        {
            citizen.Model.Home = home;
        }
        return citizen;
    }
    public HomeBuilder Home()
    {
        return CityBuilder.Home();
    }

    public void Age(int age)
    {
        CitizenBuilder.Age(age);
    }
    public void Name(string name)
    {
        CitizenBuilder.Name(name);
    }
    public void Home(HomeBuilder home)
    {
        CitizenBuilder.Home(home);
    }
}

// The model is without encapsulation
sealed class MutableCitizen
{
    public const int InvalidAge = -1;

    public int Age = InvalidAge;
    public string? Name = null;
    public MutableHome? Home = null;
}

// Single Responsibility
sealed class CitizenBuilder
{
    public readonly MutableCitizen Model;
    public CitizenBuilder(MutableCitizen model)
    {
        Model = model;
    }

    // Encapsulation is on the builder level
    public void Age(int age)
    {
        // Contract
        if (age < 0)
        {
            throw new InvalidOperationException("Invalid age");
        }
        Model.Age = age;
    }
    public void Name(string name)
    {
        Model.Name = name;
    }
    public void Home(HomeBuilder home)
    {
        Model.Home = home.Home;
    }
}

sealed class MutableHome
{
    public string? Address;
}

sealed class HomeBuilder
{
    public readonly MutableHome Home;

    public HomeBuilder(MutableHome home)
    {
        Home = home;
    }

    public void Address(string address)
    {
        Home.Address = address;
    }
}

sealed class CityChanger
{
    private readonly City _city;

    public CityChanger(City city)
    {
        _city = city;
    }

    public void Change()
    {
        _city.Citizens[0] = new()
        {
            Age = 18,
            Name = "Joe",
        };
    }
}

sealed class CityLogic
{
    private readonly City _city;
    private readonly Dictionary<string, int> _citizenMapping = new();

    public CityLogic(City city)
    {
        _city = city;
        for (int index = 0; index < _city.Citizens.Length; index++)
        {
            var citizen = _city.Citizens[index];
            _citizenMapping[citizen.Name] = index;
        }
    }

    public Citizen? Search(string name)
    {
        if (!_citizenMapping.TryGetValue(name, out int index))
        {
            return null;
        }
        return _city.Citizens[index];
    }
}

// Immutabibilty -- нельзя будет изменить
// Mutable -- можно изменять
// This is an immutable model and must not be changed.
sealed class City
{
    public required Citizen[] Citizens { get; init; }
    public required Home[] Homes { get; init; }
}

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

sealed class Citizen
{
    public required string Name;
    public required int Age;
    public Home? Home;
    // public Citizen[] Cohabitants;
}

sealed class Home
{
    public required string Address;
}
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
