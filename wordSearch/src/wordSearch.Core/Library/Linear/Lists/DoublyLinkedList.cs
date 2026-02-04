using System.Collections;

namespace wordSearch.Core.Library.Linear.Lists;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private record LinkNode(T Value)
    {
        public LinkNode? Previous { get; set; }
        public T Value { get; set; } = Value;
        public LinkNode? Next { get; set; }
    }

    private LinkNode? _head;

    private LinkNode? _tail;

    public void AddToFront(T value)
    {
        LinkNode newNode = new(value);
        if (_head == null)
        {
            _head = newNode;
            if (_tail == null)
            {
                _tail = _head;
            }

            return;
        }

        newNode.Next = _head;
        _head.Previous = newNode;
        _head = newNode;
    }

    public void AddToRear(T value)
    {
        LinkNode newNode = new(value);
        if (_tail == null)
        {
            _tail = newNode;
            if (_head == null)
            {
                _head = _tail;
            }

            return;
        }

        newNode.Previous = _tail;
        _tail.Next = newNode;
        _tail = newNode;
    }

    public T? RemoveFromFront()
    {
        if (_head == null)
        {
            return default;
        }

        LinkNode? next = _head.Next;
        if (next != null)
        {
            next.Previous = null;
        }

        _head.Next = null;
        LinkNode removed = _head;
        if (_tail == _head)
        {
            _tail = null;
        }

        _head = next;

        return removed.Value;
    }

    public bool TryRemoveFromFront(out T? removed)
    {
        removed = default;

        if (_head == null)
        {
            return false;
        }

        LinkNode? next = _head.Next;
        if (next != null)
        {
            next.Previous = null;
        }

        _head.Next = null;
        removed = _head.Value;
        if (_tail == _head)
        {
            _tail = null;
        }

        _head = next;

        return true;
    }

    public T? RemoveFromRear()
    {
        if (_tail == null)
        {
            return default;
        }

        LinkNode? previous = _tail.Previous;
        if (previous != null)
        {
            previous.Next = null;
        }

        _tail.Previous = null;
        LinkNode removed = _tail;
        if (_head == _tail)
        {
            _head = null;
        }

        _tail = previous;

        return removed.Value;
    }

    public bool TryRemoveFromRear(out T? removed)
    {
        removed = default;

        if (_tail == null)
        {
            return false;
        }

        LinkNode? previous = _tail.Previous;
        if (previous != null)
        {
            previous.Next = null;
        }

        _tail.Previous = null;
        removed = _tail.Value;
        if (_head == _tail)
        {
            _head = null;
        }

        _tail = previous;

        return true;
    }

    public void Remove(T value)
    {
        LinkNode? current = _head;
        LinkNode? previous = current;
        while (current != null)
        {
            if (!current.Value!.Equals(value))
            {
                previous = current;
                current = current.Next;
                continue;
            }

            if (current == _head)
            {
                _ = TryRemoveFromFront(out _);
            }
            else if (current == _tail)
            {
                _ = TryRemoveFromRear(out _);
            }

            if (current.Next == null)
            {
                previous!.Next = null;
            }
            else
            {
                current.Next.Previous = previous;
                previous!.Next = current.Next.Next;
            }

            break;
        }
    }

    public bool TryRemove(T value)
    {
        LinkNode? current = _head;
        LinkNode? previous = current;
        while (current != null)
        {
            if (!current.Value!.Equals(value))
            {
                previous = current;
                current = current.Next;
                continue;
            }

            if (current == _head)
            {
                return TryRemoveFromFront(out _);
            }
            else if (current == _tail)
            {
                return TryRemoveFromRear(out _);
            }

            if (current.Next == null)
            {
                previous!.Next = null;
            }
            else
            {
                current.Next.Previous = previous;
                previous!.Next = current.Next.Next;
            }

            return true;
        }

        return false;
    }

    public bool TryGetValue(
        Predicate<T> filterFunction,
        out T? returned)
    {
        returned = default;

        foreach (T value in GetValues())
        {
            if (filterFunction.Invoke(value))
            {
                returned = value;
                return true;
            }
        }

        return false;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T value in GetValues())
        {
            yield return value;
        }
    }

    public IEnumerable<T> GetValues()
    {
        LinkNode? current = _head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }
}