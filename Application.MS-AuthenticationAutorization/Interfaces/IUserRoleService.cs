using Application.MS_AuthenticationAutorization.PaginationModel;
using Application.MS_AuthenticationAutorization.Responses;
using static Application.MS_AuthenticationAutorization.Requests.UserRoleRequest;

namespace Application.MS_AuthenticationAutorization.Interfaces;

public interface IUserRoleService
{
    Task<Response> CreateAsync(CreateUserRoleRequest userRoleRequest, CancellationToken cancellationToken);
    Task<UserRoleResponse?> GetIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Pagination<UserRoleResponse>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Response> UpdateAsync(UpdateUserRoleRequest userRoleRequest, CancellationToken cancellationToken);
    Task<Response> SoftDeleteAsync(Guid id, CancellationToken cancellationToken);
}
