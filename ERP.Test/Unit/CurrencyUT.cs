using ERP.Domain.Shared.ValueObjects;
using FluentAssertions;

namespace ERP.Test.Unit;

[TestFixture]
public class CurrencyUT
{
    [Test]
    public void Should_CreateCurrency_When_CurrencyIsValid()
    {
        Currency currency = Currency.Create("USD");
        currency.Code.Should().Be("USD");
    }

    private static readonly string?[] InvalidCurrencyCases = [
        null, "", " ", new('a', 4), new('a', 2)
    ];
    [TestCaseSource(nameof(InvalidCurrencyCases))]
    public void Should_Throw_When_CurrencyIsInvalid(string? code)
    {
        Action act = () => _ = Currency.Create(code!);
        act.Should().Throw();
    }
}