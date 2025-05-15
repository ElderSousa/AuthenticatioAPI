using Application.MS_AuthenticationAutorization.Interfaces;
using Application.MS_AuthenticationAutorization.PaginationModel;
using Application.MS_AuthenticationAutorization.Requests;
using Application.MS_AuthenticationAutorization.Responses;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using static Application.MS_AuthenticationAutorization.Requests.UserRequest;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MS_AuthenticationAutorization.API.Controllers;
/// <summary>
/// Controller responsável pela gestão dos usuários.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private Response _response = new();
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Cria o usuário com os dados informados.
    /// </summary>
    /// <param name="userRequest">Objeto com os parâmtros necessários para criar o usuário.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna um resultado assíncrono</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Response), 200)]
    [ProducesResponseType(typeof(Response), 400)]
    public async Task<IActionResult> CreateAsync(CreateUserRequest userRequest, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateAsync(userRequest, cancellationToken);
        return _response.Error ? BadRequest(_response) : Ok(_response);
    }

    /// <summary>
    /// Busca todos os usuários solicitados na requisição
    /// </summary>
    /// <param name="page">Número da página</param>
    /// <param name="pageSize">Quantidade de itens na página</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna uma lista de paginada de usuários.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Pagination<UserResponse>), 200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var allUsers = await _userService.GetAllAsync(page, pageSize, cancellationToken);
        return !allUsers.Itens.Any() ? NoContent() : Ok(allUsers);
    }
}
