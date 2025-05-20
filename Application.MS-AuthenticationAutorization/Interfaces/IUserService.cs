using Application.MS_AuthenticationAutorization.PaginationModel;
using Application.MS_AuthenticationAutorization.Responses;
using static Application.MS_AuthenticationAutorization.Requests.UserRequest;

namespace Application.MS_AuthenticationAutorization.Interfaces;

public interface IUserService
{
    Task<Response> CreateAsync(CreateUserRequest userRequest, CancellationToken cancellationToken);
    Task<UserResponse?> GetIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Pagination<UserResponse>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Response> UpdateAsync(UpdateUserRequest userRequest, CancellationToken cancellationToken);
    Task<Response> SoftDeleteAsync(Guid id, CancellationToken cancellationToken);
}
