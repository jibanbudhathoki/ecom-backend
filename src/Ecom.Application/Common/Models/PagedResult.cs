using System;
using System.Collections.Generic;

namespace Ecom.Application.Common.Models
{
    public class PagedResult<T>
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; } = "Data retrieved successfully";
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(TotalCount / (double)PageSize) : 0;
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
