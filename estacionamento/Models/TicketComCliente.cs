using System;

namespace estacionamentoDapper.Models;

public class TicketComCliente
{
    public Ticket Ticket { get; set; } = default!;
    public string NomeCliente { get; set; } = default!;
}