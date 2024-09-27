using System;
using System.ComponentModel.DataAnnotations.Schema;
using estacionamentoDapper.Repositorios;

namespace estacionamentoDapper.Models;

[Table("tickets")]
public class Ticket 
{
    public int Id { get; set; } = default!;
    public DateTime DataEntrada { get; set; } = default!;
    public DateTime? DataSaida { get; set; }
    public float Valor { get; set; } = default!;
    public int VeiculoId { get; set; } = default!;
    [IgnoreInDapper]
    public Veiculo Veiculo { get; set; } = default!;
    public int VagaId { get; set; } = default!;
    [IgnoreInDapper]
    public Vaga Vaga { get; set; } = default!;

    public float ValorTotal(ValorDoMinuto valorDoMinuto)
    {
        if (this.DataSaida != null) return this.Valor;
    
        var valorMinuto = valorDoMinuto.Valor / valorDoMinuto.Minutos;

        TimeSpan diferenca = DateTime.Now - this.DataEntrada;
        int minutos = (int)diferenca.TotalMinutes;

        return minutos * valorMinuto;
    }

    public void Pago(ValorDoMinuto valorDoMinuto)
    {
        this.Valor = this.ValorTotal(valorDoMinuto);
        this.DataSaida = DateTime.Now;
    }
}