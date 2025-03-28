using EVDataApi.Helpers;
using EVDataApi.Interfaces;
using EVDataApi.Models;
using EVDataApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace EVDataApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EVDataController : ControllerBase
    {
        private readonly IEVDataService _evDataService;

        public EVDataController(IEVDataService eVDataService)
        {
            _evDataService = eVDataService;
        }

        [HttpGet]
        public async Task<ActionResult<bool>> Get()
        {
            var c = _evDataService.GetEVByDOLVehicleID(12);
            return Ok(c);
        }

        [HttpGet("{GetRecordByDOLVehicleID}")]
        public async Task<ActionResult<EVDataModel>> GetRecordById(int GetRecordByDOLVehicleID)
        {
            var c = _evDataService.GetRecordByDOLVehicleID(GetRecordByDOLVehicleID);
            return Ok(c);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<EVDataModel>> GetFilteredData(string? model, string? make, string? county, string? city, int? initialModelYear, int? endModelYear)
        {
            var c = await _evDataService.GetFilteredData(model, make, county, city, initialModelYear, endModelYear);
            return Ok(c);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<EVDataModel>> GetPaginatedData(int page = 1, int objectsPerPage = 10)
        {
            var c = await _evDataService.GetPaginatedData(page, objectsPerPage);

            return Ok(c);
        }

        [HttpGet("reportdataget")]
        public async Task<ActionResult<EVDataModel>> GetFilteredReportingDataGet(string? county, string? city, string? state, string? make, string? model, int page = 1, int objectsPerPage = 10)
        {
            var c = await _evDataService.GetFilteredReportingData(county, city, state, make, model, page, objectsPerPage);
            return Ok(c);
        }

        [HttpPost("reportdatapost")]
        [EVModelActionFilter]
        public async Task<ActionResult<EVDataModel>> GetFilteredReportingDataPost([FromBody] ReportRequest data, int page = 1, int objectsPerPage = 10)
        {

            var c = await _evDataService.GetFilteredReportingData(data, page, objectsPerPage);
            return Ok(c);
        }
    }
}