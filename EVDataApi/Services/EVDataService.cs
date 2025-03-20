using EVDataApi.Data;
using EVDataApi.Interfaces;
using EVDataApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EVDataApi.Services
{
    public class EVDataService : IEVDataService
    {
        private readonly DataContext _context;
        public EVDataService(DataContext context)
        {
            _context = context;
        }

        public bool GetEVByDOLVehicleID(int DOLVehicleID)
        {
            return false;
        }

        public async Task<ICollection<EVDataModel>> GetFilteredData(string model, string make, string county, string city, int? initialModelYear, int? endModelYear)
        {
            //var c = _context
            //        .Electric_Vehicle_Population_Data
            //        .Where(p => p.Model == model)
            //        .ToList();

            //var t = (from car in _context.Electric_Vehicle_Population_Data
            //         where car.Model == model:null
            //         se
            //         )

            var q = _context.Electric_Vehicle_Population_Data.AsQueryable();

            if (!string.IsNullOrWhiteSpace(model))
            {
                q = q.Where(p => p.Model == model);
            }

            if (!string.IsNullOrWhiteSpace(make))
            {
                q = q.Where(p => p.Make == make);
            }

            if (!string.IsNullOrWhiteSpace(county))
            {
                q = q.Where(p => p.County == county);
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                q = q.Where(p => p.City == city);
            }

            int x;
            if (int.TryParse(initialModelYear.ToString(), out x) && x > 0)
            {
                q = q.Where(p => p.ModelYear >= initialModelYear);
            }

            if (int.TryParse(endModelYear.ToString(), out x) && x > 0)
            {
                q = q.Where(p => p.ModelYear <= endModelYear);
            }

            return await q.ToListAsync();
        }

        public async Task<PaginationModel<EVDataModel>> GetPaginatedData(int page, int objectsPerPage)
        {
            IQueryable<EVDataModel> queryElectricVehicle = _context.Electric_Vehicle_Population_Data;

            var p = PaginationModel<EVDataModel>.getPagination(queryElectricVehicle, page, objectsPerPage);

            return p;
        }

        public ICollection<EVDataModel> GetRecordByDOLVehicleID(int DOLVehicleID)
        {
            return _context.Electric_Vehicle_Population_Data.Where(p => p.DOLVehicleID == DOLVehicleID).ToList();
        }
    }
}
