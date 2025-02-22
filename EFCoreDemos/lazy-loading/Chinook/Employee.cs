﻿using System;
using System.Collections.Generic;

namespace lazy_loading.Chinook;

public class Employee : BaseEntity
{
    public Employee()
    {
        Customers = new HashSet<Customer>();
        InverseReportsToNavigation = new HashSet<Employee>();
    }

    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Title { get; set; }
    public int ReportsTo { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }

    public virtual Employee? ReportsToNavigation { get; set; }

    public virtual ICollection<Customer> Customers { get; set; }

    public virtual ICollection<Employee> InverseReportsToNavigation { get; set; }
}