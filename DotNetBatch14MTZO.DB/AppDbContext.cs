using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14MTZO.DB;

public class AppDbContext : DbContext
{
    private readonly SqlConnectionStringBuilder _sqlConnection = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "MiniATM",
        UserID = "sa",
        Password = "mtzoo@123",
        TrustServerCertificate = true

    };

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_sqlConnection.ConnectionString);
        }
    }

    public DbSet<UserAcountModel> UserAcounts { get; set; }
    public object UserAccounts { get; set; }
}
