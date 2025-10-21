using passwordGenerator.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using passwordGenerator.Core.Library.DataStructures.Linear.Lists.LinkedList;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap.State;
using static passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap.Shared.Constants;
using static passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap.Shared.Exceptions;

namespace passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;

public class HashMap<K, V>(float loadFactor = LOAD_FACTOR) where K : notnull
{
    private readonly float _loadFactor = loadFactor;

    private DynamicallyAllocatedArray<DoublyLinkedList<HashNode<K, V>>> _buckets = new();

    private int _capacity = INITIAL_CAPACITY;

    public int Size = 0;

    public IEnumerable<KeyValuePair<K, V?>> KeyValues => GetKeyValues();

    public V? this[K key]
    {
        get => GetValue(key);
        set => Update(key, value);
    }

    public HashMap(params HashNode<K, V>[] keyValues) : this()
    {
        foreach ((K key, V value) in keyValues)
        {
            Add(key, value);
        }
    }

    public V? GetValue(K key)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            throw KeyDoesNotContainException;
        }

        return hashNode!.Value;
    }

    public bool TryGetValue(K key, out V? value)
    {
        value = default;

        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            return false;
        }

        value = hashNode!.Value;

        return true;
    }

    public void Add(K key, V value)
    {
        if (ContainsKey(key, out DoublyLinkedList<HashNode<K, V>>? bucket, out _))
        {
            throw KeyContainsException;
        }

        bucket!.AddToTail(new HashNode<K, V>(key, value));
        Size++;

        if (Size / _capacity >= _loadFactor)
        {
            ReHash();
        }
    }

    public bool TryAdd(K key, V value)
    {
        if (ContainsKey(key, out DoublyLinkedList<HashNode<K, V>>? bucket, out _))
        {
            return false;
        }

        bucket!.AddToTail(new HashNode<K, V>(key, value));
        Size++;

        if (Size / _capacity >= _loadFactor)
        {
            ReHash();
        }

        return true;
    }

    public void Update(K key, V? value)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            throw KeyDoesNotContainException;
        }

        hashNode!.Value = value;
    }

    public bool TryUpdate(K key, V value)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            return false;
        }

        hashNode!.Value = value;

        return true;
    }

    private IEnumerable<KeyValuePair<K, V?>> GetKeyValues()
    {
        foreach (HashNode<K, V> hashNode in GetHashNodes())
        {
            yield return hashNode.KeyValue;
        }
    }

    private IEnumerable<HashNode<K, V>> GetHashNodes()
    {
        foreach (DoublyLinkedList<HashNode<K, V>>? bucket in _buckets.Values)
        {
            foreach (HashNode<K, V> item in bucket?.ValuesHeadToTail ?? [])
            {
                yield return item;
            }
        }
    }

    private void ReHash()
    {
        _capacity *= 2;
        DynamicallyAllocatedArray<DoublyLinkedList<HashNode<K, V>>> newBuckets = new(_capacity);
        int newIndex;
        foreach ((K key, V value) in GetHashNodes())
        {
            newIndex = GetHashCode(key, _capacity);
            if (!ContainsBucket(newIndex, out DoublyLinkedList<HashNode<K, V>>? bucket, newBuckets))
            {
                bucket = newBuckets.Insert(newIndex, new());
            }

            bucket!.AddToTail(new(key, value));
        }

        _buckets = newBuckets;
    }

    private bool ContainsKey(K key, out DoublyLinkedList<HashNode<K, V>>? bucket, out HashNode<K, V>? value)
    {
        bucket = default;
        value = default;

        int index = GetHashCode(key, _capacity);

        bool containsBucket = ContainsBucket(index, out bucket, _buckets);
        bool containsKey = false;
        if (containsBucket)
        {
            containsKey = ContainsKey(bucket!, hashNode => hashNode.Key.Equals(key), out value);
        }
        else
        {
            bucket = _buckets.Insert(index, new());
        }

        return containsKey;
    }

    private static bool ContainsKey(
        DoublyLinkedList<HashNode<K, V>> bucket,
        Predicate<HashNode<K, V>> filterFunction,
        out HashNode<K, V>? value)
        => bucket.TryGetValue(filterFunction, out value);

    private static bool ContainsBucket(
        int index,
        out DoublyLinkedList<HashNode<K, V>>? bucket,
        DynamicallyAllocatedArray<DoublyLinkedList<HashNode<K, V>>> buckets)
        => buckets.TryGetValue(index, out bucket) && bucket is not null;

    private static int GetHashCode(K key, int capacity)
    {
        int hashCode = key.GetHashCode();
        int bitMaskForIndex = hashCode >> 31;
        hashCode = (hashCode ^ bitMaskForIndex) - bitMaskForIndex;

        return hashCode % capacity;
    }
}