using System.Linq;
using TcknVkn;

namespace TcknVkn.Tests;

//------------------------------------------------------------------------------
// Proje: TcknVkn
// Dosya: TcknTests.cs
// Açıklama: TCKN doğrulama senaryoları için birim testleri içerir.
// Oluşturma Tarihi: 2026-04-24
// Lisans: MIT
// Site: https://www.tcknvkn.com
//------------------------------------------------------------------------------

public class TcknTests
{
    [Theory]
    [InlineData("10000000146")]
    [InlineData("100-000-001 46")]
    [InlineData("100.000.001-46")]
    public void Validate_Should_Return_Valid_For_Known_Good_Input(string input)
    {
        ValidationResult result = Tckn.Validate(input);

        Assert.True(result.Valid);
        Assert.Equal("10000000146", result.Value);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData(null)]
    public void Validate_Should_Fail_When_Length_Is_Not_11(string? input)
    {
        ValidationResult result = Tckn.Validate(input);

        Assert.False(result.Valid);
        Assert.Contains("11 haneli olmalıdır.", result.Errors);
    }

    [Fact]
    public void Validate_Should_Fail_When_First_Digit_Is_Zero()
    {
        ValidationResult result = Tckn.Validate("01234567890");

        Assert.False(result.Valid);
        Assert.Contains("İlk hane 0 olamaz.", result.Errors);
    }

    [Fact]
    public void Validate_Should_Fail_When_Check_Digits_Are_Invalid()
    {
        ValidationResult result = Tckn.Validate("10000000145");

        Assert.False(result.Valid);
        Assert.Contains(result.Errors, e => e.Contains("kontrol hanesi", StringComparison.Ordinal));
    }

    [Fact]
    public void Validate_Should_Fail_When_All_Digits_Are_Same()
    {
        ValidationResult result = Tckn.Validate("11111111111");

        Assert.False(result.Valid);
        Assert.Contains("Geçersiz örüntü: tüm haneler aynı.", result.Errors);
    }

    [Fact]
    public void Validate_Should_Normalize_Mixed_Text_Input()
    {
        ValidationResult result = Tckn.Validate("TC: 100.000.001-46");

        Assert.True(result.Valid);
        Assert.Equal("10000000146", result.Value);
    }

    [Fact]
    public void ValidateMultiple_Should_Preserve_Order_And_Run_All_Entries()
    {
        string?[] inputs = ["10000000146", "10000000145", "11111111111"];

        ValidationResult[] results = Tckn.ValidateMultiple(inputs).ToArray();

        Assert.Equal(3, results.Length);
        Assert.True(results[0].Valid);
        Assert.False(results[1].Valid);
        Assert.False(results[2].Valid);
    }

    [Fact]
    public void ValidateMultiple_Should_Handle_Null_Item_As_Invalid_Record()
    {
        string?[] inputs = ["10000000146", null];

        ValidationResult[] results = Tckn.ValidateMultiple(inputs).ToArray();

        Assert.Equal(2, results.Length);
        Assert.True(results[0].Valid);
        Assert.False(results[1].Valid);
        Assert.Contains("11 haneli olmalıdır.", results[1].Errors);
    }

    [Fact]
    public void ValidateMultiple_Should_Throw_For_Null_Collection()
    {
        Assert.Throws<ArgumentNullException>(() => Tckn.ValidateMultiple(null!).ToArray());
    }
}
