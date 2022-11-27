using AwesomeHotels.Services.Users.Domain.Entities;
using AwesomeHotels.Services.Users.Domain.Factories;
using AwesomeHotels.Services.Users.Domain.Repositories;
using AwesomeHotels.Services.Users.Domain.Specifications;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Bus;
using BuildingBlocks.Application.IdGeneration;
using ErrorOr;

namespace AwesomeHotels.Services.Users.Application.Commands.AddUser;

public class AddUserCommandHandler : ICommandHandler<AddUserCommand, ErrorOr<long>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordFactory _passwordFactory;
    private readonly IIdGenerator _idGenerator;
    private readonly IDateTimeService _dateService;
    private readonly IUnitOfWork _unitOfWork;

    public AddUserCommandHandler(
        IUsersRepository usersRepository, 
        IPasswordFactory passwordFactory, 
        IIdGenerator idGenerator, 
        IDateTimeService dateService,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _passwordFactory = passwordFactory;
        _idGenerator = idGenerator;
        _dateService = dateService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<long>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var specification = new UserWithEmailSpecification(request.Email);
        var existUserWithSameEmail = await _usersRepository.ExistsAsync(specification);
        if (existUserWithSameEmail)
        {
            return Errors.AddUser.UserAlreadyExists(request.Email);
        }

        var id = _idGenerator.Generate();
        var utcNow = _dateService.GetDateTimeOffsetUtcNow();
        var user = User.Create(id, request.Email, request.FirstName, request.LastName, request.DateOfBirth, _passwordFactory, request.Password, utcNow);
        _usersRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id.Value;
    }
}