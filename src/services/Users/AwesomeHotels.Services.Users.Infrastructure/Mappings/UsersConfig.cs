using AwesomeHotels.Services.Users.Application.Queries.GetUsers;
using AwesomeHotels.Services.Users.Domain.Entities;
using Mapster;

namespace AwesomeHotels.Services.Users.Infrastructure.Mappings;

public class UsersConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, GetUsersResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Email, src => src.EmailAddress.Value)
            .Map(dest => dest.DateOfBirth, src => src.DateOfBirth.Value);
    }
}