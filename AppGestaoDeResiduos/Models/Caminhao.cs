using AppGestaoDeResiduos.Models;

public class Caminhao
{
    public string? Id { get; set; } // MongoDB usa ObjectId ou string como ID
    public string? Placa { get; set; }
    public int? QtdDeColetas { get; set; }
    public int? QtdDeColetasMax { get; set; }
    public Localizacao? LocalizacaoAtual { get; set; } // Subdocumento
    public List<Coleta>? Coletas { get; set; } // Array de subdocumentos
}

public class Localizacao
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public DateTime DataHora { get; set; }
}