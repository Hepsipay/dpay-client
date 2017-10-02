# dpay-dotnet

[![Build Status](https://ci.appveyor.com/api/projects/status/github/Hepsipay/dpay-client?branch=master&svg=true)](https://ci.appveyor.com/api/projects/status/github/Hepsipay/dpay-client?branch=master&svg=true)

# Kullanım

### 3D’siz Satış İşlemi

#### Request

```c#
var paymentRequest = new PaymentRequest
{
	Version = "1.0",
	ApiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJyb2xlIjoxLCJBcGlLZXkiOiJhNTNkZjYwZjUwNmY0MzBjYTRjNzQzNTRkNDU1Y2RlYyJ9.O-eJWMK1sHt3JZgHFsnJp6yyiqmbzdyuOOe0-AvnHzg",
	TransactionId = "TestSale00002",
	TransactionTime = "1443600845",
	Amount = 5499,
	Description = "E-ticaretÖdemesi",
	Currency = "TRY",
	Installment = 1,
	Card = new Card
	{
		CardHolderName = "Ahmet Mehmet",
		CardNumber = "4355084355084358",
		ExpireMonth = "02",
		ExpireYear = "20",
		SecurityCode = "000"
	},
	BasketItems = new List
	{
		new BasketItem
		{
			Description = "BoyamaKalemSeti",
			ProductCode = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41",
			Amount = 8750,
			VatRatio = 18,
			Count = 1,
			Url = "http://www.ahmetmarket.com.tr/boyama-kalem-seti"
		},
		new BasketItem
		{
			Description = "BoyamaKitabı",
			ProductCode = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41",
			Amount = 2550,
			VatRatio = 18,
			Count = 3,
			Url = "http://www.ahmetmarket.com.tr/boyama-kitabi"
		},
		new BasketItem
		{
			Description = "KargoBedeli",
			Amount = 1000,
			VatRatio = 18,
			Count = 1
		}
	},
	Customer = new Customer
	{
		Name = "Ahmet",
		Surname = "Mehmet",
		Email = "ahmetmehmet@ahmetmarket.com.tr",
		PhoneNumber = "5337654321",
		Code = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41",
		IpAddress = "127.0.0.1"
	},
	ShippingAddress = new ShippingAddress
	{
		Name = "Ahmet Mehmet",
		Address = "Kuştepe Mahallesi Mecidiyeköy Yolu Cad. No:12 Trump Towers Kule:2 Kat:11 ŞİŞLİ",
		Country = "Türkiye",
		CountryCode = "TUR",
		City = "İstanbul",
		CityCode = "34",
		ZipCode = "34580"
	},
	InvoiceAddress = new InvoiceAddress
	{
		Name = "Ahmet Mehmet",
		Address = "Kuştepe Mahallesi Mecidiyeköy Yolu Cad. No:12 Trump Towers Kule:2 Kat:11 ŞİŞLİ",
		Country = "Türkiye",
		CountryCode = "TUR",
		City = "İstanbul",
		CityCode = "34",
		ZipCode = "34580"
	},
	Extras = new List { new Extra { Key = "INT_SPRS_KODU", Value = "spr_123456789" } },
	MerchantCardId = "",
	SaveCreditCard = true,
	HashVersion = "1.1"
};
var apiUrl = "https://apientgr.hepsipay.com/payments/pay";
var paymentResponse = _paymentProcessor.Pay(paymentRequest, apiUrl, "123");
```

#### Response
```c#
{ 
  "Amount": 12300,
  "TransactionType": 0, 
  "Installment": 9, 
  "ApiKey": "1ca71cb09c7f4a2188fbddfa90efb481",
  "TransactionId": "tx_121345678912345", 
  "TransactionTime": "1447752023",
  "Signature": "8480954d54d94e5f272c53caa69efdcb0b678e837d3997eec42c4dbfa636cdde",
  "Currency": "TRY", 
  "Extras": [ { "Key": "INT_SPRS_KODU", "Value": "spr_123456789" } ],
  "Success": true, 
  "MessageCode": "0000", 
  "Message": "Başarılı", 
  "UserMessage": "İşleminiz başarıyla gerçekleştirildi."
  }
```

### 3D'li Satış İşlemi

#### Request

```c#

public ActionResult Sale()
{
	var createThreedRequest = new CreateThreedRequest
	{
		ApiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJyb2xlIjoxLCJBcGlLZXkiOiJhNTNkZjYwZjUwNmY0MzBjYTRjNzQzNTRkNDU1Y2RlYyJ9.O-eJWMK1sHt3JZgHFsnJp6yyiqmbzdyuOOe0-AvnHzg",
		TransactionId = "TestThreedPayment00001",
		TransactionTime = "1443600845",
		Amount = 5499,
		Description = "E-ticaretÖdemesi",
		Currency = "TRY",
		Installment = 1,
		Card = new Card
		{
			CardHolderName = "Ahmet Mehmet",
			CardNumber = "4355084355084358",
			ExpireMonth = "02",
			ExpireYear = "20",
			SecurityCode = "000"
		},
		BasketItems = new List
		{
			new BasketItem
			{
				Description = "BoyamaKalemSeti",
				ProductCode = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41",
				Amount = 8750,
				VatRatio = 18,
				Count = 1,
				Url = "http://www.ahmetmarket.com.tr/boyama-kalem-seti"
			},
			new BasketItem
			{
				Description = "BoyamaKitabı",
				ProductCode = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41",
				Amount = 2550,
				VatRatio = 18,
				Count = 3,
				Url = "http://www.ahmetmarket.com.tr/boyama-kitabi"
			},
			new BasketItem
			{
				Description = "KargoBedeli",
				Amount = 1000,
				VatRatio = 18,
				Count = 1
			}
		},
		Customer = new Customer
		{
			Name = "Ahmet",
			Surname = "Mehmet",
			Email = "ahmetmehmet@ahmetmarket.com.tr",
			PhoneNumber = "5337654321",
			Code = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41"
		},
		ShippingAddress = new ShippingAddress
		{
			Name = "Ahmet Mehmet",
			Address = "Kuştepe Mahallesi Mecidiyeköy Yolu Cad. No:12 Trump Towers Kule:2 Kat:11 ŞİŞLİ",
			Country = "Türkiye",
			CountryCode = "TUR",
			City = "İstanbul",
			CityCode = "34",
			ZipCode = "34580"
		},
		InvoiceAddress = new InvoiceAddress
		{
			Name = "Ahmet Mehmet",
			Address = "Kuştepe Mahallesi Mecidiyeköy Yolu Cad. No:12 Trump Towers Kule:2 Kat:11 ŞİŞLİ",
			Country = "Türkiye",
			CountryCode = "TUR",
			City = "İstanbul",
			CityCode = "34",
			ZipCode = "34580"
		},
		Extras = new List { new Extra { Key = "INT_SPRS_KODU", Value = "spr_123456789" } },
		SuccessUrl = _baseUrl + "/ThreedPayment/SuccessfulResult",
		FailUrl = _baseUrl + "/ThreedPayment/FailedResult",
		Priority = 1,
		VisitorId = "12312312",
		UserKey = "adasdas2222",
		DiscountAmount = 4010,
		GiftCheqAmount = 5600,
		HashVersion = "1.1"
	};

	var apiUrl = "https://entgr.hepsipay.com/payment/ThreeDSecureV2";

	var createThreedResponse = _paymentProcessor.CreateThreed(createThreedRequest, apiUrl, "123");

	return Content(createThreedResponse.HtmlForm);
}

```

#### Response
```c#
{ 
  "Amount": 12300,
  "TransactionType": 0, 
  "Installment": 1, 
  "ApiKey": "1ca71cb09c7f4a2188fbddfa90efb481",
  "TransactionId": "tx_121345678912345", 
  "TransactionTime": "1447752023",
  "Signature": "8480954d54d94e5f272c53caa69efdcb0b678e837d3997eec42c4dbfa636cdde",
  "Currency": "TRY", 
  "Extras": [ { "Key": "INT_SPRS_KODU", "Value": "spr_123456789" } ],
  "Success": true, 
  "MessageCode": "0000", 
  "Message": "Başarılı", 
  "UserMessage": "İşleminiz başarıyla gerçekleştirildi."
  }
```

### İade İşlemi

#### Request

```c#

var request = new RefundRequest
{
	Version = "1.0",
	ApiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJyb2xlIjoxLCJBcGlLZXkiOiJhNTNkZjYwZjUwNmY0MzBjYTRjNzQzNTRkNDU1Y2RlYyJ9.O-eJWMK1sHt3JZgHFsnJp6yyiqmbzdyuOOe0-AvnHzg",
	TransactionId = "TestRefund00002",
	TransactionTime = "1443600845",
	Amount = 5499,
	Currency = "TRY",
	BasketItems = new List
	{
		new RefundBasketItem
		{
			BasketItemId = "BoyamaKalemSeti",
			Amount = 8750,
			MerchantOffsetAmount = 18,
			SubMerchantOffsetAmount = 1
		}
	},
	Customer = new Customer
	{
		Name = "Ahmet",
		Surname = "Mehmet",
		Email = "ahmetmehmet@ahmetmarket.com.tr",
		PhoneNumber = "5337654321",
		Code = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41",
		IpAddress = "127.0.0.1"
	},
	ReferenceTransactionId = "TestSale00002",
	HashVersion = "1.1"
};

var apiUrl = "https://apientgr.hepsipay.com/payments/refund";

var response = _paymentProcessor.Refund(request, apiUrl, "123");

```

#### Response
```c#

{
  "ApiKey": "1ca71cb09c7f4a2188fbddfa90efb481",  
  "TransactionId": " tx_123456789",  
	  "ReferenceTransactionId": "tx_12345678",  
  "TransactionTime": "1447752023",  
  "Signature": "8480954d54d94e5f272c53caa69efdcb0b678e837d3997eec42c4dbfa636cdde",  
  "Currency": "TRY", 
	  "amount": 100,  
  "MessageCode": "0000",  
	  "Message": "Başarılı",
	  "UserMessage": "İşleminiz başarıyla gerçekleştirildi"  
	}  

```

### Test Kartları


Kart No          | Banka        | Son Kullanma Tarihi	| CVV		| 3D Secure
-----------      | ----         | -------------------	| ---    	| ---------				
4531444531442283 | Halkbank     | 12/18 				| 001  	 	| Var
5818775818772285 | Halkbank     | 12/18  				| 001	 	| Var
9792004525458548 | Halkbank     | 12/20                 | 001 	 	| Var
5571135571135575 | Akbank       | 12/18                 | 000		| Var
4355084355084358 | Akbank       | 12/18                 | 000       | Var
9792087721232551 | Akbank       | 12/20                 | 000       | Var
375622005485014  | Garanti		| 07/19				    | 123       | Var
4282209004348015 | Garanti		| 07/19				    | 123       | Var
4282209027132016 | Garanti		| 05/18			        | 358       | Yok
4824894728063019 | Garanti		| 06/17				    | 959       | Yok
5289394722895016 | Garanti		| 08/17			        | 820       | Var
5549604734932029 | Garanti		| 03/18		            | 119       | Yok
4508034508034509 | İşbank       | 12/20				    | 000       | Var
5406675406675403 | İşbank       | 12/20				    | 000       | Var
9792042022562362 | İşbank       | 12/20				    | 000       | Var
5400610093155852 | Yapı Kredi	| 02/20				    | 000       | Var
5400617049774124 | Yapı Kredi	| 02/20				    | 000       | Var
5400637003737156 | Yapı Kredi	| 02/20				    | 000       | Var
4506347011448053 | Yapı Kredi	| 02/20				    | 000       | Var
4506347022052795 | Yapı Kredi	| 02/20				    | 000       | Var
4506347031187533 | Yapı Kredi	| 02/20				    | 000       | Var
4506347043358536 | Yapı Kredi	| 02/20				    | 000       | Var
4022774022774026 | FinansBank	| 12/18				    | 000       | Var
5456165456165454 | FinansBank	| 12/18				    | 000       | Var
9792031125125565 | FinansBank	| 12/20				    | 000       | Var

### Cevap Kodları

Cevap Kodu		     | Kullanıcı Mesajı																															| Teknik Mesaj
-------------------  | -----------------								  																						| -------------
0000                 | İşleminiz başarıyla gerçekleşmiştir.				  																						| İşleminiz başarıyla gerçekleşmiştir.
1007		         | Lütfen girdiğiniz kart bilgilerini kontrol edip tekrar deneyiniz.																		| Geçersiz güvenlik kodu.
1009                 | Lütfen girdiğiniz kart bilgilerini kontrol edip tekrar deneyiniz.																		| Kart bilgisi geçersiz.
1021                 | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.																| Sanal Pos Banka tarafında kapalıdır
1028		         | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.																| İadesi Yapılmış İşlem Tekrar İade Edilemez.
1029                 | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.																| Kart Hatası.
1501                 | Kartınızın yetkileri ya da kısıtlamalarından dolayı hata almaktasınız.Kartınızın bankası ile iletişime geçmenizi rica ederiz.			| Kart Hatası.
1503		         | Lütfen girdiğiniz kart bilgilerini kontrol edip tekrar deneyiniz.																		| Kart bilgisi geçersiz.
1504                 | Kartınız online işleme kapalıdır. Kartınız online işlemlere açıldığında tekrar denemenizi rica ederiz.									| Kart Hatası
1505                 | Kartınızın yetkileri ya da kısıtlamalarından dolayı hata almaktasınız.Kartınızın bankası ile iletişime geçmenizi rica ederiz.            | Kart Hatası
1506		         | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.							                                    | Tanım Hatası
1507                 | Kartınızın yetkileri ya da kısıtlamalarından dolayı hata almaktasınız.Kartınızın bankası ile iletişime geçmenizi rica ederiz.   			| Kart Hatası
1508                 | Kartınızın yetkileri ya da kısıtlamalarından dolayı hata almaktasınız.Kartınızın bankası ile iletişime geçmenizi rica ederiz. 	        | Kart Hatası
1517		         | Kartınızın yetkileri ya da kısıtlamalarından dolayı hata almaktasınız.Kartınızın bankası ile iletişime geçmenizi rica ederiz.            | Kart Hatası
1518                 | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Tanım Hatası
1520                 | Kartınız üzerinde bu işlem için yeterli bakiyeniz bulunmamaktadır.                                                                       | Hesap müsait değil.
1521		         | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Tanım Hatası
1523                 | İzin verilen şifre giriş sayısı aşıldı.Kartınızın bankası ile iletişime geçmeniz gerekmektedir.                        	                | Kart Hatası
1525                 | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Geçersiz PIN.
1526		         | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | ARQC hatası.
1530                 | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Kart Hatası
1532                 | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Banka İşlemlere Cevap Vermiyor
1544                 | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Tanım Hatası
1577		         | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Kredi kartı blacklist'te bulunmaktadır.
3025                 | Kartınız taksitli işlemlere kapalı olduğu için işleminiz gerçekleştirilemiyor.Lütfen bankanız ile iletişime geçiniz.                     | Kredi kartı taksitli işleme kapalı.
3032                 | Lütfen girdiğiniz kart bilgilerini kontrol edip tekrar deneyiniz.                                                                        | Kart bilgileri hatalı.
3065		         | Eklenmek istenilen kredi kartı daha önceden eklendiği için işleminizi gerçekleştiremiyoruz. Lütfen yeni bir kart numarası ekleyiniz.     | Kaydedilmek istenilen kart daha önceden kaydedilmiş.
3066                 | Aradığınız kriterlere uygun sonuç bulunamadı.		                                                                                    | Aranan değerlere göre kurumunuza ait kayıtlı kart bulunamamıştır.
1000,1001,1003,1015  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1016,1020,1025,1032  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası 
1032,1041,1043,1510  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1511,1513,1514,1522  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1527,1528,1531,1533  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1534,1535,1536,1537  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1539,1540,1541,1542  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1546,1547,1548,1549  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1550,1551,1553,1554  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1559,1560,1561,1562  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1563,1567,1568,1569  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1571,1574,1575,1576  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1578,1579,1580,1581  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
1582,1583,2000,2011  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
3093,3094,4022,4023  | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası
4024,4027,4032, 4034 | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası 
6000				 | Şu anda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyiniz.                                                             | Genel sistem hatası 

```
