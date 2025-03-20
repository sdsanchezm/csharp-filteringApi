using Azure;
using Microsoft.EntityFrameworkCore;

namespace EVDataApi.Models
{
    public class PaginationModel<T>where T : class
    {
        public int count { get; set; }
        public int numberOfPages { get; set; }
        public IList<T> results { get; set; }

        public static PaginationModel<T> getPagination(IQueryable<T> iq, int pageNumber, int objectsPerPage)
        {
            if (pageNumber < 0) pageNumber = 1;
            if (objectsPerPage < 0) pageNumber = 10;
            if (objectsPerPage > 100) pageNumber = 100;

            var paginationResult = new PaginationModel<T>();
            var totalObjects = iq.Count();
            var elementsToSkip = objectsPerPage * (pageNumber - 1);

            paginationResult.count = totalObjects;
            paginationResult.numberOfPages = (int)Math.Ceiling(((decimal)totalObjects / (decimal)objectsPerPage));
            paginationResult.results = iq.Skip(elementsToSkip).Take(objectsPerPage).ToList();

            return paginationResult;
        }

    }
}
