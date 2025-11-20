using System.Diagnostics;
using Xunit;

public sealed class Tests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 15)]
    [InlineData(2, 23)]
    [InlineData(3, 26)]
    [InlineData(4, 29)]
    [InlineData(6, 35)]
    [InlineData(7, 38)]
    [InlineData(8, 40)]
    [InlineData(15, 54)]
    public void Test(int input, int expected)
    {
        var config = CreateConfig();
        var actual = Helper.GetHumanAge_New(config, input);
        Assert.Equal(expected, actual);
    }

    private static Helper.Config CreateConfig()
    {
        return new([
            new(Length: 1, Coefficient: 15),
            new(Length: 1, Coefficient: 8),
            new(Length: 5, Coefficient: 3),
            new(Length: 99999, Coefficient: 2),
        ]);
    }
}
