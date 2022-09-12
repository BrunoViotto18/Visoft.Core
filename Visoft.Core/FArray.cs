using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Visoft.Core;

// MaxLength

// Copy()
// Clear()
// GetFlattenedIndex()
// GetLength()
// GetUpperBound()
// GetLowerBound()
// GetEnumerator()
// CopyTo()
// Contains()

// AsReadOnly()
// Resize()
// Clone()
// BinarySearch()
// ConvertAll()
// Exists()
// Fill()
// Find()
// FindAll()
// FindIndex()
// FindLast()
// FindLastIndex()
// IndexOf()
// LastIndexOf()
// Reverse()
// Sort()


// GetMedian() >> Wtf?
// SorterObjectArray() ???
// SorterGenericArray() ??

public class FArray<T>
{
    private T[] _array;
    public int Length { get; }
    public int Rank { get; }
    public bool IsReadOnly => false;
    public bool IsFixedSize => true;

    public static FArray<T> Empty { get; } = new(0);

    // TODO: Da para melhorar muito
    public T this[params int[] indexes]
    {
        get => _array[(int)indexes
            .Reverse()
            .Select((val, index) => Math.Pow(10, index) * val)
            .Sum()];
        set => _array[(int)indexes
            .Reverse()
            .Select((val, index) => Math.Pow(10, index) * val)
            .Sum()] = value;
    }

    public FArray(params int[] dimensionLengths)
    {
        Length = dimensionLengths.Aggregate((value, accumulator) => value * accumulator);
        _array = new T[Length];
        Rank = dimensionLengths.Length;
    }

    // TODO: Implementar este método corretamente
    public void CopyTo(T[] array, int sourceIndex, int destinationIndex, int length)
    {
        Array.Copy(_array, sourceIndex, array, destinationIndex, length);
    }
}
