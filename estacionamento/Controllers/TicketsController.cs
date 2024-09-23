using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using estacionamentoDapper.Models;
using Dapper;
using estacionamentoDapper.Repositorios;
using Microsoft.AspNetCore.Mvc.Rendering;
using estacionamentoDapper.DTO;

namespace estacionamentoDapper.Controllers;

[Route("/tickets")]
public class TicketsController : Controller
{
    private readonly IDbConnection _connection;
    private readonly IRepositorio<Ticket> _repo;

    public TicketsController(IRepositorio<Ticket> repo, IDbConnection connection)
    {
        _repo = repo;
        _connection = connection;
    }

    public IActionResult Index()
    {
        var sql = """"
            SELECT t.*, v.*, c.*, vg.* FROM tickets t
            INNER JOIN veiculos v ON v.id = t.veiculoId
            INNER JOIN clientes c ON c.id = v.clienteId
            INNER JOIN vagas vg ON vg.id = t.vagaId
        """";

        var tickets = _connection.Query<Ticket, Veiculo, Cliente, Vaga, Ticket>(sql, (ticket, veiculo, cliente, vaga) =>
        {
            ticket.Veiculo = veiculo;
            veiculo.Cliente = cliente;
            ticket.Vaga = vaga;
            return ticket;
        }, splitOn: "Id, Id, Id");
        return View(tickets);
    }

    [HttpGet("novo")]
    public IActionResult Novo()
    {
        preencheVagasViewBag();

        return View();
    }

    [HttpPost("Criar")]
    public IActionResult Criar([FromForm] TicketDTO ticketDTO)
    {
        Cliente cliente = buscarOuCadastrarClientePorDTO(ticketDTO);
        Veiculo veiculo = buscarOuCadastrarVeiculoPorDTO(ticketDTO, cliente);

        var ticket = new Ticket();
        ticket.VeiculoId = veiculo.Id;
        ticket.DataEntrada = DateTime.Now;
        ticket.VagaId = ticketDTO.VagaId;

        _repo.Inserir(ticket);

        alterarStatusVaga(ticket.VagaId, true);

        return Redirect("/tickets");
    }

    [HttpPost("{id}/apagar")]
    public IActionResult Apagar([FromRoute] int id)
    {
        var ticket = _repo.ObterPorId(id);
        alterarStatusVaga(ticket.VagaId, false);
        _repo.Excluir(id);
        return Redirect("/tickets");
    }

    private void preencheVagasViewBag()
    {
        var sql = """"
            SELECT * FROM vagas
            WHERE Ocupado = false
        """";

        var vagas = _connection.Query<Vaga>(sql);

        ViewBag.Vagas = new SelectList(vagas, "Id", "CodigoLocalizacao");
    }

    private Cliente buscarOuCadastrarClientePorDTO(TicketDTO ticketDTO)
    {
        Cliente? cliente = null;

        if (!string.IsNullOrEmpty(ticketDTO.Cpf))
        {
            var query = "SELECT * FROM clientes WHERE Cpf = @Cpf";

            cliente = _connection.QueryFirstOrDefault<Cliente>(query, new Cliente { Cpf = ticketDTO.Cpf });
        }

        if (cliente != null) return cliente;

        cliente = new Cliente();
        cliente.Nome = ticketDTO.Nome;
        cliente.Cpf = ticketDTO.Cpf;

        var sql = $"INSERT INTO clientes (Nome, CPF) VALUES (@Nome, @Cpf); SELECT LAST_INSERT_ID()";
        cliente.Id = _connection.QuerySingle<int>(sql, cliente);

        return cliente;
    }

    private Veiculo buscarOuCadastrarVeiculoPorDTO(TicketDTO ticketDTO, Cliente cliente)
    {
        Veiculo? veiculo = null;

        if (!string.IsNullOrEmpty(ticketDTO.Placa))
        {
            var query = "SELECT * FROM veiculos WHERE Placa = @Placa AND ClienteId = @ClienteId";

            veiculo = _connection.QueryFirstOrDefault<Veiculo>(query, new Veiculo { Placa = ticketDTO.Placa, ClienteId = cliente.Id });
        }

        if (veiculo != null) return veiculo;

        veiculo = new Veiculo();
        veiculo.Placa = ticketDTO.Placa;
        veiculo.Marca = ticketDTO.Marca;
        veiculo.Modelo = ticketDTO.Modelo;
        veiculo.ClienteId = cliente.Id;

        var sql = $"INSERT INTO veiculos (Placa, Marca, Modelo, ClienteId) VALUES (@Placa, @Marca, @Modelo, @ClienteId); SELECT LAST_INSERT_ID()";
        veiculo.Id = _connection.QuerySingle<int>(sql, veiculo);

        return veiculo;
    }

    private void alterarStatusVaga(int vagaId, bool ocupado)
    {
        var sql = $"UPDATE vagas SET ocupado = @Ocupado  WHERE id = @id";
        _connection.Execute(sql, new { id = vagaId, Ocupado = ocupado });
    }
}