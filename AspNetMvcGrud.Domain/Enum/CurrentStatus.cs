using System.ComponentModel;

namespace AspNetMvcGrud.Domain;

public enum CurrentStatus
{
	[Description("")]
	None,
	[Description("Active")]
	Active,
	[Description("Inactive")]
	Inactive
}
