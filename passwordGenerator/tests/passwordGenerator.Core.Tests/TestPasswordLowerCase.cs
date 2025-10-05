using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Tests;

public class TestPasswordLowerCase
{
    [Fact]
    public void Should_Test_For_Generator_For_Arguments_LowerCase_Data()
    {
        // Arrange
        string[] args = ["--lowercase"];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.All(password.Data, item => char.IsLower(item));
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }

    [Fact]
    public void Should_Test_For_Generator_For_Arguments_LowerCase_Data_Prefix()
    {
        // Arrange
        string[] args = ["-l"];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.All(password.Data, item => char.IsLower(item));
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }
}
