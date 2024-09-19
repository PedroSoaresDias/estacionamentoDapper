using System;
using System.ComponentModel.DataAnnotations.Schema;
using estacionamentoDapper.Repositorios;

namespace estacionamentoDapper.Models;

[Table("vagas")]
public class Vaga 
{
    [IgnoreInDapper]
    public int Id { get; set; }
    public string CodigoLocalizacao { get; set; } = default!;
    public bool Ocupada { get; set; } = default!;
}