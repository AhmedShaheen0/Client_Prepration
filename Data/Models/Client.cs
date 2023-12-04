using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestPrepation.Data.Models;

public partial class Client
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public int MaritalStatusId { get; set; }

    public string MobileNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ImagePath { get; set; }

    public virtual MaritalStatus MaritalStatus { get; set; } = null!;
}
