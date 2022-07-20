using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Models.Helpers
{
    public class QueryCompanyParams
    {
        /// <summary>
        /// Max number of results per page
        /// </summary>
        private const int MaxPageSize = 15;
        
        /// <summary>
        /// Number of page
        /// </summary>
        public int PageNumber { get; set; } = 1;
        
        /// <summary>
        /// Init page page size
        /// </summary>
        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}
