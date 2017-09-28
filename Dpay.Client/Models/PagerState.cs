using System.Collections.Generic;

namespace Dpay.Client.Models
{
    public class PagerState
    {
        public class PagerStateDto
        {
            public bool HasNextPage { get; set; }
            public bool HasPreviousPage { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }

            public IList<int> Range()
            {
                var firstIndex =
                    (this.TotalPages <= 9)
                        ? 0
                        : (this.PageIndex <= 4)
                        ? 0
                        : (this.PageIndex >= (this.TotalPages - 1 - 4)
                            ? (this.TotalPages - 1 - 2 * 4)
                            : (this.PageIndex - 4));

                var lastIndex =
                    (this.TotalPages <= 9)
                        ? (this.TotalPages - 1)
                        : (this.PageIndex <= 4)
                        ? (2 * 4)
                        : (this.PageIndex >= (this.TotalPages - 1 - 4)
                            ? (this.TotalPages - 1)
                            : (this.PageIndex + 4));


                var ret = new List<int>();
                for (var n = firstIndex; n <= lastIndex; n++)
                    ret.Add(n);

                return ret;
            }
        }
    }
}
