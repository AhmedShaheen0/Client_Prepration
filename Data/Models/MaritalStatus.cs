using System;
using System.Collections.Generic;

namespace TestPrepation.Data.Models;

public partial class MaritalStatus
{
    public int MaritalStatusId { get; set; }

    public string MaritalStatusName { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; } = new List<Client>();
}
