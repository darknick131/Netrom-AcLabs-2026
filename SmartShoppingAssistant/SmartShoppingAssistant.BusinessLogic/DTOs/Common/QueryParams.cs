namespace SmartShoppingAssistant.BusinessLogic.DTOs.Common
{
    public class QueryParams
    {
        private int _page = 1;
        private int _pageSize = 10;

        public int Page
        {
            get => _page;
            set => _page = Math.Max(1, value);
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Max(1, value);
        }

        public string? Search { get; set; }

        public string? SortBy { get; set; }

        public string SortDir { get; set; } = "asc";

        public int? CategoryId { get; set; }
    }
}
