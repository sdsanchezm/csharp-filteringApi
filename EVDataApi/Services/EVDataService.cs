using EVDataApi.Data;
using EVDataApi.Interfaces;
using EVDataApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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

            var query = _context.Electric_Vehicle_Population_Data.AsQueryable();

            query = ValidateAndFilter(query, county, city, null, make, model);

            int x;
            if (int.TryParse(initialModelYear.ToString(), out x) && x > 0)
            {
                query = query.Where(p => p.ModelYear >= initialModelYear);
            }

            if (int.TryParse(endModelYear.ToString(), out x) && x > 0)
            {
                query = query.Where(p => p.ModelYear <= endModelYear);
            }

            return await query.ToListAsync();
        }

        public async Task<PaginationModel<EVDataModel>> GetFilteredReportingData(string county, string city, string state, string make, string model, int page, int objectsPerPage)
        {
            IQueryable<EVDataModel> queryElectricVehicle = _context.Electric_Vehicle_Population_Data;

            queryElectricVehicle = ValidateAndFilter(queryElectricVehicle, county, city, state, make, model);

            var paginatedData = PaginationModel<EVDataModel>.GetPagination(queryElectricVehicle, page, objectsPerPage);

            return await paginatedData;
        }

        public async Task<PaginationModel<EVDataModel>> GetFilteredReportingData(BodyRequestDto data, int page, int objectsPerPage)
        {
            IQueryable<EVDataModel> queryElectricVehicle = _context.Electric_Vehicle_Population_Data;

            queryElectricVehicle = ValidateAndFilter(queryElectricVehicle, data.County, data.City, data.State, data.Make, data.Model);

            var paginatedData = PaginationModel<EVDataModel>.GetPagination(queryElectricVehicle, page, objectsPerPage);

            return await paginatedData;
        }

        public async Task<PaginationModel<EVDataModel>> GetPaginatedData(int page, int objectsPerPage)
        {
            IQueryable<EVDataModel> queryElectricVehicle = _context.Electric_Vehicle_Population_Data;

            var paginatedData = PaginationModel<EVDataModel>.GetPagination(queryElectricVehicle, page, objectsPerPage);

            return await paginatedData;
        }

        public ICollection<EVDataModel> GetRecordByDOLVehicleID(int DOLVehicleID)
        {
            return _context.Electric_Vehicle_Population_Data.Where(p => p.DOLVehicleID == DOLVehicleID).ToList();
        }

        private IQueryable<EVDataModel> ValidateAndFilter(IQueryable<EVDataModel> query, string county, string city, string state, string make, string model)
        {
            //var queryElectricVehicle = query;

            if (!string.IsNullOrWhiteSpace(county))
            {
                query = query.Where(ev => ev.County == county);
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                query = query.Where(ev => ev.City == city);
            }

            if (!string.IsNullOrWhiteSpace(model))
            {
                query = query.Where(ev => ev.Model == model);
            }

            if (!string.IsNullOrWhiteSpace(state))
            {
                query = query.Where(ev => ev.State == state);
            }

            if (!string.IsNullOrWhiteSpace(make))
            {
                query = query.Where(ev => ev.Make == make);
            }

            return query;
        }
    }
}
