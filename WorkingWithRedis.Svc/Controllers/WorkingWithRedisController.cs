using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Set(UserModel user, CancellationToken cancellationToken)
        {
            await _userRepository.SetUserAsync(user, cancellationToken);
            return Created("","");
        }

        [HttpGet("{cpfCnpj}")]
        [ResponseCache(CacheProfileName = "Default900")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string cpfCnpj, CancellationToken cancellationToken)
        {
            if (!Regex.IsMatch(cpfCnpj, @"^[0-9]+$"))
                return BadRequest("Please enter numbers only in Cpf/Cnpj");

            if (cpfCnpj.Length > 14)
                return BadRequest("Cpf/Cnpj must not be longer than 14 characters");

            var response = await _userRepository.GetUserAsync(cpfCnpj, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UserModel user, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateUserAsync(user, cancellationToken);
            return Ok();
        }

        [HttpDelete("{cpfCnpj}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string cpfCnpj, CancellationToken cancellationToken)
        {
            if (!Regex.IsMatch(cpfCnpj, @"^[0-9]+$"))
                return BadRequest("Please enter numbers only in Cpf/Cnpj");

            if (cpfCnpj.Length > 14)
                return BadRequest("Cpf/Cnpj must not be longer than 14 characters");

            await _userRepository.DeleteUserAsync(cpfCnpj, cancellationToken);
            return Ok();
        }
    }
}