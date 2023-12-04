using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TestPrepation.Data.Models;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace TestPrepation.Data.ViewModels;

public partial class ClientViewModel
{
    public int ClientId { get; set; }

    [MaxLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;
    [MaxLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;
    [Display(Name = "Publishing Date")]
    //  [AssertThat("PublishingDate <=Today()", ErrorMessage = Errors.NotAllowFutureDate)]
    [AssertThat("DateOfBirth<=Today()")]
    public DateTime DateOfBirth { get; set; }

    public int MaritalStatusId { get; set; }

    public IEnumerable<SelectListItem>? SelectStatus { get; set; }
    public string StatusName { get; set; }


    public int MobileNumber { get; set; }

    public string Email { get; set; } = null!;

    public string? ImagePath { get; set; } = null!;

    public IFormFile? Image { get; set; }


}
