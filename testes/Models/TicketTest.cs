using System;
using estacionamentoDapper.Models;

[TestClass]
public class TicketTest
{
    [TestMethod]
    public void TestandoPropriedadesDoModelTicket()
    {
        // Arrange
        var ticket = new Ticket();

        // Act
        ticket.Id = 1;
        ticket.Valor = 10.00f;
        ticket.VeiculoId = 1;
        ticket.VagaId = 1;

        // Assert
        Assert.AreEqual(1, ticket.Id);
        Assert.AreEqual(10.00f, ticket.Valor);
        Assert.AreEqual(1, ticket.VeiculoId);
        Assert.AreEqual(1, ticket.VagaId);
    }

    [TestMethod]
    public void TestandoMetodoValorTotal()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, Nome = "Pedro" };
        var veiculo = new Veiculo { Id = 1, Marca = "Volkswagen", Modelo = "Virtus", ClienteId = cliente.Id, Placa = "MIT5895" };
        var ticket = new Ticket { DataEntrada = DateTime.Now.AddHours(-1), Id = 1, VagaId = 1, VeiculoId = veiculo.Id };
        var valorDoMinuto = new ValorDoMinuto { Minutos = 1, Valor = 1 };

        // Act
        var valorTotal = ticket.ValorTotal(valorDoMinuto);

        // Assert
        Assert.AreEqual(60, valorTotal);
    }

    [TestMethod]
    public void TestandoValorPagoDoTicket()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, Nome = "Pedro" };
        var veiculo = new Veiculo { Id = 1, Marca = "Volkswagen", Modelo = "Virtus", ClienteId = cliente.Id, Placa = "MIT5895" };
        var ticket = new Ticket { DataEntrada = DateTime.Now.AddHours(-1), Id = 1, VagaId = 1, VeiculoId = veiculo.Id };
        var valorDoMinuto = new ValorDoMinuto { Minutos = 1, Valor = 1 };
        var valorTotalDesejado = ticket.ValorTotal(valorDoMinuto);

        // Act
        ticket.Pago(valorDoMinuto);

        // Assert
        Assert.AreEqual(valorTotalDesejado, ticket.Valor);
        Assert.IsNotNull(ticket.DataSaida);
    }
}