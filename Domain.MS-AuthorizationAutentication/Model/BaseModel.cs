namespace Domain.MS_AuthorizationAutentication.Model;

public class BaseModel
{
    public Guid Id { get; set; }
    public Guid CriadoPor { get; set; }
    public Guid ModificadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime ModificadoEm { get; set; }
    public DateTime ExcluidoEm { get; set; }

    public void AdicionarBaseModel(Guid usuarioId, DateTime dataHora, bool cadastrar)
    {
        if (cadastrar)
        {
            Id = Guid.NewGuid();
            CriadoPor = usuarioId;
            CriadoEm = dataHora;
        }
        else
        {
            ModificadoPor = usuarioId;
            ModificadoEm = dataHora;
        }

    }
}
