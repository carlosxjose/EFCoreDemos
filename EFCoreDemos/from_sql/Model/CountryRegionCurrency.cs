﻿using System;

namespace from_sql.Model;

public class CountryRegionCurrency
{
    public string CountryRegionCode { get; set; }
    public string CurrencyCode { get; set; }
    public DateTime ModifiedDate { get; set; }

    public virtual CountryRegion CountryRegionCodeNavigation { get; set; }
    public virtual Currency CurrencyCodeNavigation { get; set; }
}