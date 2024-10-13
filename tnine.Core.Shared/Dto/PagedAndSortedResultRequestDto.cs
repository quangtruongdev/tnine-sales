namespace tnine.Core.Shared.Dto
{
    public class PagedAndSortedResultRequestDto
    {
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public string Sorting { get; set; }
    }
}
