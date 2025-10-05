using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Tests;

public class TestPasswordNumeric
{
    [Fact]
    public void Should_Test_For_Generator_For_Arguments_Numericals_Data()
    {
        // Arrange
        string[] args = ["--numeric"];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.All(password.Data, item => char.IsNumber(item));
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }

    [Fact]
    public void Should_Test_For_Generator_For_Arguments_Numericals_Data_Prefix()
    {
        // Arrange
        string[] args = ["-n"];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.All(password.Data, item => char.IsNumber(item));
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }
}
