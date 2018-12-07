using Microsoft.EntityFrameworkCore;

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
