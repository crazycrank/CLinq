using System.Linq;

namespace CLinq.ComposableQuery
{
    internal class ComposableQueryOfClass<T> : ComposableQuery<T>
        where T : class
    {
        public ComposableQueryOfClass(IQueryable<T> inner) : base(inner)
        {
        }
    }
}