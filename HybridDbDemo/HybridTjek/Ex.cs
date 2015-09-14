using System;
using System.Collections.Generic;
using System.Linq;

namespace HybridTjek
{
    public static class Ex
    {
        public static T ItemFrom<T>(this Random random, IEnumerable<T> items)
        {
            var list = items.ToList();

            return list[random.Next(list.Count)];
        }

        public static void Times(this int count, Action action)
        {
            for (var counter = 0; counter < count; counter++)
            {
                action();
            }
        }
    }
}