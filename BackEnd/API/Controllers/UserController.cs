using API.DTOs;
using AutoMapper;
using Business.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new UserReadQuery();
            var userBusinessDtos = await _mediator.Send(query);
            var userApiDtos = _mapper.Map<IEnumerable<UserApiDto>>(userBusinessDtos);
            return Ok(userApiDtos);
        }
    }
}
