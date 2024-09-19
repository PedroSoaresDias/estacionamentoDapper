using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using estacionamentoDapper.Models;
using Dapper;
using estacionamentoDapper.Repositorios;

namespace estacionamentoDapper.Controllers;

[Route("/valores")]
public class ValorDoMinutoController : Controller
{
    private readonly IRepositorio<ValorDoMinuto> _repo;

    public ValorDoMinutoController(IRepositorio<ValorDoMinuto> repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        var valores = _repo.ObterTodos();
        return View(valores);
    }

    [HttpGet("novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("Criar")]
    public IActionResult Criar([FromForm] ValorDoMinuto valorDoMinuto)
    {
        _repo.Inserir(valorDoMinuto);

        return Redirect("/valores");
    }

    [HttpPost("{id}/apagar")]
    public IActionResult Apagar([FromRoute] int id)
    {
        _repo.Excluir(id);

        return Redirect("/valores");
    }

    [HttpGet("{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        var valor = _repo.ObterPorId(id);
        return View(valor);
    }

    [HttpPost("{id}/alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] ValorDoMinuto valorDoMinuto)
    {
        valorDoMinuto.Id = id;
        _repo.Atualizar(valorDoMinuto);

        return Redirect("/valores");
    }
}
