var client = new Client
{
    FirstName = "Anton",
    LastName = "Curmanschii",
    BelongsToGroups = [ Group.HighlyValued ],
};
var client1 = new Client
{
    FirstName = "Anton",
    LastName = "Curmanschii",
    BelongsToGroups = [ Group.NotImportant ],
};
var whereAreDifferent = Functions.Diff(client, client1);

if (whereAreDifferent.FirstName
    && !whereAreDifferent.LastName)
{
    Console.WriteLine("Might be siblings");
}

var diffCheckMask = new ClientFieldMask
{
    FirstName = true,
    LastName = true,
};
if (Functions.AreSet(whereAreDifferent, diffCheckMask))
{
    Console.WriteLine("Either first or last name are different");
}



return;

// Field Masks

// Domain Model
sealed class Client
{
    public required string FirstName;
    public required string LastName;
    public required Group[] BelongsToGroups;
}

record struct ClientFieldMask
{
    public bool FirstName;
    public bool LastName;
    public bool BelongsToGroups;
}

enum Group
{
    HighlyValued,
    NotImportant,
}

static class Functions
{
    public static bool FirstNameEquals(Client a, Client b)
    {
        if (a.FirstName.SequenceEqual(b.FirstName))
        {
            return true;
        }
        return false;
    }

    public static ClientFieldMask Diff(
        Client a,
        Client b)
    {
        ClientFieldMask result = new();
        if (FirstNameEquals(a, b))
        {
            result.FirstName = true;
        }
        if (a.LastName != b.LastName)
        {
            result.LastName = true;
        }
        if (!a.BelongsToGroups.SequenceEqual(b.BelongsToGroups))
        {
            result.BelongsToGroups = true;
        }
        return result;
    }

    public static bool AreSet(
        ClientFieldMask mask,
        ClientFieldMask toCheck)
    {
        if (toCheck.FirstName)
        {
            if (!mask.FirstName)
            {
                return false;
            }
        }
        if (toCheck.LastName)
        {
            if (!mask.LastName)
            {
                return false;
            }
        }
        if (toCheck.BelongsToGroups)
        {
            if (!mask.BelongsToGroups)
            {
                return false;
            }
        }
        return true;
    }

    // HashSet Except
    // побитовый &
    public static ClientFieldMask Without(
        ClientFieldMask mask,
        ClientFieldMask toDelete)
    {
        if (toDelete.FirstName)
        {
            mask.FirstName = false;
        }
        if (toDelete.LastName)
        {
            mask.LastName = false;
        }
        if (toDelete.BelongsToGroups)
        {
            mask.BelongsToGroups = false;
        }
        return mask;
    }

    public static ClientFieldMask ConfigureFilter(ClientFieldMask mask)
    {
        if (mask.BelongsToGroups)
        {
            mask.FirstName = false;
        }
        return mask;
    }

    public static void PrintFirstName(Client client)
    {
        Console.WriteLine($"FirstName: {client.FirstName}");
    }
    public static void PrintLastName(Client client)
    {
        Console.WriteLine($"LastName: {client.LastName}");
    }
    public static void PrintGroups(Client client)
    {
        foreach (var group in client.BelongsToGroups)
        {
            Console.WriteLine($"BelongsToGroup: {group}");
        }
    }
    public static void Print(
        Client client,
        ClientFieldMask? mask = null)
    {
        if (mask is not { } m)
        {
            m = new()
            {
                FirstName = true,
                LastName = true,
                BelongsToGroups = true,
            };
        }

        if (m.FirstName)
        {
            PrintFirstName(client);
        }
        if (m.LastName)
        {
            PrintLastName(client);
        }
        if (m.BelongsToGroups)
        {
            PrintGroups(client);
        }
    }
}