
using estacionamentoDapper.Models;

[TestClass]
public class ValorDoMinutoTest 
{
    [TestMethod]
    public void TestandoPropriedadesDoModelValorDoMinuto()
    {
        // Arrange
        var valorDoMinuto = new ValorDoMinuto();

        // Act
        valorDoMinuto.Id = 1;
        valorDoMinuto.Minutos = 60;
        valorDoMinuto.Valor = 10.50f;

        // Assert
        Assert.AreEqual(1, valorDoMinuto.Id);
        Assert.AreEqual(60, valorDoMinuto.Minutos);
        Assert.AreEqual(10.50f, valorDoMinuto.Valor);
    }
}