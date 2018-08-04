using System;
using System.Collections.Generic;
using System.Text;

internal static class InternalExtensions
{
    internal static T AddAndGet<T>(this List<T> @this, T element)
    {
        @this.Add(element);
        return element;
    }
}