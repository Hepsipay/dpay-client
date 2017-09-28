using System;

namespace Dpay.Client.Models
{
    public class InvoiceAddress
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string ZipCode { get; set; }
    }
}