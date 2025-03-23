using EVDataApi.Models;

namespace EVDataApi.Interfaces
{
    public interface IEVDataService
    {
        bool GetEVByDOLVehicleID(int DOLVehicleID);
        ICollection<EVDataModel> GetRecordByDOLVehicleID(int DOLVehicleID);
        Task<ICollection<EVDataModel>> GetFilteredData(string model, string make, string county, string city, int? initialModelYear, int? endModelYear);
        Task<PaginationModel<EVDataModel>> GetPaginatedData(int page, int objectsPerPage);

        Task<PaginationModel<EVDataModel>> GetFilteredReportingData(string county, string city, string state, string make, string model, int page, int objectsPerPage);

    }
}
