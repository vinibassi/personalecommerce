using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestesDeAceitacao
{
    public static class EntityExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T:class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
