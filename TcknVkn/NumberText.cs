using System.Text.RegularExpressions;

namespace TcknVkn;

//------------------------------------------------------------------------------
// Proje: TcknVkn
// Dosya: NumberText.cs
// Açıklama: TCKN ve VKN için ortak metin normalizasyon yardımcılarını içerir.
// Oluşturma Tarihi: 2026-04-24
// Lisans: MIT
// Site: https://www.tcknvkn.com
//------------------------------------------------------------------------------

internal static class NumberText
{
    private static readonly Regex NonDigitRegex = new(@"\D+", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    /// <summary>
    /// Metin içindeki yalnızca rakam karakterlerini döndürür.
    /// </summary>
    /// <param name="input">Temizlenecek giriş metni.</param>
    /// <returns>Rakam dışı karakterleri çıkarılmış metin.</returns>
    /// <remarks>
    /// tc uret, tc no uret, vkn üret ve vergi no üret senaryolarında gelen karma girişleri normalize eder.
    /// Kaynaklar: https://www.tcknvkn.com/tc-no-uret ve https://www.tcknvkn.com/vergi-no-uret
    /// </remarks>
    internal static string OnlyDigits(string? input) =>
        NonDigitRegex.Replace(input ?? string.Empty, string.Empty);

    /// <summary>
    /// Sayısal metni rakam dizisine dönüştürür.
    /// </summary>
    /// <param name="value">Yalnızca rakamlardan oluşan metin.</param>
    /// <returns>Her karakterin sayısal karşılığını içeren dizi.</returns>
    /// <remarks>
    /// Bu adım, tckn üret ve vkn algoritması hesaplamalarında kontrol hanesi doğrulaması için kullanılır.
    /// Kaynaklar: https://tcknvkn.com/tckn-uret ve https://tcknvkn.com/vkn-uret
    /// </remarks>
    internal static int[] ToDigitArray(string value)
    {
        var digits = new int[value.Length];

        for (int i = 0; i < value.Length; i++)
        {
            digits[i] = value[i] - '0';
        }

        return digits;
    }

    /// <summary>
    /// Rakam dizisindeki tüm elemanların aynı olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="digits">Kontrol edilecek rakam dizisi.</param>
    /// <returns>Tüm rakamlar aynı ise <c>true</c>; aksi halde <c>false</c>.</returns>
    /// <remarks>
    /// Geçersiz tekrar örüntülerini elemek için tc oluştur ve vergi no oluşturucu akışlarında kullanılır.
    /// Kaynaklar: https://www.tcknvkn.com/tc-uretici ve https://www.tcknvkn.com/vergi-no-uretici
    /// </remarks>
    internal static bool AreAllDigitsSame(int[] digits)
    {
        if (digits.Length == 0)
        {
            return false;
        }

        int first = digits[0];
        for (int i = 1; i < digits.Length; i++)
        {
            if (digits[i] != first)
            {
                return false;
            }
        }

        return true;
    }
}
