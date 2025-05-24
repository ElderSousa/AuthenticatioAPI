using Application.MS_AuthenticationAutorization.Responses;
using Domain.MS_AuthorizationAutentication.Entities;

namespace Application.MS_AuthenticationAutorization.MapperExtension
{
    public static class RefreshTokenMapping
    {
        public static RefreshTokenResponse MapToRefreshTokeResponse(this RefreshToken refreshToken)
        {
            return new RefreshTokenResponse
            {
                Id = refreshToken.Id,
                UserId = refreshToken.UserId,
                Token =refreshToken.Token,
                Expiration = refreshToken.Expiration,
                IsRevoked = refreshToken.IsRevoked,
                CreatedBy = refreshToken.CreatedBy,
                CreatedOn = refreshToken.CreatedOn,
                ModifiedBy = refreshToken.ModifiedBy,
                ModifiedOn = refreshToken.ModifiedOn
            };
        }
    }
}
