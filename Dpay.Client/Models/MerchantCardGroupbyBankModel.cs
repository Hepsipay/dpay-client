
namespace Dpay.Client.Models
{
    public class MerchantCardGroupbyBankModel
    {
        public Bank Bank { get; set; }
        public int Count { get; set; }
    }

    public enum Bank
    {
        None = 0,
        Ziraat = 10,
        Halkbank = 12,
        Vakifbank = 15,
        Teb = 32,
        Akbank = 46,
        Sekerbank = 59,
        Garanti = 62,
        Isbank = 64,
        Ykb = 67,
        Citibank = 92,
        Turkishbank = 96,
        Ingbank = 99,
        Fibabank = 103,
        Turkland = 108,
        Tekstilbank = 109,
        Finansbank = 111,
        Hsbc = 123,
        Abank = 124,
        Burganbank = 125,
        Denizbank = 134,
        Anadolubank = 135,
        Aktifbank = 143,
        Odeabank = 146,
        Albarakaturk = 203,
        Kuveytturk = 205,
        Turkiyefinans = 206,
        BankAsya = 208,
        ZiraatKatilim = 209,
        VakifKatilim = 210,
        AkkTurizm = 900
    }
}
