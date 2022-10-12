using AwesomeHotels.Services.Users.Api.Controllers.Users.v1;
using AwesomeHotels.Services.Users.Application.AddUser;
using Mapster;

namespace AwesomeHotels.Services.Users.Api.Mappings;

public class AddUserRequestMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddUserRequest, AddUserCommand>()
            .Map(dest => dest.DateOfBirth, src => DateOnly.FromDateTime(src.DateOfBirth));
    }
}