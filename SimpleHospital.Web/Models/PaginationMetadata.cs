using Microsoft.AspNetCore.Identity;

namespace SimpleHospital.Web.Models
{
    public class PaginationMetadata
    {
        public PaginationMetadata(int currentPage, int totalCount, int itemsPerPage)
        {
            this.itemsPerPage = itemsPerPage;
            this.currentPage = currentPage;
            this.totalCount = totalCount;
            this.totalPages = (int)Math.Ceiling(totalCount / (double)itemsPerPage);
        }

        public int itemsPerPage {get;private set;}
        public int currentPage { get; private set; }
        public int totalCount { get; private set; }
        public int totalPages { get; private set; }
        public bool hasPrevious() => currentPage > 1;
        public bool hasNext() => currentPage < totalPages;
    }
}