using System.Collections;
using wordSearch.Core.Library.Linear.Arrays;
using wordSearch.Core.Library.Linear.Lists;
using static wordSearch.Core.Library.NonLinear.HashMaps.Shared.Constants;

namespace wordSearch.Core.Library.NonLinear.HashMaps;

public class HashMap<K, V> : IEnumerable<KeyValuePair<K, V?>> where K : notnull
{
    private record HashNode(K Key, V? Value)
    {
        public K Key { get; } = Key;
        public V? Value { get; set; } = Value;

        public static implicit operator KeyValuePair<K, V?>(HashNode hashNode)
        => new(hashNode.Key, hashNode.Value);
    }

    private readonly float _loadFactor;

    private int _capacity = InitialCapacity;

    public int Count { get; set; }

    private DynamicallyAllocatedArray<DoublyLinkedList<HashNode>> _buckets;

    public V? this[K key]
    {
        get => Get(key);
        set => Set(key, value);
    }

    public HashMap(float loadFactor = LoadFactor)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(loadFactor, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(loadFactor, .9f);

        _buckets = [];
        _loadFactor = loadFactor;
    }

    public HashMap(params (K, V)[] values) : this(LoadFactor)
    {
        foreach ((K key, V? value) in values)
        {
            Add(key, value);
        }
    }

    public V? Get(K key)
    {
        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            throw new ApplicationException(
                "error. cannot get. key does not exist");
        }

        return hashNode!.Value;
    }

    public bool TryGetValue(K key, out V? value)
    {
        value = default;

        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            return false;
        }

        value = hashNode!.Value;

        return true;
    }

    public void Add(K key, V value)
    {
        if (ContainsKey(key, out DoublyLinkedList<HashNode>? bucket, out _))
        {
            throw new ApplicationException(
                "error. cannot insert. key already exist");
        }

        bucket!.AddToRear(new(key, value));
        Count++;

        RehashIfSatisfies();
    }

    public bool TryAdd(K key, V value)
    {
        if (ContainsKey(key, out DoublyLinkedList<HashNode>? bucket, out _))
        {
            return false;
        }

        bucket!.AddToRear(new(key, value));
        Count++;

        RehashIfSatisfies();

        return true;
    }

    public void Update(K key, V? value)
    {
        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            throw new ApplicationException(
                "error. cannot update. key does not exist");
        }

        hashNode!.Value = value;
    }

    public bool TryUpdate(K key, V value)
    {
        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            return false;
        }

        hashNode!.Value = value;

        return true;
    }

    private void Set(K key, V? value)
    {
        if (ContainsKey(key, out DoublyLinkedList<HashNode>? bucket, out HashNode? hashNode))
        {
            hashNode!.Value = value;
        }
        else
        {
            bucket!.AddToRear(new(key, value));
            Count++;
            RehashIfSatisfies();
        }
    }

    private void RehashIfSatisfies()
    {
        if ((float)Count / _capacity < _loadFactor)
        {
            return;
        }

        _capacity *= 2;
        DynamicallyAllocatedArray<DoublyLinkedList<HashNode>> newBuckets = new(_capacity);
        int newIndex;
        foreach ((K key, V? value) in GetValues())
        {
            newIndex = GetBucketIndex(key, _capacity);
            if (!ContainsBucket(newIndex, out DoublyLinkedList<HashNode>? bucket, newBuckets))
            {
                bucket = newBuckets.Insert(newIndex, new());
            }

            bucket!.AddToRear(new(key, value));
        }

        _buckets = newBuckets;
    }

    private bool ContainsKey(
        K key,
        out DoublyLinkedList<HashNode>? bucket,
        out HashNode? value)
    {
        bucket = default;
        value = default;

        int bucketIndex = GetBucketIndex(key, _capacity);
        bool containsBucket = ContainsBucket(bucketIndex, out bucket, _buckets);
        bool containsKey = false;
        if (containsBucket)
        {
            containsKey = ContainsKey(
                bucket!,
                keyValue => keyValue.Key.Equals(key),
                out value);
        }
        else
        {
            bucket = _buckets.Insert(bucketIndex, new());
        }

        return containsKey;
    }

    private static bool ContainsKey(
        DoublyLinkedList<HashNode> bucket,
        Predicate<HashNode> filterFunction,
        out HashNode? value)
    => bucket.TryGetValue(filterFunction, out value);

    private static bool ContainsBucket(
        int bucketIndex,
        out DoublyLinkedList<HashNode>? bucket,
        DynamicallyAllocatedArray<DoublyLinkedList<HashNode>> buckets)
    => buckets.TryGet(bucketIndex, out bucket) && bucket is not null;

    private static int GetBucketIndex(K key, int capacity)
    {
        int hashCode = key.GetHashCode();
        int bitMask = hashCode >> 31;
        hashCode = (hashCode ^ bitMask) - bitMask;

        return hashCode % capacity;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<KeyValuePair<K, V?>> GetEnumerator()
    {
        foreach (KeyValuePair<K, V?> value in GetValues())
        {
            yield return value;
        }
    }

    public IEnumerable<KeyValuePair<K, V?>> GetValues()
    {
        foreach (DoublyLinkedList<HashNode> bucket in _buckets)
        {
            foreach (HashNode value in bucket)
            {
                yield return value;
            }
        }
    }
}