using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using estacionamentoDapper.Models;
using Dapper;
using estacionamentoDapper.Repositorios;

namespace estacionamentoDapper.Controllers;

[Route("/clientes")]
public class ClientesController : Controller
{
    private readonly IRepositorio<Cliente> _repo;

    public ClientesController(IRepositorio<Cliente> repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        var clientes = _repo.ObterTodos();
        return View(clientes);
    }

    [HttpGet("novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("Criar")]
    public IActionResult Criar([FromForm] Cliente cliente)
    {
        _repo.Inserir(cliente);

        return Redirect("/clientes");
    }

    [HttpPost("{id}/apagar")]
    public IActionResult Apagar([FromRoute] int id)
    {
        _repo.Excluir(id);

        return Redirect("/clientes");
    }

    [HttpGet("{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        var cliente = _repo.ObterPorId(id);
        return View(cliente);
    }

    [HttpPost("{id}/alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] Cliente cliente)
    {
        cliente.Id = id;
        _repo.Atualizar(cliente);

        return Redirect("/clientes");
    }
}
