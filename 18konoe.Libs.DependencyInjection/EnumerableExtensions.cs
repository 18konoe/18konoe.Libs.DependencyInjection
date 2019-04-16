using System;
using System.Collections.Generic;

namespace _18konoe.Libs.DependencyInjection
{
    public static class EnumerableExtensions
    {
        public static void ForAll<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence)
            {
                action(item);
            }
        }
    }
}
