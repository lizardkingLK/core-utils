namespace passwordGenerator.Core.Abstractions;

public abstract record Interaction<T>()
{
    public abstract string View { get; init; }

    public abstract void Display();
    public abstract void Prompt(T builder);
}