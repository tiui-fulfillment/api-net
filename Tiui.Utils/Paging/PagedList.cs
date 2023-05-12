using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiui.Utils.Paging
{
    public abstract class PagedList<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages
        {
            get
            {
                if (this.TotalRows <= 0 || this.PageSize <= 0)
                    return 0;
                return (int)Math.Ceiling((double)this.TotalRows / this.PageSize);
            }            
        }
    }
}
