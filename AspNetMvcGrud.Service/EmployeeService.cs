using AspNetMvcGrud.Domain;
using AspNetMvcGrud.Domain.Filter;
using AspNetMvcGrud.Interface;

namespace AspNetMvcGrud.Service.Data;

public class EmployeeService : IEmployeeService
{
	private readonly IEmployeeRepository _repository;
	public EmployeeService(IEmployeeRepository repository)
	{
		_repository = repository;
	}
	public async Task<Employee> SaveAsync(Employee employee)
	{
		return await _repository.SaveAsync(employee);
	}

	public async Task UpdateAsync(Employee employee)
	{
		await _repository.UpdateAsync(employee);
	}

	public async Task UpdateStatusAsync(int id, CurrentStatus status)
	{
		await _repository.UpdatePartiallyAsync(new
		{
			ID = id,
			Status = status
		});
	}

	public async Task DeleteAsync(int id)
	{
		await _repository.DeleteAsync(id);
	}
	public async Task<Employee> FindByIDAsync(int id)
	{
		return await _repository.FindByIDAsync(id);
	}

	public async Task<List<Employee>> GetListAsync()
	{
		return await GetListByAsync(new EmployeeFilter());
	}

	public async Task<List<Employee>> GetListByAsync(EmployeeFilter filter)
	{
		return (await _repository.GetListByAsync(filter)).ToList();
	}
}