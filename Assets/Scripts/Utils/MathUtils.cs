using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;

    public static class MathUtils
    {
        public static int Max(int[] i_Array)
        {
            int max = int.MinValue;
            foreach (int i in i_Array)
            {
                max = i > max ? i : max;
            }
            return max;
        }

        public static int SumOfArray(int[] i_Array)
        {
            int sum = 0;
            foreach (int i in i_Array)
            {
                sum += i;
            }
            return sum;
        }
    }


