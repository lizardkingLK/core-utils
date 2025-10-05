using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Tests;

public class TestPasswordHelp
{
    [Fact]
    public void Should_Test_For_Generator_For_Arguments_Help_Information()
    {
        // Arrange
        string[] args = ["--help"];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.Null(password.Data);
        Assert.NotNull(password.Information);
        Assert.Null(password.Errors);
    }

    [Fact]
    public void Should_Test_For_Generator_For_Arguments_Help_Information_Prefix()
    {
        // Arrange
        string[] args = ["-h"];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.Null(password.Data);
        Assert.NotNull(password.Information);
        Assert.Null(password.Errors);
    }
}
