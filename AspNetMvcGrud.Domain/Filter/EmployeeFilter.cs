namespace AspNetMvcGrud.Domain.Filter;

public class EmployeeFilter
{
	public string Search { get; set; }
	public string ContactNumber { get; set; }
	public string Name { get; set; }
    public Gender Gender { get; set; }
    public DateOnly FromDateOfBirth { get; set; }
	public DateOnly ToDateOfBirth { get; set; }
	public DateOnly FromJoinedDate { get; set; }
	public DateOnly ToJoinedDate { get; set; }
	public DateOnly FromReleivedDate { get; set; }
	public DateOnly ToReleivedDate { get; set; }
	public CurrentStatus Status { get; set; } = CurrentStatus.Active;
}
