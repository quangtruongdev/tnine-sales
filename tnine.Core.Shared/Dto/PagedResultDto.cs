namespace tnine.Core.Shared.Dto
{
    public class PagedResultDto
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
