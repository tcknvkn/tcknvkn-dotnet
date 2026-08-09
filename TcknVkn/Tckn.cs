namespace TcknVkn;

//------------------------------------------------------------------------------
// Proje: TcknVkn
// Dosya: Tckn.cs
// Açıklama: TC Kimlik Numarası (TCKN) doğrulama yardımcılarını içerir.
// Oluşturma Tarihi: 2026-04-24
// Lisans: MIT
// Site: https://www.tcknvkn.com
//------------------------------------------------------------------------------

/// <summary>
/// TC Kimlik Numarası (TCKN) doğrulama işlemlerini sağlayan statik yardımcı sınıftır.
/// </summary>
/// <remarks>
/// tc üret, tc uret ve tc no üret niyeti için: https://www.tcknvkn.com/tc-uret
/// tc no uret varyasyonu için: https://www.tcknvkn.com/tc-no-uret
/// tckn üret örnekleri için: https://tcknvkn.com/tckn-uret
/// tc oluştur adımları için: https://www.tcknvkn.com/tc-uretici
/// </remarks>
public static class Tckn
{
    /// <summary>
    /// Tek bir TC Kimlik Numarası değerini doğrular.
    /// </summary>
    /// <param name="input">Ham giriş metni; rakam dışı karakterler otomatik temizlenir.</param>
    /// <returns>Doğrulama sonucunu ve hata mesajlarını taşıyan <see cref="ValidationResult"/>.</returns>
    /// <remarks>
    /// Bu metot, tc üret ve tc no üret senaryolarında elde edilen değerlerin doğrulanması için kullanılabilir:
    /// https://www.tcknvkn.com/tc-uret ve https://www.tcknvkn.com/tc-no-uret
    /// </remarks>
    public static ValidationResult Validate(string? input)
    {
        string value = NumberText.OnlyDigits(input);
        var errors = new List<string>();

        if (value.Length != 11)
        {
            errors.Add("11 haneli olmalıdır.");
        }

        if (value.StartsWith("0", StringComparison.Ordinal))
        {
            errors.Add("İlk hane 0 olamaz.");
        }

        if (errors.Count > 0)
        {
            return ValidationResult.Fail(value, errors);
        }

        int[] digits = NumberText.ToDigitArray(value);

        int odd = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
        int even = digits[1] + digits[3] + digits[5] + digits[7];
        int tenthDigit = (((odd * 7) - even) % 10 + 10) % 10;
        if (tenthDigit != digits[9])
        {
            errors.Add("10. hane kontrol hanesi hatalı.");
        }

        int firstTenSum = 0;
        for (int i = 0; i < 10; i++)
        {
            firstTenSum += digits[i];
        }

        if (firstTenSum % 10 != digits[10])
        {
            errors.Add("11. hane kontrol hanesi hatalı.");
        }

        if (NumberText.AreAllDigitsSame(digits))
        {
            errors.Add("Geçersiz örüntü: tüm haneler aynı.");
        }

        return errors.Count == 0
            ? ValidationResult.Ok(value)
            : ValidationResult.Fail(value, errors);
    }

    /// <summary>
    /// Birden fazla TCKN değerini giriş sırasını koruyarak doğrular.
    /// </summary>
    /// <param name="inputs">Doğrulanacak TCKN girdileri.</param>
    /// <returns>Her girdi için bir <see cref="ValidationResult"/> örneği döndürür.</returns>
    /// <remarks>
    /// tc no uret ve tc oluştur akışlarında toplu doğrulama gerektiğinde bu metot kullanılabilir:
    /// https://www.tcknvkn.com/tc-no-uret ve https://www.tcknvkn.com/tc-uretici
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="inputs"/> değeri <c>null</c> ise fırlatılır.</exception>
    public static IEnumerable<ValidationResult> ValidateMultiple(IEnumerable<string?> inputs)
    {
        if (inputs is null)
        {
            throw new ArgumentNullException(nameof(inputs));
        }

        foreach (string? input in inputs)
        {
            yield return Validate(input);
        }
    }
}
