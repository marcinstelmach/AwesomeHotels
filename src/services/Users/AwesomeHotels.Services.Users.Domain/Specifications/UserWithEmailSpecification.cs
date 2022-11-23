using System.Linq.Expressions;
using AwesomeHotels.Services.Users.Domain.Entities;
using BuildingBlocks.Domain.Specifications;

namespace AwesomeHotels.Services.Users.Domain.Specifications;

public class UserWithEmailSpecification : Specification<User>
{
    private readonly string _email;

    public UserWithEmailSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return x => x.EmailAddress.Value == _email;
    }
}