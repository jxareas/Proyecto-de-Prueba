using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public List<InvoiceDetail> Detail { get; set; }
        public decimal IVA { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public Invoice()
        {
            Detail = new List<InvoiceDetail>();
        }
    }
}
