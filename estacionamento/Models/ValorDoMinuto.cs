using System;
using System.ComponentModel.DataAnnotations.Schema;
using estacionamentoDapper.Repositorios;

namespace estacionamentoDapper.Models;

[Table("valores")]
public class ValorDoMinuto
{
    [IgnoreInDapper]
    public int Id { get; set; } = default!;
    [Column("minutos")]
    public int Minutos { get; set; } = default!;
    public float Valor { get; set; } = default!;
}