using AspNetMvcGrud.Domain;
using AspNetMvcGrud.Domain.Filter;

namespace AspNetMvcGrud.Interface;

public interface IEmployeeService
{
	Task<Employee> SaveAsync(Employee employee);

	Task UpdateAsync(Employee employee);

	Task DeleteAsync(int id);

	Task UpdateStatusAsync(int id, CurrentStatus status);

	Task<Employee> FindByIDAsync(int id);

	Task<List<Employee>> GetListByAsync(EmployeeFilter filter);
	Task<List<Employee>> GetListAsync();
}
