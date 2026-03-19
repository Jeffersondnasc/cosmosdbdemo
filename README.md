# cosmosdbdemo

Este projeto demonstra o uso do Azure Cosmos DB com ASP.NET Core.

## Funcionalidades
- API REST para operações CRUD
- Swagger UI para documentação e testes

## Como executar
1. Instale o .NET SDK (versão compatível com o projeto)
2. Configure a string de conexão do Cosmos DB em `appsettings.json` (não compartilhe publicamente)
3. Execute o projeto:
   ```bash
   dotnet run
   ```
4. Acesse o Swagger UI em: [http://localhost:5080/swagger/index.html](http://localhost:5080/swagger/index.html)

## Estrutura
- `Controllers/`: Controllers da API
- `Models/`: Modelos de dados
- `appsettings.json`: Configurações do projeto

## Observações
- A string de conexão do Cosmos DB está oculta neste repositório por motivos de segurança.
- Para rodar localmente, insira sua string de conexão em `appsettings.json`.

## Repositório
[https://github.com/Jeffersondnasc/cosmosdbdemo](https://github.com/Jeffersondnasc/cosmosdbdemo)

## Branch
Os commits devem ser enviados para a branch `dev`.
