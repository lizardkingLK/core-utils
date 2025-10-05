namespace passwordGenerator.Core.Abstractions;

public abstract record Interaction
{
    public abstract void Display();
    public abstract void Prompt();
    public abstract void Process();
}