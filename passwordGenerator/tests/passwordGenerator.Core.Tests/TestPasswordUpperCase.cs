using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Tests;

public class TestPasswordUpperCase
{
    [Fact]
    public void Should_Test_For_Generator_For_Arguments_UpperCase_Data()
    {
        // Arrange
        string[] args = ["--uppercase"];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.All(password.Data, item => char.IsUpper(item));
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }

    [Fact]
    public void Should_Test_For_Generator_For_Arguments_UpperCase_Data_Prefix()
    {
        // Arrange
        string[] args = ["-u"];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.All(password.Data, item => char.IsUpper(item));
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }
}
