
using estacionamentoDapper.Models;

[TestClass]
public class VeiculoTest
{
    [TestMethod]
    public void TestandoPropriedadesDoModelVeiculo()
    {
        // Arrange
        var veiculo = new Veiculo();

        // Act
        veiculo.Id = 1;
        veiculo.Placa = "MIT5895";
        veiculo.Modelo = "Virtus";
        veiculo.Marca = "Volkswagen";
        veiculo.ClienteId = 1;

        // Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("MIT5895", veiculo.Placa);
        Assert.AreEqual("Virtus", veiculo.Modelo);
        Assert.AreEqual("Volkswagen", veiculo.Marca);
        Assert.AreEqual(1, veiculo.ClienteId);
    }
}