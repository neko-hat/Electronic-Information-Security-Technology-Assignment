using System;
using System.Collections.Generic;

namespace back_end.Data.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string? StreetNumber { get; set; }

    public string? StreetName { get; set; }

    public string? City { get; set; }

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }
}
