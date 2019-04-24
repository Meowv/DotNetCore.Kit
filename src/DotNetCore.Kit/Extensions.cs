using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore.Kit
{
    public static class Extensions
    {
        public static StringBuilder AppendIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (T val in values)
            {
                if (predicate(val))
                {
                    @this.Append(val);
                }
            }
            return @this;
        }
    }
}