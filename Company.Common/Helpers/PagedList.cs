using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Company.Common.Helpers
{
    public class PagedList<T>: List<T>
    {
        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage { get; set; }
        
        /// <summary>
        /// Number of the total pages of the results
        /// </summary>
        public int TotalPages { get; set; }
        
        /// <summary>
        /// Number of the total items per page
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// Total number of items
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;

            // Returns the smallest integral value greater than or equal to the specified number.
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        /// <summary>
        /// Create the IQueryable item to return number of items
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PagedList<T>> CreateAsync(List<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
