using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Exceptions;
using FluentAssertions;
using Moq;

namespace BankSystem.Core.Tests.Unit.Domain.Cards.Models.CardTest;

public class CreateTest
{
    private ICardNumberMustBeUniqueChecker CardNumberMustBeUniqueChecker { get; }

    public CreateTest()
    {
        CardNumberMustBeUniqueChecker = Mock.Of<ICardNumberMustBeUniqueChecker>();
    }

    [Fact]
    public async Task Should_create_card()
    {
        //Arrange
        Mock.Get(CardNumberMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var number = "0000000000000000";
        var cardData = new CreateCardData(number, DateOnly.Parse("2020-10-10"), DateOnly.Parse("2024-10-10"), 100m, "Test");

        //Act
        var card = await Card.CreateAsync(cardData, CardNumberMustBeUniqueChecker);

        //Assert
        card.Should().NotBeNull();
        card.Balance.Should().Be(100m);
    }

    [Fact]
    public async Task When_card_number_is_not_unique_Should_throw_exception()
    {
        //Arrange
        Mock.Get(CardNumberMustBeUniqueChecker)
            .Setup(x => x.IsUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var number = "0000000000000000";
        var cardData = new CreateCardData(number, DateOnly.Parse("2020-10-10"), DateOnly.Parse("2024-10-10"), 100m, "Test");
        
        //Act
        var action = async () => await Card.CreateAsync(cardData, CardNumberMustBeUniqueChecker);

        //Assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result.Subject.Single();

        var failure = validationException.Errors.Single();
        failure.PropertyName.Should().Be(nameof(Card.Number));
        failure.ErrorMessage.Should().Be($"Card Number: '{number}' must be unique.");
    }
}
