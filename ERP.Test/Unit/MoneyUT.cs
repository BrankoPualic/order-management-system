using ERP.Domain.Shared.ValueObjects;
using FluentAssertions;

namespace ERP.Test.Unit;

[TestFixture]
public class MoneyUT
{
    [Test]
    public void Should_CreateMoney_When_MoneyIsValid()
    {
        Money money = Money.Create(1, "USD");
        money.Amount.Should().Be(1);
        money.Currency.Should().Be("USD");
    }

    private static readonly object?[] InvalidMoneyCases = [
        new object?[] { 0m, "USD" },
        new object?[] { -1m, "USD" },

        new object?[] { 1m, "" },
        new object?[] { 1m, " " },
        new object?[] { 1m, null },
        new object?[] { 1m, new string('a', 4) },
        new object?[] { 1m, new string('a', 2) },
    ];
    [TestCaseSource(nameof(InvalidMoneyCases))]
    public void Should_Throw_When_MoneyIsNotValid(decimal amount, string? currency)
    {
        Action act = () => _ = Money.Create(amount, currency!);
        act.Should().Throw();
    }
}