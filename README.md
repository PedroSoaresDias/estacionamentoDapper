# Sistema de estacionamento com ASP.NET MVC com Dapper

Esse projeto foi desenvolvido na seção de projetos da DIO (Digital Innovation One), que é um sistema de estacionamento que contém: (Clientes, Veiculos, Vagas, Tickets).

## Como foi desenvolvido

No desenvolvimento, foi utilizado as seguintes tecnologias:

- .NET 8
- ASP.NET MVC
- Dapper
- MySQL (Com container Docker)
- MSTest (Para testes unitários)

## Como foi o Back-end da aplicação

A parte do back-end foi baseada na arquitetura MVC (Model-View-Controller), a Model foi utilizada para representar as entidades do banco de dados MySQL e fazendo a comunicação entre o código e o banco de dados utilizando o Dapper, na parte do Controller é onde faz as requisições da aplicação e as operações CRUD com o Dapper com os seus script em SQL para realizar cada operação.

## Como foi o Front-end da aplicação

A parte do front-end também foi baseada na arquitetura MVC, mais especificamente a View, onde o usuário consegue ver e interagir com a aplicação, nessa parte foi feita com Razor (Interpolação do C# dentro do HTML) e com o Bootstrap para as estilizações básicas.
