using System.ComponentModel;

namespace AspNetMvcGrud.Domain;

public enum Gender
{
	[Description("")]
	None,
	[Description("Male")]
	Male,
	[Description("Female")]
	Female
}
