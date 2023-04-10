using AutoMapper;
using Business.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Business.Queries.Handlers
{
    public class UserReadQueryHandler : IRequestHandler<UserReadQuery, IEnumerable<UserBusinessDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserReadQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserBusinessDto>> Handle(UserReadQuery request, CancellationToken cancellationToken)
        {
            var userEntities = await _userRepository.ReadAllAsync();
            var userBusinessDtos = _mapper.Map<IEnumerable<UserBusinessDto>>(userEntities);
            return userBusinessDtos;
        }
    }
}
