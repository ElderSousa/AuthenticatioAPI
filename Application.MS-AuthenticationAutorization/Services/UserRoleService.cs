using Application.MS_AuthenticationAutorization.Interfaces;
using Application.MS_AuthenticationAutorization.MapperExtension;
using Application.MS_AuthenticationAutorization.PaginationModel;
using Application.MS_AuthenticationAutorization.Responses;
using Domain.MS_AuthorizationAutentication.Entities;
using Domain.MS_AuthorizationAutentication.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static Application.MS_AuthenticationAutorization.Requests.UserRoleRequest;

namespace Application.MS_AuthenticationAutorization.Services;

public class UserRoleService : BaseService, IUserRoleService
{
    private Response _response = new();
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IValidator<UserRole> _userRoleValidator;
    public UserRoleService(IUserRoleRepository userRoleRepository,
        IValidator<UserRole> userRoleValidator,
        IHttpContextAccessor contextAccessor,
        ILogger<UserRoleService> logger) : base(contextAccessor, logger)
    {
        _userRoleRepository = userRoleRepository;
        _userRoleValidator = userRoleValidator;
    }
    public async Task<Response> CreateAsync(CreateUserRoleRequest userRoleRequest, CancellationToken cancellationToken)
    {
        try
        {
            var userRole = userRoleRequest.MapToUseRole();
            userRole.ApplyBaseModelFields(GetUserLogged(), DateHourNow(), true);

            _response = await ExecuteValidationResponseAsync(_userRoleValidator, userRole);
            if (_response.Error)
                throw new ArgumentException(_response.Status);

            if (!await _userRoleRepository.CreateAsync(userRole, cancellationToken))
                throw new InvalidOperationException("Falha ao criar userRole");

            return ReturnResponseSuccess();

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao criar userRole com UserId: {UserId} e RoleId: {RoleId} em CreateAsync", userRoleRequest.UserId, userRoleRequest.RoleId);
            throw;
        }
    }

    public async Task<Pagination<UserRoleResponse>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var allUserRoles = UserRoleMappingExtension.MapToUseRoleResponse( await _userRoleRepository.GetAllAsync(cancellationToken));

            var pagination = Page(allUserRoles, page, pageSize);
            if (pagination == null)
                throw new ArgumentException("Falha ao paginar userRoles.");

            return pagination;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao buscar todos as userRoles em GetAllAsync");
            throw;
        }
    }

    public async Task<UserRoleResponse> GetIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        try
        {
            var userRole = await _userRoleRepository.GetIdAsync(userId, roleId, cancellationToken);
            if (userRole == null)
                throw new KeyNotFoundException($"UserRole com UserId: {userId} e RoleId: {roleId} não encontrado");

            return userRole.MapToUseRoleResponse();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao buscar role com UserId: {userId} e RoleId: {roleId} GetIdAsync", userId, roleId);
            throw;
        }
    }

    public async Task<Response> UpdateAsync(UpdateUserRoleRequest userRoleRequest, CancellationToken cancellationToken)
    {
        try
        {
            var userRole = UserRoleMappingExtension.MapToUseRole(userRoleRequest, await GetIdAsync(userRoleRequest.Id, userRoleRequest.RoleId, cancellationToken));
            userRole.ApplyBaseModelFields(GetUserLogged(), DateHourNow(), false);

            _response = await ExecuteValidationResponseAsync(_userRoleValidator, userRole);
            if (_response.Error)
                throw new ArgumentException(_response.Status);

            if (!await _userRoleRepository.UpdateAsync(userRole, cancellationToken))
                throw new InvalidOperationException("Falha ao atualizar userRole.");

            return ReturnResponseSuccess();
        } 
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao atualizar usuário: {Id}.", userRoleRequest.Id);
            throw;
        }
    }

    public async Task<Response> SoftDeleteAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        try
        {
            var userRole = await _userRoleRepository.GetIdAsync(userId, roleId, cancellationToken);
            if (userRole == null)
                throw new KeyNotFoundException($"UserRole com UserId: {userId} e RoleId: {roleId} não encontrado");

            userRole.ModifiedOn = DateHourNow();
            userRole.DeletedOn = DateHourNow();

            if (!await _userRoleRepository.SoftDeleteAsync(userRole, cancellationToken))
                throw new InvalidOperationException($"Falha ao excluir role com UserId: {userId} e RoleId: {roleId}");

            return ReturnResponseSuccess();        
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao excluir role com UserId: {userId} e RoleId: {roleId} GetIdAsync", userId, roleId);
            throw; ;
        }
    }
}
