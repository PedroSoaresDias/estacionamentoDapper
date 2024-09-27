
using estacionamentoDapper.Models;

[TestClass]
public class VagaTest 
{
    [TestMethod]
    public void TestandoPropriedadesDoModelVaga()
    {
        // Arrange
        var vaga = new Vaga();

        // Act
        vaga.Id = 1;
        vaga.CodigoLocalizacao = "VagaA1";
        vaga.Ocupado = true;

        // Assert
        Assert.AreEqual(1, vaga.Id);
        Assert.AreEqual("VagaA1", vaga.CodigoLocalizacao);
        Assert.AreEqual(true, vaga.Ocupado);
    }
}