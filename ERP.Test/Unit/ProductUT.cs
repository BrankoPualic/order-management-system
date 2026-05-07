using ERP.Domain;
using ERP.Domain.Shared.ValueObjects;
using FluentAssertions;

namespace ERP.Test.Unit;

[TestFixture]
public class ProductUT
{
    private static Money _price;

    [SetUp]
    public void SetUp()
    {
        _price = Money.Create(1m, Currency.Create("USD"));
    }

    [Test]
    public void Should_RegisterProduct_When_ProductIsValid()
    {
        Product product = Product.Register("name", "description", _price);
        product.Name.Should().Be("name");
        product.Description.Should().Be("description");
        product.Price.Should().Be(_price);
        product.PublicId.Should().NotBeNull();
        product.CreatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(10));
    }

    private static readonly object?[] ProductInvalidCases = [
        new object?[] { "", "description", _price },
        new object?[] { " ", "description", _price },
        new object?[] { null, "description", _price },
        new object?[] { new string('a', 256), "description", _price },

        new object?[] { "name", "", _price },
        new object?[] { "name", " ", _price },
        new object?[] { "name", null, _price },

        new object?[] { "name", "description", null }
    ];
    [TestCaseSource(nameof(ProductInvalidCases))]
    public void Should_Throw_When_CompanyIsInvalid(string? name, string? description, Money? price)
    {
        Action act = () => Product.Register(name!, description!, price!);
        act.Should().Throw();
    }

    [Test]
    public void Should_Rename_When_NameIsValid()
    {
        Product product = Product.Register("name", "description", _price);
        product.Rename("new name");
        product.Name.Should().Be("new name");
    }

    private readonly static string?[] InvalidRenamingCase = ["", " ", null, new string('a', 256)];
    [TestCaseSource(nameof(InvalidRenamingCase))]
    public void Should_Throw_When_InvalidRenaming(string? name)
    {
        Product product = Product.Register("name", "description", _price);
        Action act = () => product.Rename(name!);
        act.Should().Throw();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void Should_Throw_When_SettingInvalidDescription(string? description)
    {
        Product product = Product.Register("name", "description", _price);
        Action act = () => product.ChangeDescription(description!);
        act.Should().Throw();
    }

    [Test]
    public void Should_ChangeDescription_When_DescriptionIsValid()
    {
        Product product = Product.Register("name", "description", _price);
        product.ChangeDescription("new description");
        product.Description.Should().Be("new description");
    }

    [Test]
    public void Should_Throw_When_SettingInvalidPrice()
    {
        Product product = Product.Register("name", "description", _price);
        Action act = () => product.ChangePrice(null!);
        act.Should().Throw();
    }

    [Test]
    public void Should_ChangePrice_When_PriceIsValid()
    {
        Product product = Product.Register("name", "description", _price);
        Currency currency = Currency.Create("RSD");
        product.ChangePrice(Money.Create(_price.Amount + 1, currency));
        product.Price.Amount.Should().Be(_price.Amount + 1);
        product.Price.Currency.Should().Be(currency);
    }
}