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
}