using System.Text.Json.Serialization;

namespace Booking.Application.Common.Pagination
{
    public class QueryStringParameters
    {
        public string SortField { get; set; } = "CreateDate";
        public string SortOrder { get; set; } = "Desc";

        const int maxPageSize = 100;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int ProviderId { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int WorkPlaceId { get; set; }
        public int CategoryId { get; set; }
        public DateTime? FromDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        [JsonIgnore]
        public int UserId   { get; set; }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
