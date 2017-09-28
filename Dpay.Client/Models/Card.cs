namespace Dpay.Client.Models
{
    public class Card
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpireYear { get; set; }
        public string ExpireMonth { get; set; }
        public string SecurityCode { get; set; }
    }
}