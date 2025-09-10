using System.Diagnostics;

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
diffCheckMask.FirstName = true;
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
    internal int _value;

    public bool Get(ClientField field)
    {
        int firstNameIndex = (int) field;
        int v = 1 << firstNameIndex;
        int valueWithOnlyRequiredBit = _value & v;
        // _value   = 1 1 0 1
        // v        = 1 0 0 0
        //          = 1 0 0 0

        // _value   = 0 1 0 1
        // v        = 1 0 0 0
        //          = 0 0 0 0
        return valueWithOnlyRequiredBit != 0;
    }

    public void Set(ClientField field, bool value)
    {
        int firstNameIndex = (int) field;
        int v = 1 << firstNameIndex;
        if (value == true)
        {
            _value |= v;
        }
        else
        {
            _value = _value & (~v);
        }
    }

    public bool FirstName
    {
        get => Get(ClientField.FirstName);
        set => Set(ClientField.FirstName, value);
    }
    public bool LastName
    {
        get => Get(ClientField.LastName);
        set => Set(ClientField.LastName, value);
    }
    public bool BelongsToGroups
    {
        get => Get(ClientField.BelongToGroups);
        set => Set(ClientField.BelongToGroups, value);
    }
}

enum ClientField
{
    FirstName = 0,
    LastName = 1,
    BelongToGroups = 2,
    Count,

    FirstField = FirstName,
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
        // mask:     1 1 0 1
        // toCheck:  1 1 0 0
        // &:        1 1 0 0

        // mask:     1 0 0 1
        // toCheck:  1 1 0 0
        // &:        1 0 0 0
        return (mask._value & toCheck._value) == toCheck._value;
    }

    // HashSet Except
    // побитовый & ~x
    public static ClientFieldMask Without(
        ClientFieldMask mask,
        ClientFieldMask toDelete)
    {
        // mask:      1 1 0
        // delete:    1 0 1
        // ~delete:   0 1 0
        // result:    0 1 0

        // mask:      1 1 1
        // delete:    1 0 0
        // ~delete:   0 1 1
        // result:    0 1 1
        mask._value = mask._value & ~toDelete._value;
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