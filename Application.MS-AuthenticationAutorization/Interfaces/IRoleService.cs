using Application.MS_AuthenticationAutorization.PaginationModel;
using Application.MS_AuthenticationAutorization.Responses;
using static Application.MS_AuthenticationAutorization.Requests.RoleRequest;

namespace Application.MS_AuthenticationAutorization.Interfaces;

public interface IRoleService
{
    Task<Response> CreateAsync(CreateRoleRequest roleRequest, CancellationToken cancellationToken);
    Task<RoleResponse> GetIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Pagination<RoleResponse>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Response> UpdateAsync(UpdateRoleRequest roleRequest, CancellationToken cancellationToken);
    Task<Response> SoftDeleteAsync(Guid id, CancellationToken cancellationToken);
}
