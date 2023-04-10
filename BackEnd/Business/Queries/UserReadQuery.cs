using Business.Dtos;
using MediatR;

namespace Business.Queries
{
    public class UserReadQuery : IRequest<IEnumerable<UserBusinessDto>> { }
}