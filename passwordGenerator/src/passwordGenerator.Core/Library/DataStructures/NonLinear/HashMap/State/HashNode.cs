namespace passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap.State;

public record HashNode<K, V>(K Key, V Value) where K : notnull
{
    public K Key { get; init; } = Key;

    public V? Value { get; set; } = Value;

    public KeyValuePair<K, V?> KeyValue => new(Key, Value);
}