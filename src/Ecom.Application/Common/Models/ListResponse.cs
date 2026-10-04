using System.Collections.Generic;

namespace Ecom.Application.Common.Models
{
    public class ListResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        
        public ListResponse(IEnumerable<T> data)
        {
            Data = data;
        }
    }
}
