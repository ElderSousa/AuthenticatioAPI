namespace Infrastructure.MS_AuthenticationAutorization.Scripts;

public class UserScript
{
    internal static readonly string UserExist =
        @"
            SELECT 
                COUNT(*)
            FROM
                users
            WHERE 
                Id = @Id
                AND ExcluidoEm IS NULL
        ";
}
