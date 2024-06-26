﻿namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ArrayCustomization : ArrayCustomizationBase
{
    protected override IEnumerable<Type> Types { get; } = [typeof(Array)];

    protected override object Convert<T>(IEnumerable<T> source) => source.ToArray();

    protected override object CreateMultiDimensionalArray(IDummy dummy, Type arrayType, Type elementType)
    {
        var rank = arrayType.GetArrayRank();
        var lengths = new int[rank];
        for (var i = 0; i < rank; i++)
        {
            lengths[i] = dummy.Options.DefaultCollectionSize;
        }

        var array = Array.CreateInstance(elementType, lengths);
        PopulateMultiDimensionalArray(dummy, array, elementType, new int[rank], 0);
        return array;
    }

    private void PopulateMultiDimensionalArray(IDummy dummy, Array array, Type elementType, int[] indices, int dimension)
    {
        if (dimension == indices.Length)
        {
            array.SetValue(dummy.Create(elementType), indices);
        }
        else
        {
            for (var i = 0; i < array.GetLength(dimension); i++)
            {
                indices[dimension] = i;
                PopulateMultiDimensionalArray(dummy, array, elementType, indices, dimension + 1);
            }
        }
    }
}