using Application.MS_AuthenticationAutorization.Interfaces;
using Application.MS_AuthenticationAutorization.MapperExtension;
using Application.MS_AuthenticationAutorization.PaginationModel;
using Application.MS_AuthenticationAutorization.Responses;
using Domain.MS_AuthorizationAutentication.Entities;
using Domain.MS_AuthorizationAutentication.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static Application.MS_AuthenticationAutorization.Requests.RoleRequest;

namespace Application.MS_AuthenticationAutorization.Services;

public class RoleService : BaseService, IRoleService
{
    private Response _response = new();
    private readonly IRoleRepositoy _roleRepositoy;
    private readonly IValidator<Role> _roleValidator;
    public RoleService(IRoleRepositoy roleRepositoy,
        IValidator<Role> roleValidator, 
        IHttpContextAccessor contextAccessor, 
        ILogger<RoleService> logger) : base(contextAccessor, logger)
    {
        _roleRepositoy = roleRepositoy;
        _roleValidator = roleValidator;
    }
    public async Task<Response> CreateAsync(CreateRoleRequest roleRequest, CancellationToken cancellationToken)
    {
        try
        {
            var role = roleRequest.MapToRole();
            role.ApplyBaseModelFields(GetUserLogged(), DateHourNow(), true);

            _response = await ExecuteValidationResponseAsync(_roleValidator, role);
            if (_response.Error)
                throw new ArgumentException(_response.Status);

            if (!await _roleRepositoy.CreateAsync(role, cancellationToken))
                throw new InvalidOperationException("Falha ao cria uma role.");

            return ReturnResponseSuccess();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao criar role com Name: {Name} em CreateAsync", roleRequest.Name);
            throw;
        }
    }

    public async Task<Pagination<RoleResponse>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var allRoles = RoleMappingExtension.MapToRoleResponse(await _roleRepositoy.GetAllAsync(cancellationToken));
            var pagination = Page(allRoles, page, pageSize);

            if (pagination == null)
                throw new ArgumentException("Falha ao paginar roles.");

            return pagination;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao buscar todas as roles em GetAllAsync");
            throw;
        }
    }

    public async Task<RoleResponse?> GetIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleRepositoy.GetIdAsync(id, cancellationToken);
            if (role == null)
                throw new KeyNotFoundException($"Role com Id: {id} não encontrado");

            return role.MapToRoleResponse();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao buscar role com Id: {Id} GetIdAsync", id);
            throw;
        }
    }

    public async Task<Response> UpdateAsync(UpdateRoleRequest roleRequest, CancellationToken cancellationToken)
    {
        try
        {
            var role = roleRequest.MapToRole();
            role.ApplyBaseModelFields(GetUserLogged(), DateHourNow(), false);
           
            _response = await ExecuteValidationResponseAsync(_roleValidator, role);
            if (_response.Error)
                throw new ArgumentException("Falha ao atualizar usuário.");

            return ReturnResponseSuccess();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao atualizar usuário: {Id}.", roleRequest.Id);
            throw;
        }
    }

    public async Task<Response> SoftDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleRepositoy.GetIdAsync(id, cancellationToken);
            if (role == null)
                throw new KeyNotFoundException($"Role com Id: {id} não encontrado.");

            role.ModifiedOn = DateHourNow();
            role.DeletedOn = DateHourNow();

            if (!await _roleRepositoy.SoftDeleteAsync(role, cancellationToken))
                throw new InvalidOperationException($"Falha ao excluir role com Id: {id}");

            return ReturnResponseSuccess();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao excluir role com Id: {Id} DeleteAsync", id);
            throw;
        }
    }
}
