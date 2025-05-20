using Application.MS_AuthenticationAutorization.Interfaces;
using Application.MS_AuthenticationAutorization.MapperExtension;
using Application.MS_AuthenticationAutorization.PaginationModel;
using Application.MS_AuthenticationAutorization.Responses;
using Domain.MS_AuthorizationAutentication.Entities;
using Domain.MS_AuthorizationAutentication.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static Application.MS_AuthenticationAutorization.Requests.UserRequest;

namespace Application.MS_AuthenticationAutorization.Services;

public class UserService : BaseService, IUserService
{
    private Response _response = new();
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;
    public UserService(IUserRepository userRepository,
        IValidator<User> userValidator,
        IHttpContextAccessor contextAccessor,
        ILogger<UserService> logger) : base(contextAccessor, logger)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }

    public async Task<Response> CreateAsync(CreateUserRequest userRequest, CancellationToken cancellationToken)
    {
        try
        {
            var user = UserMappingExtension.MapToUser(userRequest);
            user.ApplyBaseModelFields(GetUserLogged(), DateHourNow(), true);
            user.ValidationRegister = true;

            _response = await ExecuteValidationResponse(_userValidator, user);
            if (_response.Error)
                throw new ArgumentException(_response.Status);

            user.PasswordHash = GeneratePasswordHash(user.PasswordHash);

            if (!await _userRepository.CreateAsync(user, cancellationToken))
                throw new InvalidOperationException("Falha ao criar usuário.");

            return ReturnResponseSuccess();        
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao criar usuário com Email: {Email} em CreateAsync", userRequest.Email);
            throw;
        }
    }


    public async Task<Pagination<UserResponse>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var allUsers = UserMappingExtension.MapToUserResponse(await _userRepository.GetAllAsync(cancellationToken));

            var pagination = Page(allUsers, page, pageSize);
            if (pagination == null)
                throw new ArgumentException("Falha ao paginar usuários.");

            return pagination;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao buscar Todos os usuários em GetAllAsync");
            throw;
        }
    }

    public async Task<UserResponse?> GetIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetIdAsync(id, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException($"Usuário com Id: {id} não encontrado");

            return UserMappingExtension.MapToUserResponse(user);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao buscar usuário com Id: {Id} GetIdAsync", id);
            throw;
        }
    }

    public async Task<Response> UpdateAsync(UpdateUserRequest userRequest, CancellationToken cancellationToken)
    {
        try
        {
            var user = UserMappingExtension.MapToUser(userRequest);
            user.ApplyBaseModelFields(GetUserLogged(), DateHourNow(), false);
            user.ValidationRegister = false;

            _response = await ExecuteValidationResponse(_userValidator, user);
            if (_response.Error)
                throw new ArgumentException(_response.Status);

            user.PasswordHash = GeneratePasswordHash(userRequest.PasswordHash);

            if (!await _userRepository.UpdateAsync(user, cancellationToken))
                throw new InvalidOperationException("Falha ao atualizar usuário.");

            return ReturnResponseSuccess();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao atualizar usuário: {Id}.", userRequest.Id);
            throw;
        }
    }
    public async Task<Response> SoftDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetIdAsync(id, cancellationToken);
            if (user != null)
            user.DeletedOn = DateTime.UtcNow;
            user!.ModifiedOn = DateTime.UtcNow;

            if (!await _userRepository.SoftDeleteAsync(user, cancellationToken))
                throw new InvalidOperationException($"Falha ao excluir usuário com {id}");

            return ReturnResponseSuccess();
        }
        catch (Exception ex)
        {

            logger.LogError(ex, "Falha ao excluir usuário com Id: {Id} DeleteAsync", id);
            throw;
        }
    }

    #region METHODS PRIVATE
    public string GeneratePasswordHash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    #endregion
}
