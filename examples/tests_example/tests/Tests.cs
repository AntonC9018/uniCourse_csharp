using System.Diagnostics;

Helper.Config CreateConfig()
{
    return new([
        new(Length: 1, Coefficient: 15),
        new(Length: 1, Coefficient: 8),
        new(Length: 5, Coefficient: 3),
        new(Length: 99999, Coefficient: 2),
    ]);
}

void Test(int input, int expected)
{
    var config = CreateConfig();
    var actual = Helper.GetHumanAge_New(config, input);
    Debug.Assert(actual == expected, $"Output for {input} is {actual}, expected {expected}");
}

Test(0, 0);
Test(1, 15);
Test(2, 23);
Test(3, 26);
Test(4, 29);
Test(6, 35);
Test(7, 38);
Test(8, 40);
Test(15, 54);