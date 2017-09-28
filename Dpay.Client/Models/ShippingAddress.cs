using System;

namespace Dpay.Client.Models
{
    public class ShippingAddress
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
        public string DistrictCode { get; set; }
        public string ShippingCompany { get; set; }

    }
}