using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVDataApi.Models
{
    public class EVDataModel
    {
        [Key]
        [Column("id")]
        public int id { get; set;  }
        [Column("VIN (1-10)")]
        public string Vin { get; set; }
        [Column("County")]
        public string County { get; set; }
        [Column("City")]
        public string City { get; set; }
        [Column("State")]
        public string State { get; set; }
        [Column("Postal Code")]
        public long PostalCode { get; set; }
        [Column("Model Year")]
        public long ModelYear { get; set; }
        [Column("Make")]
        public string Make { get; set; }
        [Column("Model")]
        public string Model { get; set; }
        [Column("Electric Vehicle Type")]
        public string ElectricVehicleType { get; set; }
        [Column("Clean Alternative Fuel Vehicle (CAFV) Eligibility")]
        public string Eligibility { get; set; }
        [Column("Electric Range")]
        public long ElectricRange { get; set; }
        [Column("Base MSRP")]
        public long BaseMSRP { get; set; }
        [Column("Legislative District")]
        public long LegislativeDistrict { get; set; }
        [Column("DOL Vehicle ID")]
        public long DOLVehicleID { get; set; }
        [Column("Vehicle Location")]
        public string VehicleLocation { get; set; }
        [Column("Electric Utility")]
        public string ElectricUtility { get; set; }
        [Column("2020 Census Tract")]
        public string CensusTract2020 { get; set; }
    }
}
