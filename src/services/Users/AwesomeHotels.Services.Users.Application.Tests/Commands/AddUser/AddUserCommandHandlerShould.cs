using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using AwesomeHotels.Services.Users.Application.Commands.AddUser;
using AwesomeHotels.Services.Users.Domain.Entities;
using AwesomeHotels.Services.Users.Domain.Factories;
using AwesomeHotels.Services.Users.Domain.Repositories;
using AwesomeHotels.Services.Users.Domain.Specifications;
using BuildingBlocks.Application;
using BuildingBlocks.Application.IdGeneration;
using BuildingBlocks.Domain.Specifications;
using BuildingBlocks.UnitTests.Common;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AwesomeHotels.Services.Users.Application.Tests.Commands.AddUser;

public class AddUserCommandHandlerShould
{
    private readonly Fixture _fixture;

    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordFactory _passwordFactory;
    private readonly IIdGenerator _idGenerator;
    private readonly IDateTimeService _dateService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AddUserCommandHandler _sut;

    public AddUserCommandHandlerShould()
    {
        _fixture = new Fixture();

        _usersRepository = Substitute.For<IUsersRepository>();
        _passwordFactory = Substitute.For<IPasswordFactory>();
        _idGenerator = Substitute.For<IIdGenerator>();
        _dateService = Substitute.For<IDateTimeService>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _sut = new AddUserCommandHandler(_usersRepository, _passwordFactory, _idGenerator, _dateService, _unitOfWork);

        _passwordFactory.CreatePassword(Arg.Any<string>()).Returns(_fixture.Create<string>());
    }

    [Fact]
    public async Task CheckIfUserWithTheSameEmailAlreadyExistInDb()
    {
        // Arrange
        var command = CreateCommand();
        _usersRepository.ExistsAsync(Arg.Any<Specification<User>>()).Returns(true);
        var user = CreateUser(null, command.Email);

        // Act
        await _sut.Handle(command, default);

        // Assert
        await _usersRepository
            .Received(1)
            .ExistsAsync(
            Arg.Is<UserWithEmailSpecification>(y =>
                y.IsSatisfied(user)));
    }

    [Fact]
    public async Task ReturnErrorIfUserWithGivenEmailAlreadyExist()
    {
        // Arrange
        var command = CreateCommand();
        _usersRepository.ExistsAsync(Arg.Any<Specification<User>>()).Returns(true);

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.ErrorShould().FailedWithError(Errors.AddUser.UserAlreadyExists(command.Email));
    }

    [Theory]
    [AutoData]
    public async Task GenerateNewId(long id)
    {
        // Arrange
        var command = CreateCommand();
        _usersRepository.ExistsAsync(Arg.Any<Specification<User>>()).Returns(false);
        _idGenerator.Generate().Returns(id);

        // Act
        await _sut.Handle(command, default);

        // Assert
        _idGenerator.Received(1).Generate();
    }

    [Theory]
    [AutoData]
    public async Task GetsDateTimeOffsetUtcNowFromDateTimeService(long id)
    {
        // Arrange
        var command = CreateCommand();
        _usersRepository.ExistsAsync(Arg.Any<Specification<User>>()).Returns(false);
        _idGenerator.Generate().Returns(id);

        // Act
        await _sut.Handle(command, default);

        // Assert
        _dateService.Received(1).GetDateTimeOffsetUtcNow();
    }

    [Theory]
    [AutoData]
    public async Task CreatesPasswordInFactory(long id)
    {
        // Arrange
        var command = CreateCommand();
        _usersRepository.ExistsAsync(Arg.Any<Specification<User>>()).Returns(false);
        _idGenerator.Generate().Returns(id);

        // Act
        await _sut.Handle(command, default);

        // Assert
        _passwordFactory.Received(1).CreatePassword(command.Password);
    }

    [Theory]
    [AutoData]
    public async Task AddsUserToRepository(long id, DateTimeOffset utcNow, string password)
    {
        // Arrange
        var command = CreateCommand();
        _usersRepository.ExistsAsync(Arg.Any<Specification<User>>()).Returns(false);
        _idGenerator.Generate().Returns(id);
        _dateService.GetDateTimeOffsetUtcNow().Returns(utcNow);
        _passwordFactory.CreatePassword(Arg.Any<string>()).Returns(password);

        // Act
        await _sut.Handle(command, default);

        // Assert
        _usersRepository.Received(1)
            .Add(Arg.Is<User>(x =>
                x.Id.Value == id &&
                x.EmailAddress.Value == command.Email &&
                x.FirstName == command.FirstName &&
                x.LastName == command.LastName &&
                x.DateOfBirth.Value == DateOnly.FromDateTime(command.DateOfBirth) &&
                x.Password.Hash == password &&
                x.CreationDateTime == utcNow));
    }

    [Theory]
    [AutoData]
    public async Task SavesChanges(long id, DateTimeOffset utcNow, string password)
    {
        // Arrange
        var command = CreateCommand();
        _usersRepository.ExistsAsync(Arg.Any<Specification<User>>()).Returns(false);
        _idGenerator.Generate().Returns(id);
        _dateService.GetDateTimeOffsetUtcNow().Returns(utcNow);
        _passwordFactory.CreatePassword(Arg.Any<string>()).Returns(password);

        // Act
        await _sut.Handle(command, default);

        // Assert
        await _unitOfWork.Received(1).SaveChangesAsync();
    }

    [Theory]
    [AutoData]
    public async Task ReturnUserId(long id, DateTimeOffset utcNow, string password)
    {
        // Arrange
        var command = CreateCommand();
        _usersRepository.ExistsAsync(Arg.Any<Specification<User>>()).Returns(false);
        _idGenerator.Generate().Returns(id);
        _dateService.GetDateTimeOffsetUtcNow().Returns(utcNow);
        _passwordFactory.CreatePassword(Arg.Any<string>()).Returns(password);

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.Value.Should().Be(id);
    }

    private User CreateUser(long? id = default, string? email = default)
    {
        return User.Create(
            id ?? _fixture.Create<long>(),
            email ?? "test@mail.com",
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            DateTime.UtcNow.AddYears(- 19),
            _passwordFactory,
            _fixture.Create<string>(),
            _fixture.Create<DateTimeOffset>());
    }

    private AddUserCommand CreateCommand()
    {
        return _fixture.Build<AddUserCommand>()
            .With(x => x.Email, "test@mail.com")
            .With(x => x.DateOfBirth, DateTime.UtcNow.AddYears(-19))
            .Create();
    }
}