using AspNetMvcGrud.DataAccessLayer;
using AspNetMvcGrud.Domain;
using AspNetMvcGrud.Domain.Filter;
using AspNetMvcGrud.Interface;
using AspNetMvcGrud.Repository.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AspNetMvcGrud.Repository.Data;

public class EmployeeRepository : IEmployeeRepository
{
	private readonly ApplicationDbContext _database;

	public EmployeeRepository(ApplicationDbContext database)
	{
		_database = database;
	}
	public async Task<Employee> SaveAsync(Employee employee)
	{
		try
		{
			_database.Employee.Add(employee);

			await _database.SaveChangesAsync();

			return employee;
		}
		catch (SqlException ex)
		{
			throw new DataSourceException("Error Saving Employee Details", ex);
		}
	}

	public async Task UpdateAsync(Employee employee)
	{
		try
		{
			_database.Employee.Update(employee);

			await _database.SaveChangesAsync();

		}
		catch (SqlException ex)
		{
			throw new DataSourceException("Error Updating Employee Details", ex);
		}
	}

	public async Task DeleteAsync(int id)
	{
		try
		{
			var employee = await _database.Employee.FindAsync(id);

			_database.Employee.Remove(employee);

			await _database.SaveChangesAsync();

		}
		catch (SqlException ex)
		{
			throw new DataSourceException("Error Deleting Employee", ex);
		}
	}

	public async Task UpdatePartiallyAsync(object employee)
	{
		var updatableEmployee = new Employee();

		try
		{
			Type myType = employee.GetType();

			IList<PropertyInfo> propertyList = new List<PropertyInfo>(myType.GetProperties());

			foreach (PropertyInfo property in propertyList)
			{
				string propertyName = property.Name;

				if (property == null)
					return;

				object propertyValue = property.GetValue(employee, null);

				if (propertyValue == null)
					return;

				var entry = _database.Entry(updatableEmployee);

				if (propertyName == nameof(Employee.ID) && Convert.ToInt32(propertyValue) != 0)
				{
					updatableEmployee.ID = Convert.ToInt32(propertyValue);

				}

				if (propertyName == nameof(Employee.Status) && (CurrentStatus)propertyValue != CurrentStatus.None)
				{
					updatableEmployee.Status = (CurrentStatus)propertyValue;
					entry.Property(employee => employee.Status).IsModified = true;
				}
			}

			await _database.SaveChangesAsync();

		}
		catch (SqlException ex)
		{
			throw new DataSourceException("Error In Partial Update Of Employee Details", ex);
		}
	}

	public async Task<Employee> FindByIDAsync(int id)
	{
		try
		{
			var employee = await _database.Employee.FindAsync(id);

			if (employee == null)
				throw new DataSourceException($"Error Finding Employee Details - ID ({id}) Not Found");

			return employee;

		}
		catch (SqlException ex)
		{
			throw new DataSourceException("Error Finding Employee Details", ex);
		}
	}

	public async Task<IEnumerable<Employee>> GetListByAsync(EmployeeFilter filter)
	{
		try
		{
			IQueryable<Employee> queryableEmployeeList;

			var employeeList = _database.Employee;

			queryableEmployeeList = employeeList.Where(item => 1 == 1);

			if (filter.Name != null && filter.Name != string.Empty)
				queryableEmployeeList = queryableEmployeeList.Where(item => item.Name == filter.Name);

			if (filter.ContactNumber != null && filter.ContactNumber != string.Empty)
				queryableEmployeeList = queryableEmployeeList.Where(item => item.ContactNumber == filter.ContactNumber);

			if (filter.Status != default)
				queryableEmployeeList = queryableEmployeeList.Where(item => item.Status == filter.Status);

			if(filter.Gender != default)
				queryableEmployeeList = queryableEmployeeList.Where(item => item.Gender == filter.Gender);

			if(filter.FromDateOfBirth != default)
				queryableEmployeeList = queryableEmployeeList
					.Where(item => 
							item.DateOfBirth.Month >= filter.FromDateOfBirth.Month 
							&& item.DateOfBirth.Day >= filter.FromDateOfBirth.Day);

			if (filter.ToDateOfBirth != default)
				queryableEmployeeList = queryableEmployeeList
					.Where(item => item.DateOfBirth.Month <= filter.FromDateOfBirth.Month
							&& item.DateOfBirth.Day <= filter.FromDateOfBirth.Day);

			if (filter.FromJoinedDate != default)
				queryableEmployeeList = queryableEmployeeList.Where(item => item.JoinedDate >= filter.FromJoinedDate);

			if (filter.ToJoinedDate != default)
				queryableEmployeeList = queryableEmployeeList.Where(item => item.JoinedDate <= filter.ToJoinedDate);

			if (filter.FromReleivedDate != default)
				queryableEmployeeList = queryableEmployeeList.Where(item => item.RelievedDate >= filter.FromReleivedDate);

			if (filter.ToReleivedDate != default)
				queryableEmployeeList = queryableEmployeeList.Where(item => item.RelievedDate <= filter.ToReleivedDate);

			if (!string.IsNullOrWhiteSpace(filter.Search))
				queryableEmployeeList = queryableEmployeeList.Where(item => item.Name.Contains(filter.Search) 
				|| item.ContactNumber.Contains(filter.Search));

			return await queryableEmployeeList.ToListAsync();

		}
		catch (SqlException ex)
		{
			throw new DataSourceException("Error Getting Employee Details", ex);
		}
	}

	
}
