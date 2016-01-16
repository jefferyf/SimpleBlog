using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Infrastructure
{
    public class PagedData<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _currentItems;

        public int TotalCount { get; private set; }
        public int Page { get; private set; }
        public int PerPage { get; private set; }
        public int PageRange { get; private set; }
        public int MaxDisplayPage { get; private set; }
        public int MinDisplayPage { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasNextPage { get; private set; }
        public bool HasPreviousPage { get; private set; }

        public bool HasNextEllipsis {
            get
            {
                return !((Page > (TotalPages - PageRange)) && (Page <= TotalPages));
            }
        }

        public bool HasPreviousEllipsis
        {
            get
            {
                return !((Page > 0) && (Page <= PageRange));
            }
        }

        public int NextPage
        {
            get
            {
                if (!HasNextPage)
                    throw new InvalidOperationException();

                return Page + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (!HasPreviousPage)
                    throw new InvalidOperationException();

                return Page - 1;
            }
        }

        public PagedData(IEnumerable<T> currenItems, int totalCount, int page, int perpage, int pagerange)
        {
            _currentItems = currenItems;
            TotalCount = totalCount;
            Page = page;
            PerPage = perpage;
            PageRange = pagerange;

            TotalPages = (int)Math.Ceiling((float)TotalCount / PerPage);
            MaxDisplayPage = Math.Min(Page + PageRange - 1, TotalPages);
            MinDisplayPage = Math.Max(0, Page - PageRange);
       
            HasNextPage = Page < TotalPages;
            HasPreviousPage = Page > 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _currentItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}