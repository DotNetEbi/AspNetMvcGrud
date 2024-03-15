using AspNetMvcGrud.Domain;
using AspNetMvcGrud.Interface;
using AspNetMvcGrud.XunitTest.SerrviceProvider;
using Xunit;
using Xunit.Abstractions;
using FluentValidation;
using AspNetMvcGrud.Validator;

namespace AspNetMvcGrud.Xunit.Test.Validator;

public class EmployeeValidationTest
{
	private readonly ITestOutputHelper _outputHelper;

	public EmployeeValidationTest(ITestOutputHelper outputHelper)
	{
		_outputHelper = outputHelper;
	}

	public virtual IEmployeeService GetService()
	{
		return DataService.GetEmployeeService();
	}

	[Fact]
	public async Task EmptyValidationTest()
	{
		var service = GetService();

		var employee = new Employee
		{

		};

		var validation = new EmployeeValidator();

		var result = await validation.ValidateAsync(employee);

		if(result.IsValid)
			 result = await validation.ValidateAsync(employee, options => options.IncludeAllRuleSets());


		if (result.IsValid)
		{
			_outputHelper.WriteLine("No Errors Found");
		}
		else
		{
			result.Errors.ForEach(error =>
			{
				switch(error.PropertyName)
				{
					default:
						_outputHelper.WriteLine($"Error Message : {error.ErrorMessage} - Property Name: ({error.PropertyName})");
						break;
				}
			});
		}
	}
}
