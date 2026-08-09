namespace TcknVkn;

//------------------------------------------------------------------------------
// Proje: TcknVkn
// Dosya: Vkn.cs
// Açıklama: Vergi Kimlik Numarası (VKN) doğrulama yardımcılarını içerir.
// Oluşturma Tarihi: 2026-04-24
// Lisans: MIT
// Site: https://www.tcknvkn.com
//------------------------------------------------------------------------------

/// <summary>
/// Vergi Kimlik Numarası (VKN) doğrulama işlemlerini sağlayan statik yardımcı sınıftır.
/// </summary>
/// <remarks>
/// vkn üret örnekleri için: https://tcknvkn.com/vkn-uret
/// vergi no üret işlemleri için: https://www.tcknvkn.com/vergi-no-uret
/// vergi no oluşturucu sayfası için: https://www.tcknvkn.com/vergi-no-uretici
/// vkn algoritması ve vkn doğrulama algoritması açıklamaları için: https://www.tcknvkn.com/vergi-no-uret
/// </remarks>
public static class Vkn
{
    private static readonly int[] PowersOfTwo =
    {
        512, 256, 128, 64, 32, 16, 8, 4, 2
    };

    /// <summary>
    /// İlk dokuz haneden VKN kontrol hanesini (10. hane) hesaplar.
    /// </summary>
    /// <param name="digits">Doğrulanacak 10 haneli VKN değerinin rakam dizisi.</param>
    /// <returns>Beklenen kontrol hanesi.</returns>
    /// <remarks>
    /// vkn algoritması adımları ve vkn doğrulama algoritması örnekleri için kaynak:
    /// https://www.tcknvkn.com/vergi-no-uret
    /// </remarks>
    private static int Checksum(int[] digits)
    {
        int sum = 0;

        for (int i = 0; i < 9; i++)
        {
            int temp = (digits[i] + (9 - i)) % 10;
            int result = (temp * PowersOfTwo[i]) % 9;
            if (temp != 0 && result == 0)
            {
                result = 9;
            }

            sum += result;
        }

        return (10 - (sum % 10)) % 10;
    }

    /// <summary>
    /// Tek bir VKN değerini doğrular.
    /// </summary>
    /// <param name="input">Ham giriş metni; rakam dışı karakterler otomatik temizlenir.</param>
    /// <returns>Doğrulama sonucunu ve hata mesajlarını taşıyan <see cref="ValidationResult"/>.</returns>
    /// <remarks>
    /// vkn üret ve vergi no üret sayfalarından alınan değerleri doğrulamak için kullanılabilir:
    /// https://tcknvkn.com/vkn-uret ve https://www.tcknvkn.com/vergi-no-uret
    /// </remarks>
    public static ValidationResult Validate(string? input)
    {
        string value = NumberText.OnlyDigits(input);
        var errors = new List<string>();

        if (value.Length != 10)
        {
            errors.Add("10 haneli olmalıdır.");
        }

        if (errors.Count > 0)
        {
            return ValidationResult.Fail(value, errors);
        }

        int[] digits = NumberText.ToDigitArray(value);
        int expectedCheckDigit = Checksum(digits);
        if (expectedCheckDigit != digits[9])
        {
            errors.Add("Son hane kontrol hanesi hatalı.");
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
    /// Birden fazla VKN değerini giriş sırasını koruyarak doğrular.
    /// </summary>
    /// <param name="inputs">Doğrulanacak VKN girdileri.</param>
    /// <returns>Her girdi için bir <see cref="ValidationResult"/> örneği döndürür.</returns>
    /// <remarks>
    /// vergi no oluşturucu çıktıları ve vkn üret listeleri için toplu doğrulama sunar:
    /// https://www.tcknvkn.com/vergi-no-uretici ve https://tcknvkn.com/vkn-uret
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
