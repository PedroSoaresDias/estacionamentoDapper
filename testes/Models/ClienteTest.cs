
using estacionamentoDapper.Models;

[TestClass]
public class ClienteTest 
{
    [TestMethod]
    public void TestandoPropriedadesDoModelCliente()
    {
        // Arrange
        var cliente = new Cliente();

        // Act
        cliente.Id = 1;
        cliente.Nome = "Pedro";
        cliente.Cpf = "000.000.000-01";

        // Assert
        Assert.AreEqual(1, cliente.Id);
        Assert.AreEqual("Pedro", cliente.Nome);
        Assert.AreEqual("000.000.000-01", cliente.Cpf);
    }
}