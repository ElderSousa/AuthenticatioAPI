namespace Application.MS_AuthenticationAutorization.Validation;

public static class ValidationMessage
{
    public static string requiredField =
        "O campo {PropertyName} é obrigatório.";  
    public static string NotFound =
        "O campo {PropertyName} não foi encontrado em nossa base de dados."; 
    public static string InvalidEmail =
        "Formato do email invalído."; 
}
