using System;

namespace Dpay.Client.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IpAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
        public string Tckn { get; set; }
        public string VatNumber { get; set; }
        public int MemberSince { get; set; }
        public int Birthdate { get; set; }

    }
}