using ERP.Domain.Shared.ValueObjects;
using FluentAssertions;

namespace ERP.Test.Unit;

[TestFixture]
public class MoneyUT
{
    private static Currency _currency = Currency.Create("USD");

    [Test]
    public void Should_CreateMoney_When_MoneyIsValid()
    {
        Money money = Money.Create(1m, _currency);
        money.Amount.Should().Be(1m);
        money.Currency.Should().Be(_currency);
    }

    private static readonly object?[] InvalidMoneyCases = [
        new object?[] { 0m, _currency },
        new object?[] { -1m, _currency },
        new object?[] { 1m, null },
    ];
    [TestCaseSource(nameof(InvalidMoneyCases))]
    public void Should_Throw_When_MoneyIsInvalid(decimal amount, Currency? currency)
    {
        Action act = () => _ = Money.Create(amount, currency!);
        act.Should().Throw();
    }

    [Test]
    public void Should_Add_When_CurrenciesMatch()
    {
        Money left = Money.Create(1m, _currency);
        Money right = Money.Create(2.3m, _currency);
        Money add1 = left.Add(right);
        Money add2 = left + right;
        add1.Amount.Should().Be(3.3m);
        add2.Amount.Should().Be(3.3m);
    }

    [Test]
    public void Should_Throw_When_AddingMoneyOfDifferentCurrencies()
    {
        Money left = Money.Create(1m, Currency.Create("USD"));
        Money right = Money.Create(1m, Currency.Create("RSD"));
        Action act1 = () => _ = left.Add(right);
        Action act2 = () => _ = left + right;
        act1.Should().Throw();
        act2.Should().Throw();
    }

    [Test]
    public void Should_Subtract_When_CurrenciesMatchAndSubtractorIsBigger()
    {
        Money left = Money.Create(2.3m, _currency);
        Money right = Money.Create(1m, _currency);
        Money subtract1 = left.Subtract(right);
        Money subtract2 = left - right;
        subtract1.Amount.Should().Be(1.3m);
        subtract2.Amount.Should().Be(1.3m);
    }

    private static readonly object?[] SubtractInvalidCases = [
        new object?[] { Money.Create(1m, _currency), Money.Create(2m, _currency) },
        new object?[] { Money.Create(2m, Currency.Create("USD")), Money.Create(1m, Currency.Create("RSD")) }
    ];
    [TestCaseSource(nameof(SubtractInvalidCases))]
    public void Should_Throw_When_MoneySubtractIsInvalid(Money left, Money right)
    {
        Action act1 = () => _ = left.Subtract(right);
        Action act2 = () => _ = left - right;
        act1.Should().Throw();
        act2.Should().Throw();
    }

    [Test]
    public void Should_Scale_When_FacotrPositive()
    {
        Money money = Money.Create(2m, _currency);
        Money scaled1 = money.Scale(2m);
        Money scaled2 = money * 2m;
        Money scaled3 = 2m * money;
        scaled1.Amount.Should().Be(4m);
        scaled2.Amount.Should().Be(4m);
        scaled3.Amount.Should().Be(4m);
    }

    private static readonly decimal[] ScaleInvalidCases = [0m, -1m];
    [TestCaseSource(nameof(ScaleInvalidCases))]
    public void Should_Throw_When_MoneyScaleIsInvalid(decimal factor)
    {
        Money money = Money.Create(2m, _currency);
        Action act1 = () => money.Scale(factor);
        Action act2 = () => _ = money * factor;
        Action act3 = () => _ = factor * money;
        act1.Should().Throw();
        act2.Should().Throw();
        act3.Should().Throw();
    }
}