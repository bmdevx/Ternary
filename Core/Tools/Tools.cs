using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Tools
{
    public static class Tools
    {
    }

    public static class Create
    {
        public static T[] NewArray<T>(int arraySize, Func<int, T> create)
        {
            T[] array = new T[arraySize];

            for (int i = 0; i < arraySize; i++)
                array[i] = create(i);

            return array;
        }
        
        public static T[] NewTryteSizedArray<T>(Func<int, T> create)
        {
            T[] array = new T[Tryte.NUMBER_OF_TRITS];

            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
                array[i] = create(i);

            return array;
        }

        public static List<T> NewList<T>(int numberOfItems, Func<int, T> create)
        {
            List<T> list = new List<T>(numberOfItems);

            for (int i = 0; i < numberOfItems; i++)
                list.Add(create(i));

            return list;
        }
    }
}
