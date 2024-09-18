using System;

namespace estacionamentoDapper.Models;

public class Valores 
{
    public int Id { get; set; } = default!;
    public int Minutos { get; set; } = default!;
    public float ValorUnitario { get; set; } = default!;
}