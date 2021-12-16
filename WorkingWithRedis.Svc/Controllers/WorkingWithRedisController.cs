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
        private readonly ILogger<WorkingWithRedisController> _logger;

        public WorkingWithRedisController(
            IUserRepository userRepository,
            ILogger<WorkingWithRedisController> logger
            )
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Set(UserModel user, CancellationToken cancellationToken)
        {
            if (user.IsValid())
                return BadRequest();

            var response = await _userRepository.SetUser(user, cancellationToken);

            if (response == null)
                return BadRequest();

            return Created("", response);
        }

        [HttpGet("{userId}")]
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

            var response = await _userRepository.UpdateUser(user, cancellationToken);

            if (!response)
                return NotFound();

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

            var response = await _userRepository.DeleteUser(userId, cancellationToken);

            if (!response)
                return NotFound();

            return Ok();
        }
    }
}