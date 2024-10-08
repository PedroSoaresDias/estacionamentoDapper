using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using estacionamentoDapper.Models;
using Dapper;
using estacionamentoDapper.Repositorios;

namespace estacionamentoDapper.Controllers;

[Route("/veiculos")]
public class VeiculosController : Controller
{

    private readonly IDbConnection _connection;
    private readonly IRepositorio<Veiculo> _repo;

    public VeiculosController(IRepositorio<Veiculo> repo, IDbConnection connection)
    {
        _repo = repo;
        _connection = connection;
    }

    public IActionResult Index()
    {
        var sql = """"
            SELECT v.*, c.* FROM veiculos v
            INNER JOIN clientes c ON c.id = v.clienteId
        """";

        var veiculos = _connection.Query<Veiculo, Cliente, Veiculo>(sql, (veiculo, cliente) => {
            veiculo.Cliente = cliente;
            return veiculo;
        }, splitOn: "Id");
        return View(veiculos);
    }

    [HttpGet("novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("Criar")]
    public IActionResult Criar([FromForm] Veiculo veiculo)
    {
        _repo.Inserir(veiculo);

        return Redirect("/veiculos");
    }

    [HttpPost("{id}/apagar")]
    public IActionResult Apagar([FromRoute] int id)
    {
        _repo.Excluir(id);

        return Redirect("/veiculos");
    }

    [HttpGet("{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        var veiculo = _repo.ObterPorId(id);
        return View(veiculo);
    }

    [HttpPost("{id}/alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] Veiculo veiculo)
    {
        veiculo.Id = id;
        _repo.Atualizar(veiculo);

        return Redirect("/veiculos");
    }
}
