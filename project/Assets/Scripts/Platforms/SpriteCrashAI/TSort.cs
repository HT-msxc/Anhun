using System;
public class TSort<T> where T : IComparable
{
    public void QuickSort(T[] array, int l, int r)
    {
        if( l >= r)
            return;
        T x = array[l];
        int i = l , j = r ;
        while(i < j)
        {
            while(i < j && CompareT(array[j], x) >=0)
                --j;
            array[i] = array[j];
            while(i < j && CompareT(array[i], x) <= 0)
                ++i;
            array[j] = array[i];
        }
        array[i] = x;
        QuickSort(array, l, j-1);
        QuickSort(array, j + 1, r);
    }

    private static int CompareT(T a1, T a2)
    {
        if(a1.CompareTo(a2) > 0)
            return 1;
        else if (a1.CompareTo(a2) == 0)
            return 0;
        else
            return -1;
    }
}
