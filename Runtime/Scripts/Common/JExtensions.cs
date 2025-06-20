using System;
using System.Collections.Generic;
using System.Linq;

namespace JFramework
{
    public static class JExtensions
    {
        public static List<T> GetRandomElements<T>(this List<T> source, int count)
        {
            return source.OrderBy(x => Guid.NewGuid()).Take(count).ToList();
        }
    }
}


