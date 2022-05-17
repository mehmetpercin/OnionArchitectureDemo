namespace Domain.Common
{
    public class PageableResponse<T>
    {
        public T Data { get; set; }
        public int TotalCount { get; set; }
    }
}
