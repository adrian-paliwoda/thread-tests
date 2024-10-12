using System.Collections.Generic;

namespace MultithreadingLinqAndPlinqCompare
{
    internal class GeneratorIEnumerable
    {
        public List<int> GenertateIntList(int sizeOfData)
        {
            var list = new List<int>();

            for (int item = 0; item < sizeOfData; item++)
            {
                list.Add(item);
            }

            return list;
        }
    }
}