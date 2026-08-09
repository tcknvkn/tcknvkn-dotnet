using System.Linq;
using TcknVkn;

namespace TcknVkn.Tests;

//------------------------------------------------------------------------------
// Proje: TcknVkn
// Dosya: VknTests.cs
// Açıklama: VKN doğrulama senaryoları için birim testleri içerir.
// Oluşturma Tarihi: 2026-04-24
// Lisans: MIT
// Site: https://www.tcknvkn.com
//------------------------------------------------------------------------------

public class VknTests
{
    [Theory]
    [InlineData("0850005256")]
    [InlineData("085-000-52 56")]
    [InlineData("085.000.5256")]
    public void Validate_Should_Return_Valid_For_Known_Good_Input(string input)
    {
        ValidationResult result = Vkn.Validate(input);

        Assert.True(result.Valid);
        Assert.Equal("0850005256", result.Value);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1234567")]
    [InlineData(null)]
    public void Validate_Should_Fail_When_Length_Is_Not_10(string? input)
    {
        ValidationResult result = Vkn.Validate(input);

        Assert.False(result.Valid);
        Assert.Contains("10 haneli olmalıdır.", result.Errors);
    }

    [Fact]
    public void Validate_Should_Fail_When_Check_Digit_Is_Invalid()
    {
        ValidationResult result = Vkn.Validate("0850005257");

        Assert.False(result.Valid);
        Assert.Contains("Son hane kontrol hanesi hatalı.", result.Errors);
    }

    [Fact]
    public void Validate_Should_Fail_When_All_Digits_Are_Same()
    {
        ValidationResult result = Vkn.Validate("1111111111");

        Assert.False(result.Valid);
        Assert.Contains("Geçersiz örüntü: tüm haneler aynı.", result.Errors);
    }

    [Fact]
    public void Validate_Should_Normalize_Mixed_Text_Input()
    {
        ValidationResult result = Vkn.Validate("Vergi No: 085-000-52 56");

        Assert.True(result.Valid);
        Assert.Equal("0850005256", result.Value);
    }

    [Fact]
    public void ValidateMultiple_Should_Preserve_Order_And_Run_All_Entries()
    {
        string?[] inputs = ["0850005256", "0850005257", "1111111111"];

        ValidationResult[] results = Vkn.ValidateMultiple(inputs).ToArray();

        Assert.Equal(3, results.Length);
        Assert.True(results[0].Valid);
        Assert.False(results[1].Valid);
        Assert.False(results[2].Valid);
    }

    [Fact]
    public void ValidateMultiple_Should_Handle_Null_Item_As_Invalid_Record()
    {
        string?[] inputs = ["0850005256", null];

        ValidationResult[] results = Vkn.ValidateMultiple(inputs).ToArray();

        Assert.Equal(2, results.Length);
        Assert.True(results[0].Valid);
        Assert.False(results[1].Valid);
        Assert.Contains("10 haneli olmalıdır.", results[1].Errors);
    }

    [Fact]
    public void ValidateMultiple_Should_Throw_For_Null_Collection()
    {
        Assert.Throws<ArgumentNullException>(() => Vkn.ValidateMultiple(null!).ToArray());
    }
}
