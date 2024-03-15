using AspNetMvcGrud.Domain;
using AspNetMvcGrud.Domain.Filter;
using AspNetMvcGrud.Interface;
using AspNetMvcGrud.XunitTest.SerrviceProvider;
using Xunit;
using Xunit.Abstractions;

namespace AspNetMvcGrud.XunitTest.Data.Service;

public class EmployeeServiceTest
{
	private readonly ITestOutputHelper _outputHelper;

	public EmployeeServiceTest(ITestOutputHelper outputHelper)
	{
		_outputHelper = outputHelper;
	}

	public virtual IEmployeeService GetService()
	{
		return DataService.GetEmployeeService();
	}

	[Fact]
	public virtual async Task SaveTestAsync()
	{
		var service = GetService();

		var state = await service.SaveAsync(new Employee
		{
			Name = "Test",
			Address = "Test",
			ContactNumber = "Test",
			Education = "Test",
			DateOfBirth = new DateOnly(1900, 01, 01),
			Description = "Save Test",
			Gender = Gender.Female,
			Occupation = "Test",
			JoinedDate = DateOnly.FromDateTime(DateTime.Now),
			Status = CurrentStatus.Active,
		});

		_outputHelper.WriteLine($"Emplyee ID : {state.ID} ");
	}

	[Fact]
	public virtual async Task UpdateTestAsync()
	{
		var service = GetService();

		await service.UpdateAsync(new Employee
		{
			ID = 2,
			Name = "Test2",
			Address = "Test",
			ContactNumber = "Test",
			Education = "Test",
			DateOfBirth = new DateOnly(2000, 01, 01),
			Description = "Update test from xunit test",
			Gender = Gender.Female,
			Occupation = "Test",
			JoinedDate = DateOnly.FromDateTime(DateTime.Now),
			Status = CurrentStatus.Active,
		});

		_outputHelper.WriteLine("Employee Model Updated Successfully");
	}

	[Fact]
	public virtual async Task UpdatePartiallyTestAsync()
	{
		var service = GetService();

		await service.UpdateStatusAsync(1, CurrentStatus.Active);

		_outputHelper.WriteLine("Status Of Employee Model Updated Successfully");
	}

	[Fact]
	public virtual async Task FindByIDTestAsync()
	{
		var service = GetService();

		var result = await service.FindByIDAsync(2);

		if (result == null)
		{
			_outputHelper.WriteLine($"{"No Data Found"}");
			return;
		}

		//Assert.NotNull(result);

		_outputHelper.WriteLine($"{"Found Employee Details \nEmployee Name Is"} {result.Name}");
	}

	[Fact]
	public virtual async Task GetListTestAsync()
	{
		var service = GetService();

		var resultList = await service.GetListAsync();

		if (resultList.Count() == default)
		{
			_outputHelper.WriteLine($"{"No Data Found"}");
			return;
		}

		//Assert.NotNull(result);

		_outputHelper.WriteLine($"{"Found Employee Details - total Employees = "} {resultList.Count()}");
	}

	[Fact]
	public virtual async Task GetInactiveListTestAsync()
	{
		var service = GetService();

		var resultList = await service.GetListByAsync(new EmployeeFilter
		{
			Status = CurrentStatus.Inactive,
		});

		if (resultList.Count() == default)
		{
			_outputHelper.WriteLine($"{"No Data Found"}");
			return;
		}

		//Assert.NotNull(result);

		_outputHelper.WriteLine($"{"Found Employee Details - total Employees = "} {resultList.Count()}");
	}


	[Fact]
	public virtual async Task GetActiveListTestAsync()
	{
		var service = GetService();

		var resultList = await service.GetListByAsync(new EmployeeFilter
		{
			Status = CurrentStatus.Active,
		});

		if (resultList.Count() == default)
		{
			_outputHelper.WriteLine($"{"No Data Found"}");
			return;
		}

		//Assert.NotNull(result);

		_outputHelper.WriteLine($"{"Found Employee Details - total Employees = "} {resultList.Count()}");
	}

	[Fact]
	public virtual async Task GetMaleEmployeeListTestAsync()
	{
		var service = GetService();

		var resultList = await service.GetListByAsync(new EmployeeFilter
		{
			Gender = Gender.Male,
		});

		if (resultList.Any(item => item.Gender == Gender.Female))
		{
			_outputHelper.WriteLine($"{"Wrong result! Revise Logic"}");
			return;
		}

		if (resultList.Count() == default)
		{
			_outputHelper.WriteLine($"{"No Data Found"}");
			return;
		}

		//Assert.NotNull(result);

		_outputHelper.WriteLine($"{"Found Employee Details - total Employees = "} {resultList.Count()}");
	}

	[Fact]
	public virtual async Task GetFemaleEmployeeListTestAsync()
	{
		var service = GetService();

		var resultList = await service.GetListByAsync(new EmployeeFilter
		{
			Gender = Gender.Female,
		});

		if (resultList.Count() == default)
		{
			_outputHelper.WriteLine($"{"No Data Found"}");
			return;
		}

		if(resultList.Any(item => item.Gender == Gender.Male))
		{
			_outputHelper.WriteLine($"{"Wrong result! Revise Logic"}");
			return;
		}

		//Assert.NotNull(result);

		_outputHelper.WriteLine($"{"Found Employee Details - total Employees = "} {resultList.Count()}");
	}

	[Fact]
	public virtual async Task GetListByBirthDateTestAsync()
	{
		var service = GetService();

		var resultList = await service.GetListByAsync(new EmployeeFilter
		{
			FromDateOfBirth = new DateOnly(2024, 01, 01),
			ToDateOfBirth = new DateOnly(2024, 03, 15)
		});

		if (resultList.Count() == default)
		{
			_outputHelper.WriteLine($"{"No Data Found"}");
			return;
		}

		//Assert.NotNull(result);

		_outputHelper.WriteLine($"{"Found Employee Details - total Employees = "} {resultList.Count()}");
	}

	[Fact]
	public virtual async Task GetListByJoinedDateTestAsync()
	{
		var service = GetService();

		var resultList = await service.GetListByAsync(new EmployeeFilter
		{
			FromJoinedDate = new DateOnly(2024, 03, 15),
			ToJoinedDate = new DateOnly(2024, 03, 15)
		});

		if (resultList.Count() == default)
		{
			_outputHelper.WriteLine($"{"No Data Found"}");
			return;
		}

		//Assert.NotNull(result);

		_outputHelper.WriteLine($"{"Found Employee Details - total Employees = "} {resultList.Count()}");
	}

	[Fact]
	public virtual async Task DeleteTestAsync()
	{
		var service = GetService();

		await service.DeleteAsync(2);

		var deletedEmployee = await service.FindByIDAsync(2);

		if(deletedEmployee != null)
		{
			_outputHelper.WriteLine("Employee details not deleted");
		}
		else
		{
			_outputHelper.WriteLine("Employee Record Deleted Successfully");
		}

		//Assert.NotNull(result);
	}
}
