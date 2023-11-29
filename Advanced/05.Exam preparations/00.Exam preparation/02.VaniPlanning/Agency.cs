namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Agency : IAgency
    {
        private Dictionary<string, Invoice> invoicesByNumber = new Dictionary<string, Invoice>();

        private OrderedDictionary<DateTime, HashSet<Invoice>> invoicesByEndDate = new OrderedDictionary<DateTime, HashSet<Invoice>>();

        public void Create(Invoice invoice)
        {
            if(Contains(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }

            invoicesByNumber.Add(invoice.SerialNumber, invoice);

            if (!invoicesByEndDate.ContainsKey(invoice.DueDate))
            {
                invoicesByEndDate.Add(invoice.DueDate, new HashSet<Invoice>());
            }

            invoicesByEndDate[invoice.DueDate].Add(invoice);
        }

        public void ThrowInvoice(string number)
        {
            if (!Contains(number))
            {
                throw new ArgumentException();
            }
            var invoice = invoicesByNumber[number];
            invoicesByEndDate[invoice.DueDate].Remove(invoice);
            invoicesByNumber.Remove(number);
        }

        public void ThrowPayed()
        {
            var paid = invoicesByNumber.Values.Where(i => i.Subtotal == 0);

            foreach ( var invoice in paid )
            {
                invoicesByNumber.Remove(invoice.SerialNumber);
                invoicesByEndDate[invoice.DueDate].Remove(invoice);
            }
        }

        public int Count() => invoicesByNumber.Count;

        public bool Contains(string number) => invoicesByNumber.ContainsKey(number);

        public void PayInvoice(DateTime due)
        {
            if (!invoicesByEndDate.ContainsKey(due) || invoicesByEndDate[due].Count == 0)
            {
                throw new ArgumentException();
            }
            var invoices = invoicesByEndDate[due];

            foreach (var invoice in invoices)
            {
                invoice.Subtotal = 0;
            }
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
            => invoicesByNumber.Values
            .Where(i => i.IssueDate >= start && i.IssueDate <= end)
            .OrderBy(i => i.IssueDate)
            .ThenBy(i => i.DueDate);

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            var result = invoicesByNumber.Values
                .Where(i => i.SerialNumber.Contains(serialNumber))
                .OrderByDescending(i => i.SerialNumber);

            return result.Count() == 0 
                ? throw new ArgumentException() 
                : result;
        }
            
        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var toBeRemoved = invoicesByEndDate
                .Range(start, false, end, false).Values
                .SelectMany(i => i)
                .ToArray();

            if (toBeRemoved.Length == 0)
            {
                throw new ArgumentException();
            }

            foreach (var invoice in toBeRemoved)
            {
                invoicesByNumber.Remove(invoice.SerialNumber);
                invoicesByEndDate[invoice.DueDate].Remove(invoice);
            }

            return toBeRemoved;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
            => invoicesByNumber.Values
            .Where(i => i.Department == department)
            .OrderByDescending (i => i.Subtotal)
            .ThenBy(i => i.IssueDate);

        public IEnumerable<Invoice> GetAllByCompany(string company)
            => invoicesByNumber.Values
            .Where(i => i.CompanyName == company)
            .OrderByDescending(i => i.SerialNumber);

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            if (!invoicesByEndDate.ContainsKey(dueDate) || invoicesByEndDate[dueDate].Count == 0)
            {
                throw new ArgumentException();
            }

            var invoices = invoicesByEndDate[dueDate];
            invoicesByEndDate.Remove(dueDate);

            var newDate = dueDate.AddDays(days);

            if (!invoicesByEndDate.ContainsKey(newDate))
            {
                invoicesByEndDate.Add(newDate, new HashSet<Invoice>());
            }

            foreach (var invoice in invoices)
            {
               invoice.DueDate = newDate;

                invoicesByEndDate[newDate].Add(invoice);
            }

        }
    }
}
