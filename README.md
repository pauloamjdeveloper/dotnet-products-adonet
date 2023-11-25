<h1 align="center">Products ADO.NET</h1>

<p align="center">
  <a href="https://learn.microsoft.com/pt-br/dotnet/"><img alt="DotNet 6" src="https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white&style=for-the-badge" /></a>
  <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/"><img alt="C#" src="https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white&style=for-the-badge" /></a>
  <a href="https://www.microsoft.com/pt-br/sql-server/sql-server-downloads"><img alt="SQL Server" src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" /></a>
</p>

## :computer: Projeto

Repositório com projeto desenvolvido para fins acadêmicos para representar uma WebAPI, o objetivo é simular uma lista de `Produtos` e aplicar um CRUD básico para revisar conceitos referentes a comunicação com banco de dados usando 
[ADO.NET](https://learn.microsoft.com/pt-br/dotnet/framework/data/adonet/ado-net-code-examples) de uma mais organizada.

## :white_check_mark: Decisões Técnicas

Foram tomadas algumas decisões técnicas durante o desenvolvimento da aplicação, mesmo criando um projeto simples o propósito foi aplicar boas práticas visando código com qualidade e uma estrutura organizada caso o projeto recebar futuras modicações.

- `IConnectionDataBase`: essa interface genérica foi criada para realizar a comunicação com o banco de dados, nesse projeto foi utilizado o [SQL Server 2019](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) caso um dia for necessário alterar para outra base de dados essa estrutura vai permitir essa mudança de uma maneira mais flexível.

- `Repository Pattern`: este padrão tem a responsabilidade de centralizar a lógica de acesso aos dados, seguindo uma ideia similar a da interface `IConnectionDataBase` a adição desse padrão tem objetivo de aplicar uma estrutura mais flexível para qualquer mudança necessária.

- `Service Pattern`: esse padrão foi adicionado para separar a camada de regra de negócio das demais camadas do projeto, assim dispõe de flexibilidade para a criação de testes, manutenção de código e na reutilização por outras partes do projeto. 

## :heavy_check_mark: Recursos Utilizados

- ``.NET 6``
- ``ASP.NET``
- ``C#``
- ``ADO.NET``
- ``SQL Server``
- ``Swagger``

## :floppy_disk: Clonar Repositório

```bash
git clone https://github.com/pauloamjdeveloper/dotnet-products-adonet.git
```

## :boy: Author

<a href="https://github.com/pauloamjdeveloper"><img src="https://avatars.githubusercontent.com/u/137198048?v=4" width=70></a>
[Paulo Alves](https://github.com/pauloamjdeveloper)

