using AspNetMvcGrud.Domain;
using AspNetMvcGrud.Domain.Filter;

namespace AspNetMvcGrud.Interface;

public interface IEmployeeRepository
{
	Task<Employee> SaveAsync(Employee employee);

	Task UpdateAsync(Employee employee);

	Task DeleteAsync(int id);

	Task UpdatePartiallyAsync(object employee);

	Task<Employee> FindByIDAsync(int id);

	Task<IEnumerable<Employee>> GetListByAsync(EmployeeFilter filter);
}
