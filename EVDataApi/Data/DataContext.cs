using EVDataApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EVDataApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<EVDataModel> Electric_Vehicle_Population_Data { get; set; }

    }
}
