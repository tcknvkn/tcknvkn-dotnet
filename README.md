# TcknVkn

`TcknVkn`, .NET projelerinde **TC Kimlik Numarası (TCKN)** ve **Vergi Kimlik Numarası (VKN)** doğrulaması için geliştirilen hafif bir NuGet kütüphanesidir.

## Öne Çıkan Özellikler
- Tekil ve toplu TCKN doğrulama (`Tckn.Validate`, `Tckn.ValidateMultiple`)
- Tekil ve toplu VKN doğrulama (`Vkn.Validate`, `Vkn.ValidateMultiple`)
- Girdi normalizasyonu: rakam dışı karakterleri otomatik temizleme
- Çoklu hedef framework desteği
- CI/CD ile otomatik build, test, paketleme ve release akışı

## Desteklenen Frameworkler
- `net10.0`
- `net9.0`
- `net8.0`
- `netstandard2.1`
- `netstandard2.0`
- `net48`
- `net462`

## Kurulum

```bash
dotnet add package TcknVkn
```

NuGet Package Manager:

```powershell
Install-Package TcknVkn
```

## Hızlı Kullanım

### TCKN doğrulama

```csharp
using TcknVkn;

ValidationResult result = Tckn.Validate("10000000146");

if (result.Valid)
{
    Console.WriteLine($"Geçerli TCKN: {result.Value}");
}
else
{
    Console.WriteLine(string.Join(" | ", result.Errors));
}
```

### VKN doğrulama

```csharp
using TcknVkn;

ValidationResult result = Vkn.Validate("0850005256");

if (result.Valid)
{
    Console.WriteLine($"Geçerli VKN: {result.Value}");
}
else
{
    Console.WriteLine(string.Join(" | ", result.Errors));
}
```

### Toplu doğrulama

```csharp
using TcknVkn;

string?[] tcknInputs = ["10000000146", "10000000145", null];
string?[] vknInputs = ["0850005256", "0850005257", null];

foreach (ValidationResult item in Tckn.ValidateMultiple(tcknInputs))
{
    Console.WriteLine($"TCKN {item.Value}: {(item.Valid ? "OK" : "FAIL")}");
}

foreach (ValidationResult item in Vkn.ValidateMultiple(vknInputs))
{
    Console.WriteLine($"VKN {item.Value}: {(item.Valid ? "OK" : "FAIL")}");
}
```

## Algoritma Özeti

### TCKN (11 hane)
- Uzunluk 11 hane olmalıdır.
- İlk hane `0` olamaz.
- 10. hane kontrolü uygulanır.
- 11. hane kontrolü uygulanır.
- Tüm haneler aynıysa geçersiz kabul edilir.

### VKN (10 hane)
- Uzunluk 10 hane olmalıdır.
- Son hane kontrol hanesi hesaplanarak doğrulanır.
- Tüm haneler aynıysa geçersiz kabul edilir.

## İlgili Kaynaklar
- tc üret / tc uret: [https://www.tcknvkn.com/tc-uret](https://www.tcknvkn.com/tc-uret)
- tc no üret / tc no uret: [https://www.tcknvkn.com/tc-no-uret](https://www.tcknvkn.com/tc-no-uret)
- tc oluştur / tc üretici: [https://www.tcknvkn.com/tc-uretici](https://www.tcknvkn.com/tc-uretici)
- tckn üret: [https://tcknvkn.com/tckn-uret](https://tcknvkn.com/tckn-uret)
- vergi no üret: [https://www.tcknvkn.com/vergi-no-uret](https://www.tcknvkn.com/vergi-no-uret)
- vergi no oluşturucu: [https://www.tcknvkn.com/vergi-no-uretici](https://www.tcknvkn.com/vergi-no-uretici)
- vkn üret: [https://tcknvkn.com/vkn-uret](https://tcknvkn.com/vkn-uret)

## Yerel Geliştirme

```bash
dotnet restore ./TcknVkn.sln
dotnet build ./TcknVkn.sln -c Release
dotnet test ./tests/TcknVkn.Tests/TcknVkn.Tests.csproj -c Release
dotnet pack ./TcknVkn/TcknVkn.csproj -c Release -o ./artifacts
```

## Bağlantılar
- Proje sitesi: [https://www.tcknvkn.com](https://www.tcknvkn.com)
- C# sayfası: [https://www.tcknvkn.com/kutuphaneler/csharp](https://www.tcknvkn.com/kutuphaneler/csharp)
- Kütüphaneler: [https://www.tcknvkn.com/kutuphaneler](https://www.tcknvkn.com/kutuphaneler)
- NuGet paketi: [https://www.nuget.org/packages/TcknVkn](https://www.nuget.org/packages/TcknVkn)
- Repository: [https://github.com/tcknvkn/tcknvkn-dotnet](https://github.com/tcknvkn/tcknvkn-dotnet)

## Lisans
MIT. Ayrıntılar için [LICENSE](LICENSE) dosyasına bakabilirsiniz.
