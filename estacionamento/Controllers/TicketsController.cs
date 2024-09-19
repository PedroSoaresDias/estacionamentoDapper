using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using estacionamentoDapper.Models;
using Dapper;
using estacionamentoDapper.Repositorios;

namespace estacionamentoDapper.Controllers;

[Route("/tickets")]
public class TicketsController : Controller
{
    private readonly IRepositorio<Ticket> _repo;

    public TicketsController(IRepositorio<Ticket> repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        var tickets = _repo.ObterTodos();
        return View(tickets);
    }

    [HttpGet("novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("Criar")]
    public IActionResult Criar([FromForm] Ticket ticket)
    {
        _repo.Inserir(ticket);

        return Redirect("/tickets");
    }

    [HttpPost("{id}/apagar")]
    public IActionResult Apagar([FromRoute] int id)
    {
        _repo.Excluir(id);

        return Redirect("/tickets");
    }

    [HttpGet("{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        var ticket = _repo.ObterPorId(id);
        return View(ticket);
    }

    [HttpPost("{id}/alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] Ticket ticket)
    {
        ticket.Id = id;
        _repo.Atualizar(ticket);

        return Redirect("/tickets");
    }
}
