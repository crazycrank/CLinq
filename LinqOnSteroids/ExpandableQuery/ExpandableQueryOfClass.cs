using System.Linq;

namespace LinqOnSteroids.ExpandableQuery
{
    internal class ExpandableQueryOfClass<T> : ExpandableQuery<T>
        where T : class
    {
        public ExpandableQueryOfClass(IQueryable<T> inner) : base(inner)
        {
        }
    }
}