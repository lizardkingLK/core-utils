using System.Collections;
using static wordSearch.Core.Library.Linear.Arrays.Shared.Constants;

namespace wordSearch.Core.Library.Linear.Arrays;

public class DynamicallyAllocatedArray<T> : IEnumerable<T>
{
    private T?[] _values;

    private int _capacity;

    public int Count { get; set; }

    public T? this[int index]
    {
        get => Get(index);
        set => Update(index, value);
    }

    public DynamicallyAllocatedArray()
    {
        _capacity = InitialCapacity;
        _values = new T[InitialCapacity];
    }

    public DynamicallyAllocatedArray(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 0);

        _capacity = capacity == 0 ? InitialCapacity : capacity;
        _values = new T[_capacity];
    }

    public T? Add(T value) => Insert(Count, value);

    public T? Insert(int index, T value)
    {
        if (IsInvalidIndex(index))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        _values[index] = value;
        Count++;

        ResizeIfSatisfies();

        return _values[index];
    }

    public T? Get(int index)
    {
        if (IsListEmpty())
        {
            throw new InvalidOperationException("error. cannot remove. list is empty");
        }

        if (IsInvalidIndex(index))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return _values[index];
    }

    public bool TryGet(int index, out T? value)
    {
        value = default;

        if (IsListEmpty() || IsInvalidIndex(index))
        {
            return false;
        }

        value = _values[index];

        return true;
    }

    public void Update(int index, T? value)
    {
        if (IsListEmpty())
        {
            throw new InvalidOperationException("error. cannot remove. list is empty");
        }

        if (IsInvalidIndex(index))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        _values[index] = value;
    }

    public void Delete(int index)
    {
        if (IsListEmpty())
        {
            throw new InvalidOperationException("error. cannot remove. list is empty");
        }

        if (IsInvalidIndex(index))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        _values[index] = default;
        Count--;

        ShrinkIfSatisfies();
    }

    public bool TryDelete(int index)
    {
        if (IsListEmpty() || IsInvalidIndex(index))
        {
            return false;
        }

        _values[index] = default;
        Count--;

        ShrinkIfSatisfies();

        return true;
    }

    private void ShrinkIfSatisfies()
    {
        if ((float)Count / _capacity >= GrowthFactor)
        {
            return;
        }

        _capacity = Count < InitialCapacity ? InitialCapacity : Count;

        T?[] values = new T[_capacity];
        for (int i = 0; i < _capacity; i++)
        {
            values[i] = _values[i];
        }

        _values = values;
    }

    private void ResizeIfSatisfies()
    {
        if ((float)Count / _capacity < GrowthFactor)
        {
            return;
        }

        T?[] values = new T[_capacity * 2];
        for (int i = 0; i < _capacity; i++)
        {
            values[i] = _values[i];
        }

        _capacity = values.Length;
        _values = values;
    }

    private bool IsListEmpty() => Count == 0;

    private bool IsInvalidIndex(int index) => index < 0 || index >= _capacity;

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T? value in _values)
        {
            if (value != null)
            {
                yield return value;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}