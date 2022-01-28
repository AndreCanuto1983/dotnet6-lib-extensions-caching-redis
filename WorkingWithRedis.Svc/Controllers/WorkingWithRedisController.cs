using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace WorkingWithRedis.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkingWithRedisController : ControllerBase
    {
        private readonly IUserRepository _userRepository;       

        public WorkingWithRedisController(IUserRepository userRepository)
        {
            _userRepository = userRepository;            
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Set(UserModel user, CancellationToken cancellationToken)
        {
            if (user.IsValid())
                return BadRequest();

            var response = await _userRepository.SetUser(user, cancellationToken);

            return Created("", response);
        }

        [HttpGet("{userId}")]
        [ResponseCache(CacheProfileName = "Default900")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid userId, CancellationToken cancellationToken)
        {
            if (userId == Guid.Empty)
                return BadRequest();

            var response = await _userRepository.GetUser(userId, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UserModel user, CancellationToken cancellationToken)
        {
            if (user.IsValid())
                return BadRequest();

            await _userRepository.UpdateUser(user, cancellationToken);

            return Ok();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid userId, CancellationToken cancellationToken)
        {
            if (userId == Guid.Empty)
                return BadRequest();

            await _userRepository.DeleteUser(userId, cancellationToken);

            return Ok();
        }
    }
}