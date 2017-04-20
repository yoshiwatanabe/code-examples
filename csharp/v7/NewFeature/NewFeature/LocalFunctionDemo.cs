using System.Linq;

namespace NewFeature
{
    class LocalFunctionDemo
    {
        public static void QuickSort(int[] array, int begin, int last)
        {            
            void Swap(int[] _arrayInternal, int _pos1, int _pos2)
            {
                if (_pos1 != _pos2)
                {
                    int temp = _arrayInternal[_pos1];
                    _arrayInternal[_pos1] = _arrayInternal[_pos2];
                    _arrayInternal[_pos2] = temp;
                }
            }

            int Partition(int[] _array, int _begin, int _last)
            {
                int storeIndex = _begin;

                for (int i = _begin; i < _last; i++)
                {
                    if (_array[i] <= _array[_last])
                    {
                        Swap(_array, i, storeIndex);
                        storeIndex = storeIndex + 1;
                    }
                }

                Swap(_array, storeIndex, _last);

                return storeIndex;
            }
            
            if (begin < last)
            {
                int pivotIndex = Partition(array, begin, last);
                QuickSort(array, begin, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, last);
            }
            else
            {
                return; // Base condition
            }
        }
        
        public static void Run()
        {
            bool IsSorted(int[] a)
            {
                return a.TakeWhile((val, i) => { return i < a.Length - 1 && a[i] < a[i + 1]; })
                .Count()
                .Equals(a.Length - 1);
            }

            int[] array = new int[] { 12, 7, 14, 9, 10, 11 };
            QuickSort(array, 0, 5);
            bool sorted = IsSorted(array);
        }
    }
}
