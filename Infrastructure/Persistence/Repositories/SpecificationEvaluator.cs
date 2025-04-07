using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence.Repositories
{
    public class SpecificationEvaluator
    {
         //ProductWithBrandAndTypeSpecifications specifications = new ProductWithBrandAndTypeSpecifications();
        public SpecificationEvaluator() {
            
        }
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery,Specifications<T> specifications)where T : class
        {
            //Step 01
            var query = inputQuery;
            //step 02
            if (specifications.Criteria is not null) query = query.Where(specifications.Criteria);
            query = specifications.IncludeExpression.Aggregate(
                query,
                (currentquery, IncludeExpression) => currentquery.Include(IncludeExpression));

            if(specifications.OrderBy is not null)
            {
                query=query.OrderBy(specifications.OrderBy);
            }
            else if (specifications.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }
            return query;
        }
    }
}
