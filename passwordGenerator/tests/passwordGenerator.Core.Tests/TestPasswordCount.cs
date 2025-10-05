using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Tests;

public class TestPasswordCount
{
    [Theory]
    [InlineData(16)]
    [InlineData(20)]
    [InlineData(25)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(128)]
    public void Should_Test_For_Generator_For_Arguments_Counts_Data(int count)
    {
        // Arrange
        string[] args = ["--count", count.ToString()];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.Equal(password.Data.Length, count);
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }

    [Theory]
    [InlineData(16)]
    [InlineData(20)]
    [InlineData(25)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(128)]
    public void Should_Test_For_Generator_For_Arguments_Counts_Data_Prefix(int count)
    {
        // Arrange
        string[] args = ["-c", count.ToString()];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.NotNull(password.Data);
        Assert.Equal(password.Data.Length, count);
        Assert.Null(password.Information);
        Assert.Null(password.Errors);
    }

    [Theory]
    [InlineData(-101)]
    [InlineData(-20)]
    [InlineData(0)]
    [InlineData(15)]
    [InlineData(129)]
    [InlineData(1000)]
    public void Should_Test_For_Generator_For_Arguments_Counts_Errors(int count)
    {
        // Arrange
        string[] args = ["--count", count.ToString()];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.Null(password.Data);
        Assert.Null(password.Information);
        Assert.NotNull(password.Errors);
    }

    [Theory]
    [InlineData(-101)]
    [InlineData(-20)]
    [InlineData(0)]
    [InlineData(15)]
    [InlineData(129)]
    [InlineData(1000)]
    public void Should_Test_For_Generator_For_Arguments_Counts_Errors_Prefix(int count)
    {
        // Arrange
        string[] args = ["-c", count.ToString()];

        // Act
        Password password = Generator.Generate(args);

        // Assert
        Assert.Null(password.Data);
        Assert.Null(password.Information);
        Assert.NotNull(password.Errors);
    }
}
