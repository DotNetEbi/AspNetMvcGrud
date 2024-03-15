using AspNetMvcGrud.Domain;
using FluentValidation;

namespace AspNetMvcGrud.Validator;

public class EmployeeValidator :AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(employee => employee.Name)
            .NotEmpty()
            .WithMessage("Name Required");

		RuleFor(employee => employee.Gender)
			.NotEmpty()
			.WithMessage("Gender Required");

		RuleFor(employee => employee.Address)
			.NotEmpty()
			.WithMessage("Address Required");

		RuleFor(employee => employee.ContactNumber)
			.NotEmpty()
			.WithMessage("Contact Number Required")
			.DependentRules(() =>
			{
				RuleFor(employee => employee.ContactNumber.Length)
					.GreaterThanOrEqualTo(10)
					.OverridePropertyName(nameof(Employee.ContactNumber))
					.WithMessage("Contact Number Invalid");
			});

		RuleFor(employee => employee.DateOfBirth)
			.NotEmpty()
			.WithMessage("Date Of Birth Required");

		RuleFor(employee => employee.Education)
			.NotEmpty()
			.WithMessage("Education Required");

		RuleFor(employee => employee.JoinedDate)
			.NotEmpty()
			.WithMessage("Joined Date Required");
	}
}
