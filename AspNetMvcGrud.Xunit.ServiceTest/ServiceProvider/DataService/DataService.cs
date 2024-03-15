using AspNetMvcGrud.DataAccessLayer;
using AspNetMvcGrud.Interface;
using AspNetMvcGrud.Repository.Data;
using AspNetMvcGrud.Service.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcGrud.XunitTest.SerrviceProvider;

public class DataService
{
	private const string _connectionString = "Server = (LocalDB)\\V2016; Database = AspMvcGrudDb;Trusted_Connection=True;TrustServerCertificate=True";

	private static DbContextOptions<ApplicationDbContext> _options = new DbContextOptionsBuilder<ApplicationDbContext>()
		  .UseSqlServer(_connectionString)
		  .Options;

	public static IEmployeeService GetEmployeeService()
	{
		return new EmployeeService(new EmployeeRepository(new ApplicationDbContext(_options)));
	}
}
