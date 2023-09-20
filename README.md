# Cubos Challenge - Desenvolvedor C# Junior

## 🚀 Características principais

Este projeto tem como finalidade atender ao desafio para vaga de Desenvolvedor C# Jr da Cubos.
O projeto tem como base a consturção de uma API para simular transações financeiras.
Principais pontos da aplicação:

1. Cadastro de Pessoa (CPF ou CNPJ)
   1. Uma pessoa poderá ter várias contas.
2. Cadastro de cartão para conta existente
   1. Uma conta poderá ter vários cartões
   2. Apenas um cartão físico por conta e vários virtuais
3. A conta poderá realizar transações de crédito e débito.

## 🧱 Requisitos para testar a aplicação

- Possuir Docker instalado
- Postman para testar os endpoints
- Conexão com a internet
- Estar com as Portas `8081` e `5432` livres para serem usadas pela aplicação e banco de dados respectivamente

## 🎮 Executando

> Clone o repositório para sua máquina e execute os seguites passos:

- Abra um terminal no diretório do arquivo `docker-compose.yml`
- execute o comando:
  - `docker compose up -d`
- Testar endpoints utilizando `localhost:8081`

## 💻 Funcionalidades

> **IMPORTANTE: Todas as informações referente a implementação das funcionalidades do projeto estão explicadas em detalhes nos tópicos abaixo. Sendo as principais a utilização de _Validações, Paginação e Filtro._**

1. Ao cadastrar uma pessoa, deve-se informar um CNPJ ou CPF
   - Endpoint `POST /people`
   - Request:
   ```javascript
   {
       "name": "Carolina Rosa Marina Barros",
       "document": "569.679.155-76",
       "password": "senhaforte"
   }
   ```
   - Response: Status200Ok
   ```javascript
   {
        "id": "4ca336a9-b8a5-4cc6-8ef8-a0a3b5b45ed7",
        "name": "Carolina Rosa Marina Barros",
        "document": "56967915576",
        "createdAt": "2022-08-01T14:30:41.203653",
        "updatedAt": "2022-08-01T14:30:41.203653"
   }
   ```
   - **Validações**:
     - Validação de CPF e CNPJ, aceitando apenas documentos válidos.
     - Verificação de contas existentes com o documento informado.
     - _Em caso de conta existente com o documento informado a API retornará um BadRequeste(400) informando que uma conta com o documento já existe e o número do documento digitado pelo usuário._
2. Adicionar uma conta e listar contas de uma pessoa.

   - Endpoint `POST /people/:peopleId/accounts`

     | Parâmetro  | Tipo     | Descrição                     |
     | :--------- | :------- | :---------------------------- |
     | `peopleId` | `string` | **Obrigatório**. Formato uuid |

   - Request:

   ```javascript
   {
        "branch": "001",
        "accountNumber": "2033392-5"
   }
   ```

   - Response: Status200Ok

   ```javascript
   {
        "id": "b0a0ec35-6161-4ebf-bb4f-7cf73cf6448f",
        "branch": "001",
        "account": "2033392-5",
        "createdAt": "2022-08-01T14:30:41.203653",
        "updatedAt": "2022-08-01T14:30:41.203653"
   }
   ```

   - **Validações**:
     - Branch, aceitando apenas 3 digitos numéricos.
     - Account, aceitando apenas digitos numéricos e caracteres especiais (-), não aceitando letras.
   - _O preenchimento errado acarretará em um BadRequest(400) com o valor que ocasionou o erro sendo retornado para verificação do usuário._

3. Realiza a listagem da conta de uma pessoa com o Id informado.

   - Endpoint `GET /people/:peopleId/accounts`

     | Parâmetro  | Tipo     | Descrição                     |
     | :--------- | :------- | :---------------------------- |
     | `peopleId` | `string` | **Obrigatório**. Formato uuid |

   - Response: Status200Ok

   ```javascript
   [
     {
       id: "48bb7773-8ccc-4686-83f9-79581a5e5cd8",
       branch: "001",
       account: "2033392-5",
       createdAt: "2022-08-01T14:30:41.203653",
       updatedAt: "2022-08-01T14:30:41.203653",
     },
   ];
   ```

4. Adicionar cartões a uma conta.

   - Endpoint `POST /people/:peopleId/accounts`

     | Parâmetro  | Tipo     | Descrição                     |
     | :--------- | :------- | :---------------------------- |
     | `peopleId` | `string` | **Obrigatório**. Formato uuid |

   - Request:

   ```javascript
   {
        "type": "virtual",
        "number": "5179 7447 8594 6978",
        "cvv": "512"
   }
   ```

   - Response: Status200Ok

   ```javascript
   {
        "id": "b0a0ec35-6161-4ebf-bb4f-7cf73cf6448f",
        "type": "virtual",
        "number": "6978",
        "cvv": "512",
        "createdAt": "2022-08-01T14:30:41.203653",
        "updatedAt": "2022-08-01T14:30:41.203653"
   }
   ```

   - **Validações**:
     - Type. Verifica se o tipo informado é uma das opções aceitas, sendo elas `physical` ou `virtual`.
       - Em caso de valor diferente informado ou erro de digitação um BadRequest(400) é retornado para o usuário com o valor digitado.
       - Verifica se a conta de Id informado já possui um cartão físico, caso o tipo informado seja `physical`, caso haja, um BadRequest(400) é retornado com a mensagem personalizada informado ao usuário que um cartão **físico** já existe para a conta informada
     - Number. Só é aceito uma sequencia numérica de 16 dígitos. Espaços são aceitos.
     - Cvv. Só é aceito uma sequencia numérica de 3 dígitos.
     - Uma response do tipo BadRequest irá ser retornada caso alguma das restrições acima não sejam atendidas. O valor informado pelo usuário será retornado.

5. Realiza a listagem dos cartões registrados em uma determinada conta.

   - Endpoint `GET /accounts/:accountId/cards`

     | Parâmetro   | Tipo     | Descrição                     |
     | :---------- | :------- | :---------------------------- |
     | `accountId` | `string` | **Obrigatório**. Formato uuid |

   - Response: Status200Ok

   ```javascript
     {
        "id": "48bb7773-8ccc-4686-83f9-79581a5e5cd8",
        "branch": "001",
        "account": "2033392-5",
        "cards": [
                    {
                      "id": "05a0ab2d-5ece-45b6-b7d3-f3ecce2713d5",
                      "type": "physical",
                      "number": "0423",
                      "cvv": "231",
                      "createdAt": "2022-08-01T14:30:41.203653",
                      "updatedAt": "2022-08-01T14:30:41.203653"
                    },
                    {
                      "id": "b0a0ec35-6161-4ebf-bb4f-7cf73cf6448f",
                      "type": "virtual",
                      "number": "2145",
                      "cvv": "512",
                      "createdAt": "2022-08-01T14:30:41.203653",
                      "updatedAt": "2022-08-01T14:30:41.203653"
                    }
                ],
        "createdAt": "2022-08-01T14:30:41.203653",
        "updatedAt": "2022-08-01T14:30:41.203653"
     }
   ```

6. Realiza a listagem de todos os cartões registrados em nome de uma mesma pessoa, independente de conta.

   - Endpoint `GET /people/:peopleId/cards`

     | Parâmetro  | Tipo     | Descrição                     |
     | :--------- | :------- | :---------------------------- |
     | `peopleId` | `string` | **Obrigatório**. Formato uuid |

   - Response: Status200Ok

   ```javascript
     {
      "cards": [
        {
          "id": "05a0ab2d-5ece-45b6-b7d3-f3ecce2713d5",
          "type": "physical",
          "number": "0423",
          "cvv": "231",
          "createdAt": "2022-08-01T14:30:41.203653",
          "updatedAt": "2022-08-01T14:30:41.203653"
        },
        {
          "id": "b0a0ec35-6161-4ebf-bb4f-7cf73cf6448f",
          "type": "virtual",
          "number": "6978",
          "cvv": "512",
          "createdAt": "2022-08-01T14:30:41.203653",
          "updatedAt": "2022-08-01T14:30:41.203653"
        }
     ],
    "pagination": {
        "itemsPerPage": 5,
        "currentPage": 1
    }
   }
   ```

   - **Paginação**:

     - A paginação poderá ser executada através de query params, porém por padrão, caso o valor não seja informado, a paginação é executada considerando a Pagina 1, com os 5 primeiro elementos sendo retornados.
     - Para diferente valores executar chamada no endpoint da seguinte forma:

       - `GET /people/:peopleId/cards?ItemsPerPage=5&CurrentPage=2`

       | Parâmetro      | Tipo     | Descrição                     |
       | :------------- | :------- | :---------------------------- |
       | `peopleId`     | `string` | **Obrigatório**. Formato uuid |
       | `ItemsPerPage` | `int`    | **Não obrigatório**.          |
       | `CurrentPage`  | `int`    | **Não obrigatório**.          |

       - Altere os valores conforme desejado depois do `=`.

7. Realiza a criação de uma transação em uma determinada conta

   - Endpoint `POST /accounts/:accountId/transactions`
     | Parâmetro | Tipo | Descrição |
     | :-------- | :------- | :------------------------- |
     | `accountId` | `string` | **Obrigatório**. Formato uuid |

   - Request:

   ```javascript
   {
        "value": 100.00,
        "description": "Venda do cimento para Clodson"
   }
   ```

   - Response: Status200Ok

   ```javascript
   {
        "id": "05a0ab2d-5ece-45b6-b7d3-f3ecce2713d5",
        "value": 100.00,
        "description": "Venda do cimento para Lucas",
        "createdAt": "2022-08-01T14:30:41.203653",
        "updatedAt": "2022-08-01T14:30:41.203653"
   }
   ```

   - **Validações**:
     - O Valor informado permite casas decimais e valores negativos.
       - As transações de `Débito` ou `Crédito` serão identificadas através do valor informado (valores positivos ou negativos)
       - Antes do registro da transação, a checagem de saldo é feita na conta do usuário, permitindo transações desde que no resultado final o saldo da conta permaneça positivo.
     - Em caso de transação de **Débito** com valor superior ao saldo uma response do tipo BadRequest(400) é retornada informando o usuário que a operação não pode ser concluída.

8. Realiza a listagem de todas as transações de uma conta, podendo ser mostrada ao usuário de forma paginada através de query params.

   - Endpoint `GET /accounts/:accountId/transactions`

     | Parâmetro   | Tipo     | Descrição                     |
     | :---------- | :------- | :---------------------------- |
     | `accountId` | `string` | **Obrigatório**. Formato uuid |

   - Response: Status200Ok

   ```javascript
     {
      "transactions": [
        {
          "id": "05a0ab2d-5ece-45b6-b7d3-f3ecce2713d5",
          "value": 100.00,
          "description": "Venda do cimento para Lucas.",
          "createdAt": "2022-08-01T14:30:41.203653",
          "updatedAt": "2022-08-01T14:30:41.203653"

        }
     ],
    "pagination": {
        "itemsPerPage": 5,
        "currentPage": 1
    }
   }
   ```

   - **Paginação**:

     - A paginação poderá ser executada através de query params, porém por padrão, caso o valor não seja informado, a paginação é executada considerando a Pagina 1, com os 5 primeiro elementos sendo retornados.
     - Para diferente valores executar chamada no endpoint da seguinte forma:

       - `GET /accounts/:accountId/transactions?ItemsPerPage=5&CurrentPage=2`

       | Parâmetro      | Tipo     | Descrição                     |
       | :------------- | :------- | :---------------------------- |
       | `accountId`    | `string` | **Obrigatório**. Formato uuid |
       | `ItemsPerPage` | `int`    | **Não obrigatório**.          |
       | `CurrentPage`  | `int`    | **Não obrigatório**.          |

       - Altere os valores conforme desejado depois do `=`.

   - **Filtro**:

     - O Filtro para seleção de transações por data também pode ser executado por query params.
     - Para diferente valores executar chamada no endpoint da seguinte forma:

       - `GET /accounts/:accountId/transactions?ItemsPerPage=5&CurrentPage=2&TransactionDate=2023-09-19T14:19:46`

       | Parâmetro         | Tipo       | Descrição                     |
       | :---------------- | :--------- | :---------------------------- |
       | `accountId`       | `string`   | **Obrigatório**. Formato uuid |
       | `ItemsPerPage`    | `int`      | **Não obrigatório**.          |
       | `CurrentPage`     | `int`      | **Não obrigatório**.          |
       | `TransactionDate` | `DateTime` | **Não obrigatório**.          |

       - O valor utilizado para data está no formato DateTime.
       - Altere os valores conforme desejado depois do `=`.

9. Retorna o saldo da conta solicitada.

   - Endpoint `GET /accounts/:accountId/balance`

     | Parâmetro   | Tipo     | Descrição                     |
     | :---------- | :------- | :---------------------------- |
     | `accountId` | `string` | **Obrigatório**. Formato uuid |

   - Response: Status200Ok

   ```javascript
     {
        "balance": 100.00
     }
   ```

10. Reverte uma transação. Se a operação foi de crédito, será realizada uma nova transação de Débito no mesmo valor da transação informada. O mesmo sendo executado para o cernário inverso.

- Endpoint `POST /accounts/:accountId/transactions/:transactionId/revert`

  | Parâmetro   | Tipo     | Descrição                     |
  | :---------- | :------- | :---------------------------- |
  | `accountId` | `string` | **Obrigatório**. Formato uuid |

- Request:

```javascript
    "Texto de descrição enviado no body da requisição"
```

- Response: Status200Ok

```javascript
{
     "id": "092ec73f-d7c3-4afb-bac0-9c7e8eb33eae",
     "value": 100.00,
     "description": "Estorno por devolução de produto",
     "createdAt": "2022-08-01T14:30:41.203653",
     "updatedAt": "2022-08-01T14:30:41.203653"

}
```

- **Validações**:
  - Verifica se a conta e transação informada existem.
    - A reversão da operação é feita de forma automátiva. Se o valor da transação registrada a ser revertida é positivo (**Crédito**), a nova operação será feita com o mesmo valor, na operação **Débito** sem a necessidade da interação do usuário para informar o novo valor.

## 🛠️ Construído com

- [ASP.NET Core 7.0](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0)
- [EntityFramework Core](https://learn.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org/)
- [PostgreSQL](https://www.postgresql.org/)
- [Docker](https://www.docker.com/)

## 📄 Licença

Este projeto está sob a licença MIT - veja o arquivo [LICENSE.md](https://github.com/Holiv/CubosChallenge/blob/main/LICENSE) para detalhes.
