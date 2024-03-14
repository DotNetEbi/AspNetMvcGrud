using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMvcGrud.Domain;

public class Employee
{
    [Key]
    public int ID { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(100)")]
	public string Name { get; set; }

	[Required]
    public Gender Gender { get; set; }

    [Required]
	[Column(TypeName = "nvarchar(100)")]
	public string Address { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(100)")]
	[DisplayName("Contact number")]
	public string ContactNumber { get; set; }

	[Required]
	[Column(TypeName = "date")]
	[DisplayName("Date of birth")]
	public DateOnly DateOfBirth { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(100)")]
	public string Education { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(100)")]
	public string Occupation { get; set; }

	[Required]
	[Column(TypeName = "date")]
	[DisplayName("Joined Date")]
	public DateOnly JoinedDate { get; set; }

	[Required]
	[Column(TypeName = "date")]
	[DisplayName("Relieved Date")]
	public DateOnly RelievedDate { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(500)")]
	public string Description { get; set; }

	[Required]
	public CurrentStatus Status { get; set; }
}
