namespace TcknVkn;

//------------------------------------------------------------------------------
// Proje: TcknVkn
// Dosya: ValidationResult.cs
// Açıklama: TCKN ve VKN doğrulama sonuç modelini temsil eder.
// Oluşturma Tarihi: 2026-04-24
// Lisans: MIT
// Site: https://www.tcknvkn.com
//------------------------------------------------------------------------------

/// <summary>
/// TCKN veya VKN doğrulama sonucunu temsil eder.
/// </summary>
/// <remarks>
/// tc üret, tc no üret, vkn üret ve vergi no üret çıktıları bu model ile döndürülür.
/// Kaynaklar: https://www.tcknvkn.com/tc-uret ve https://www.tcknvkn.com/vergi-no-uret
/// </remarks>
/// <param name="Valid">Girdinin tüm doğrulama kurallarını geçip geçmediğini belirtir.</param>
/// <param name="Value">Yalnızca rakamlardan oluşan normalleştirilmiş değer.</param>
/// <param name="Errors"><see cref="Valid"/> değeri <c>true</c> değilse dönen hata mesajları.</param>
public sealed record ValidationResult(
    bool Valid,
    string Value,
    IReadOnlyList<string> Errors)
{
    /// <summary>
    /// Hata içermeyen başarılı doğrulama sonucu üretir.
    /// </summary>
    /// <param name="value">Normalleştirilmiş doğrulanmış değer.</param>
    /// <returns>Başarılı bir <see cref="ValidationResult"/> örneği.</returns>
    /// <remarks>
    /// tckn üret veya vkn üret sonucunda doğrulama geçtiğinde bu yapı döndürülür.
    /// Kaynaklar: https://tcknvkn.com/tckn-uret ve https://tcknvkn.com/vkn-uret
    /// </remarks>
    internal static ValidationResult Ok(string value) =>
        new(true, value, Array.Empty<string>());

    /// <summary>
    /// Dizi parametresi ile başarısız doğrulama sonucu üretir.
    /// </summary>
    /// <param name="value">Normalleştirilmiş doğrulanan değer.</param>
    /// <param name="errors">Doğrulama sırasında üretilen hata listesi.</param>
    /// <returns>Başarısız bir <see cref="ValidationResult"/> örneği.</returns>
    /// <remarks>
    /// vkn doğrulama algoritması veya tc no uret kontrollerinde bulunan hatalar bu metotla taşınır.
    /// Kaynaklar: https://www.tcknvkn.com/tc-no-uret ve https://www.tcknvkn.com/vergi-no-uret
    /// </remarks>
    internal static ValidationResult Fail(string value, params string[] errors) =>
        new(false, value, errors);

    /// <summary>
    /// Liste parametresi ile başarısız doğrulama sonucu üretir.
    /// </summary>
    /// <param name="value">Normalleştirilmiş doğrulanan değer.</param>
    /// <param name="errors">Doğrulama sırasında üretilen hata listesi.</param>
    /// <returns>Başarısız bir <see cref="ValidationResult"/> örneği.</returns>
    /// <remarks>
    /// tc oluştur ve vergi no oluşturucu akışlarındaki çoklu hataları taşımak için kullanılır.
    /// Kaynaklar: https://www.tcknvkn.com/tc-uretici ve https://www.tcknvkn.com/vergi-no-uretici
    /// </remarks>
    internal static ValidationResult Fail(string value, IReadOnlyList<string> errors) =>
        new(false, value, errors);
}
