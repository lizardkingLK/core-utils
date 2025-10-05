using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Tests;

public class TestPasswordMiscellaneous
{
    [Theory]
    [InlineData(["-nlus"])]
    [InlineData(["-lsun"])]
    [InlineData(["-n", "-l", "-u", "-s"])]
    [InlineData(["--numeric", "--lowercase", "--uppercase", "--symbolic"])]
    [InlineData(["-l", "-s", "-u", "-n"])]
    [InlineData(["--lowercase", "--symbolic", "--uppercase", "--numeric"])]
    [InlineData(["-c", "20"])]
    [InlineData(["-nlsuc", "20"])]
    [InlineData(["-cnlsu", "20"])]
    [InlineData(["-lsunc", "20"])]
    [InlineData(["-clsun", "20"])]
    public void Should_Test_For_Generator_For_Miscellaneous_Data(params string[] args)
    {
        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }

    [Theory]
    [InlineData(["-nlus", "20"])]
    [InlineData(["-lsun", "20"])]
    [InlineData(["-n", "-l", "-u", "-s", "20"])]
    [InlineData(["--numeric", "--lowercase", "--uppercase", "--symbolic", "20"])]
    [InlineData(["-l", "-s", "-u", "-n", "20"])]
    [InlineData(["--lowercase", "--symbolic", "--uppercase", "--numeric", "20"])]
    [InlineData(["-c"])]
    [InlineData(["-nlsuc"])]
    [InlineData(["-cnlsu"])]
    [InlineData(["-lsunc"])]
    [InlineData(["-clsun"])]
    public void Should_Test_For_Generator_For_Miscellaneous_Errors(params string[] args)
    {
        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.Null(password.Data);
        Assert.Null(password.Information);
        Assert.NotNull(password.Errors);
    }

    [Theory]
    [InlineData("--")]
    [InlineData("-")]
    [InlineData("-o")]
    [InlineData("--output")]
    [InlineData("-a")]
    [InlineData("--all")]
    [InlineData("-ao")]
    [InlineData("-- what")]
    public void Should_Test_For_Generator_For_Miscellaneous_Errors_Invalid_Arguments(params string[] args)
    {
        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.Null(password.Data);
        Assert.Null(password.Information);
        Assert.NotNull(password.Errors);
    }
}
