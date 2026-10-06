# 📦 API de Catálogo de Produtos

Uma API RESTful desenvolvida em ASP.NET Core (.NET 10) para o gerenciamento de produtos. Este projeto foi construído como base de estudos em C#, com foco na implementação rigorosa dos fundamentos de operações CRUD utilizando armazenamento temporário em memória.

## 🚀 Tecnologias Utilizadas
- **C#**
- **.NET 10**
- **Swagger / OpenAPI** (Documentação e testes de interface)

## ⚙️ Funcionalidades (Endpoints)
A API expõe os seguintes endpoints para manipulação do catálogo:

| Verbo HTTP | Rota | Descrição |
| :--- | :--- | :--- |
| `GET` | `/api/produtos` | Retorna a lista de todos os produtos cadastrados. |
| `GET` | `/api/produtos/{id}` | Busca os detalhes de um produto específico através do ID. |
| `POST` | `/api/produtos` | Cadastra um novo produto gerando um ID automaticamente. |
| `PUT` | `/api/produtos/{id}` | Atualiza integralmente os dados de um produto existente. |
| `DELETE` | `/api/produtos/{id}` | Remove permanentemente um produto do catálogo. |

## 💻 Como Executar Localmente
1. Clone este repositório em sua máquina local.
2. Abra o arquivo da Solução (`.sln`) no Visual Studio.
3. Certifique-se de que o SDK do .NET 10 está instalado.
4. Pressione `F5` para iniciar a compilação e a depuração.
5. O navegador abrirá automaticamente na interface interativa do **Swagger**, permitindo testar todos os endpoints.