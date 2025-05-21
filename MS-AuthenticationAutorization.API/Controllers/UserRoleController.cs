using Application.MS_AuthenticationAutorization.Interfaces;
using Application.MS_AuthenticationAutorization.PaginationModel;
using Application.MS_AuthenticationAutorization.Responses;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using static Application.MS_AuthenticationAutorization.Requests.UserRoleRequest;

namespace MS_AuthenticationAutorization.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private Response _response = new();
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService useRoleService)
        {
            _userRoleService = useRoleService;
        }

        /// <summary>
        /// Cria a userRole com os dados informados.
        /// </summary>
        /// <param name="userRoleRequest">Objeto com os parâmetros necessários para criar a userRole.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna um response com o status da requisição.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        public async Task<IActionResult> CreateAsync(CreateUserRoleRequest userRoleRequest, CancellationToken cancellationToken)
        {
            _response = await _userRoleService.CreateAsync(userRoleRequest, cancellationToken);
            return _response.Error ? BadRequest(_response) : Ok(_response);
        }

        /// <summary>
        /// Busca todos as userRoles solicitadas na requisição
        /// </summary>
        /// <param name="page">Número da página</param>
        /// <param name="pageSize">Quantidade de itens na página</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna uma lista de paginada de roles.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Pagination<UserResponse>), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var allUserRoles = await _userRoleService.GetAllAsync(page, pageSize, cancellationToken);
            return !allUserRoles.Itens.Any() ? NoContent() : Ok(allUserRoles);
        }

        /// <summary>
        /// Busca a userRole pertencente ao Id informado.
        /// </summary>
        /// <param name="id">Parâmetro informado na requisição.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna a role solicitada na requisição.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var userRole = await _userRoleService.GetIdAsync(id, cancellationToken);
                return Ok(userRole);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza a userRole com os dados informados.
        /// </summary>
        /// <param name="userRoleRequest">Parâmetro informado para atualização da userRole.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna um response com o status da requisição.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateUserRoleRequest userRoleRequest, CancellationToken cancellationToken)
        {
            _response = await _userRoleService.UpdateAsync(userRoleRequest, cancellationToken);
            return _response.Error ? base.BadRequest(_response) : base.Ok(_response);
        }

        /// <summary>
        /// Deleta a userRole do Id informado.
        /// </summary>
        /// <param name="id">Parâmetro informado para exclusão da userRole</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna um response com o status da requisição.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            _response = await _userRoleService.SoftDeleteAsync(id, cancellationToken);
            return _response.Error ? BadRequest(_response) : Ok(_response);
        }
    }
}
