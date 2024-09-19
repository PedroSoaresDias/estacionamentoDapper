using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using estacionamentoDapper.Models;
using Dapper;
using estacionamentoDapper.Repositorios;

namespace estacionamentoDapper.Controllers;

[Route("/vagas")]
public class VagasController : Controller
{
    private readonly IRepositorio<Vaga> _repo;

    public VagasController(IRepositorio<Vaga> repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        var vagas = _repo.ObterTodos();
        return View(vagas);
    }

    [HttpGet("novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("Criar")]
    public IActionResult Criar([FromForm] Vaga vaga)
    {
        _repo.Inserir(vaga);

        return Redirect("/vagas");
    }

    [HttpPost("{id}/apagar")]
    public IActionResult Apagar([FromRoute] int id)
    {
        _repo.Excluir(id);

        return Redirect("/vagas");
    }

    [HttpGet("{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        var vaga = _repo.ObterPorId(id);
        return View(vaga);
    }

    [HttpPost("{id}/alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] Vaga vaga)
    {
        vaga.Id = id;
        _repo.Atualizar(vaga);

        return Redirect("/vagas");
    }
}
