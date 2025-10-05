using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Tests;

public class TestPasswordSymbolic
{
    [Fact]
    public void Should_Test_For_Generator_For_Arguments_Symbols_Data()
    {
        // Arrange
        string[] args = ["--symbolic"];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.All(password.Data, item => char.IsSymbol(item));
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }

    [Fact]
    public void Should_Test_For_Generator_For_Arguments_Symbols_Data_Prefix()
    {
        // Arrange
        string[] args = ["-s"];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.All(password.Data, item => char.IsSymbol(item));
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }
}
