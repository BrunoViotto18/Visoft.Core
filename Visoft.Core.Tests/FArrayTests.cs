using IndexOutOfRangeException = System.IndexOutOfRangeException;

namespace Visoft.Core.Tests;

public class FArrayTests
{
    [Fact]
    public void Constructor_ShouldAllocateTheSpecifiedLength_WhenInstantiatedWithANumber()
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(100);
        
        // Act
        var count = 0;
        for (var i = 0; i < 100; i++)
        {
            var _ = fArray[i];
            count++;
        }

        // Assert
        Assert.Equal(100, count);
    }

    [Fact]
    public void Constructor_ShouldSetLengthToTheAllocatedLength_WhenInstantiated()
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(100);
        
        // Act
        var result = fArray.Length;

        // Assert
        Assert.Equal(100, result);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(99)]
    public void Get_ShouldGetItemAtSpecifiedIndex_WhenIndexIsValid(int index)
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(100);
        for (var i = 0; i < 100; i++)
            fArray[i] = i;

        // Act
        var result = fArray[index];

        // Assert
        Assert.Equal(index, result);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 7, 7)]
    [InlineData(4, 5, 45)]
    [InlineData(7, 0, 70)]
    [InlineData(9, 9, 99)]
    public void Get_ShouldGetItemAtSpecifiedIndexes_WhenFArrayIsMultiDimensional
        (int indexI, int indexJ, int expected)
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(10, 10);
        for (var i = 0; i < 10; i++)
            for (var j = 0; j < 10; j++)
                fArray[i, j] = 10 * i + j;

        // Act
        var result = fArray[indexI, indexJ];

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(100)]
    public void Get_ShouldThrowIndexOutOfRangeException_WhenIndexIsInvalid(int index)
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(100);
        
        // Act
        void Action() { var _ = fArray[index]; }

        // Assert
        Assert.Throws<IndexOutOfRangeException>(Action);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(99)]
    public void Set_ShouldSetItemAtSpecifiedIndex_WhenIndexIsValid(int index)
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(100);
        for (var i = 0; i < 100; i++)
            fArray[i] = i;

        // Act
        fArray[index] = -1;

        // Assert
        Assert.Equal(-1, fArray[index]);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 7)]
    [InlineData(4, 5)]
    [InlineData(7, 0)]
    [InlineData(9, 9)]
    public void Set_ShouldSetItemAtSpecifiedIndexes_WhenFArrayIsMultiDimensional
        (int indexI, int indexJ)
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(10, 10);
        for (var i = 0; i < 10; i++)
        for (var j = 0; j < 10; j++)
            fArray[i, j] = 10 * i + j;

        // Act
        fArray[indexI, indexJ] = -1;

        // Assert
        Assert.Equal(-1, fArray[indexI, indexJ]);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(100)]
    public void Set_ShouldThrowIndexOutOfRangeException_WhenIndexIsInvalid(int index)
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(100);
        
        // Act
        void Action() => fArray[index] = -1;

        // Assert
        Assert.Throws<IndexOutOfRangeException>(Action);
    }

    [Theory]
    [InlineData("Length")]
    [InlineData("IsReadOnly")]
    [InlineData("IsFixedSize")]
    [InlineData("Empty")]
    [InlineData("Rank")]
    public void Property_ShouldOnlyHaveTheGetProperty_Always(string propertyName)
    {
        // Arrange
        var fArrayType = typeof(FArray<int>);
        var fArrayProp = fArrayType.GetProperty(propertyName);

        // Act
        var getter = fArrayProp!.GetGetMethod(false);
        var setter = fArrayProp.GetSetMethod(false);

        // Assert
        Assert.NotNull(getter);
        Assert.Null(setter);
    }
    
    [Fact]
    public void IsReadOnly_ShouldReturnFalse_Always()
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(100);
        
        // Act
        var result = fArray.IsReadOnly;
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsFixedSize_ShouldReturnTrue_Always()
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(100);
        
        // Act
        var result = fArray.IsFixedSize;
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Empty_ShouldContainAnEmptyArray_Always()
    {
        // Arrange

        // Act
        FArray<int> fArray = FArray<int>.Empty;

        // Assert
        Assert.Equal(0, fArray.Length);
    }
    
    [Fact]
    public void Rank_ShouldReturnTheFArrayDimension_Always()
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(10, 10);
        
        // Act
        var result = fArray.Rank;
        
        // Assert
        Assert.Equal(2, result);
    }

    [Theory]
    // Copies the whole FArray into the whole Array Array
    [InlineData(100, 100, 0, 0, 100)]

    // Copies the whole FArray into parts of Array
    [InlineData(50, 100, 0, 0, 50)]
    [InlineData(50, 100, 0, 25, 50)]
    [InlineData(50, 100, 0, 50, 50)]
    // Copies parts of FArray into the whole Array
    [InlineData(100, 50, 0, 0, 50)]
    [InlineData(100, 50, 25, 0, 50)]
    [InlineData(100, 50, 50, 0, 50)]

    // Copies parts of FArray into the start of Array
    [InlineData(100, 100, 0, 0, 50)]
    [InlineData(100, 100, 25, 0, 50)]
    [InlineData(100, 100, 50, 0, 50)]
    // Copies parts of FArray into the middle of Array
    [InlineData(100, 100, 0, 25, 50)]
    [InlineData(100, 100, 25, 25, 50)]
    [InlineData(100, 100, 50, 25, 50)]
    // Copies parts of FArray into the end of Array
    [InlineData(100, 100, 0, 50, 50)]
    [InlineData(100, 100, 25, 50, 50)]
    [InlineData(100, 100, 50, 50, 50)]
    public void CopyTo_ShouldCopyItselfToAnArray_WhenArrayAndFArrayAreUniDimensionalAndCompatible
        (int sourceLength, int destinationLength, int sourceIndex, int destinationIndex, int length)
    {
        // Arrange
        FArray<int> fArray = new FArray<int>(sourceLength);
        int[] array = new int[destinationLength];
        for (var i = 0; i < sourceLength; i++)
            fArray[i] = 1;
        for (var i = 0; i < destinationLength; i++)
            array[i] = -1;

        // Act
        fArray.CopyTo(array, sourceIndex, destinationIndex, length);

        // Assert
        for (var i = 0; i < destinationIndex; i++)
            Assert.Equal(-1, array[i]);
        for (var i = destinationIndex; i < destinationIndex + length; i++)
            Assert.Equal(1, array[i]);
        for (var i = destinationIndex + length; i < destinationLength; i++)
            Assert.Equal(-1, array[i]);
    }
}
