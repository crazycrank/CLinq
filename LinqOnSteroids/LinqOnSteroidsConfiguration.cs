using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqOnSteroids
{
    public static class LinqOnSteroidsConfiguration
    {
        public static Func<Expression, Expression> QueryOptimizer = e => e;
    }
}
