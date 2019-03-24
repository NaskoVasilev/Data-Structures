using System;
using System.Collections.Generic;
using System.Linq;

public class Agency : IAgency
{
    private Dictionary<string, Invoice> bySerialNumber;
    private List<Invoice> payedInvoices;

    public Agency()
    {
        this.bySerialNumber = new Dictionary<string, Invoice>();
        this.payedInvoices = new List<Invoice>();
    }

    public bool Contains(string number)
    {
        return this.bySerialNumber.ContainsKey(number);
    }

    public int Count()
    {
        return this.bySerialNumber.Count;
    }

    public void Create(Invoice invoice)
    {
        if (this.bySerialNumber.ContainsKey(invoice.SerialNumber))
        {
            throw new ArgumentException();
        }

        this.bySerialNumber.Add(invoice.SerialNumber, invoice);
    }

    public void ExtendDeadline(DateTime dueDate, int days)
    {
        int extendedInvoicesCount = 0;

        foreach (var invoice in this.bySerialNumber.Values)
        {
            if(invoice.DueDate == dueDate)
            {
                extendedInvoicesCount++;
                invoice.DueDate = invoice.DueDate.AddDays(days);
            }
        }

        if(extendedInvoicesCount == 0)
        {
            throw new ArgumentException();
        }
    }

    public IEnumerable<Invoice> GetAllByCompany(string company)
    {
        return this.bySerialNumber.Values
            .Where(i => i.CompanyName == company)
            .OrderByDescending(i => i.SerialNumber)
            .ToList();
    }

    public IEnumerable<Invoice> GetAllFromDepartment(Department department)
    {
        return this.bySerialNumber.Values
            .Where(i => i.Department == department)
            .OrderByDescending(i => i.Subtotal)
            .ThenBy(i => i.IssueDate)
            .ToList();
    }

    public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
    {
        return this.bySerialNumber.Values
            .Where(i => i.IssueDate >= start && i.IssueDate <= end)
            .OrderBy(i => i.IssueDate)
            .ThenBy(i => i.DueDate)
            .ToList();
    }

    public void PayInvoice(DateTime due)
    {
        bool match = false;

        foreach (var invoice in this.bySerialNumber.Values)
        {
            if (invoice.DueDate == due)
            {
                match = true;
                invoice.Subtotal = 0;
                this.payedInvoices.Add(invoice);
            }
        }

        if (!match)
        {
            throw new ArgumentException();
        }
    }

    public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
    {
        var result = this.bySerialNumber.Values
            .Where(i => i.SerialNumber.Contains(serialNumber))
            .OrderByDescending(i => i.SerialNumber)
            .ToList();

        if (!result.Any())
        {
            throw new ArgumentException();
        }

        return result;
    }

    public void ThrowInvoice(string number)
    {
        if (!this.bySerialNumber.ContainsKey(number))
        {
            throw new ArgumentException();
        }

        this.bySerialNumber.Remove(number);
    }

    public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
    {
        var invoices = new List<Invoice>();
        
        foreach (var invoice in this.bySerialNumber.Values.ToList())
        {
            if(invoice.DueDate > start && invoice.DueDate < end)
            {
                invoices.Add(invoice);
                this.bySerialNumber.Remove(invoice.SerialNumber);
            }
        }

        if (!invoices.Any())
        {
            throw new ArgumentException();
        }

        return invoices;
    }

    public void ThrowPayed()
    {
        foreach (var payed in payedInvoices)
        {
            this.bySerialNumber.Remove(payed.SerialNumber);
        }

        this.payedInvoices.Clear();
    }
}
