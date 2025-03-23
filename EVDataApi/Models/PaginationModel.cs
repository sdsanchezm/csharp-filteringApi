using Azure;
using Microsoft.EntityFrameworkCore;

namespace EVDataApi.Models
{
    public class PaginationModel<T>where T : class
    {
        public int Count { get; set; }
        public int NumberOfPages { get; set; }
        public IList<T> Results { get; set; }

        public static PaginationModel<T> GetPagination(IQueryable<T> iq, int pageNumber, int objectsPerPage)
        {
            if (pageNumber < 0) pageNumber = 1;
            if (objectsPerPage < 0) pageNumber = 10;
            if (objectsPerPage > 100) pageNumber = 100;

            var paginationResult = new PaginationModel<T>();
            var totalObjects = iq.Count();
            var elementsToSkip = objectsPerPage * (pageNumber - 1);

            paginationResult.Count = totalObjects;
            paginationResult.NumberOfPages = (int)Math.Ceiling(((decimal)totalObjects / (decimal)objectsPerPage));
            paginationResult.Results = iq.Skip(elementsToSkip).Take(objectsPerPage).ToList();

            return paginationResult;
        }

    }
}
