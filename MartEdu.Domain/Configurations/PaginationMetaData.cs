using System;


namespace MartEdu.Domain.Configurations
{
    public class PaginationMetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PaginationMetaData(int totalCount, PaginationParams @params)
        {
            CurrentPage = @params.PageIndex;
            TotalCount = totalCount;

            TotalPages = (int)Math.Ceiling(totalCount / (double)@params.PageSize);
        }
    }
}
