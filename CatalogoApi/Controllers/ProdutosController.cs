// Using avisa ao C# para "importar" as ferramentas que serão usadas nesse arquivo
// CatalogoApi.Models permite enxergar a classe Produto que está em Models
using CatalogoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controllers
{
    // [ApiController] é um atributo que indica que essa classe é um Controller de API
    // [ApiController] é um atributo que foi feito para responder requisições de API na web. 
    [ApiController]
    // [Route("api/[controller]")] Define o endereço (URL) que será usado para acessar esse Controller. 
    [Route("api/[controller]")]

    // : ControllerBase: o símbolo ":" significa Herança
    public class ProdutosController : ControllerBase
    {
        private static List<Produto> _produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Mouse Gamer", Preco = 120.50m},
            new Produto { Id = 2, Nome = "Teclao Mecânico", Preco = 250.00m}
        };
        // [HttpGet] é um atributo que indica que esse método responde a requisições HTTP GET
        [HttpGet]
        // IActionResult é um tipo de retorno que representa uma resposta HTTP.
        public IActionResult GetProdutos()
        {
            // Ok() é um método que retorna uma resposta HTTP 200 (Sucesso) com os dados fornecidos.
            return Ok(_produtos);
        }

        // [HttpPost]: É o atributo de roteamento. Ele avisa o .NET: "Se chegar uma requisição na
        // URL /api/produtos e for do tipo POST, execute este método".
        [HttpPost]

        // [FromBody]: Um atributo de Model Binding (Vinculação de Modelo).
        // Ele diz para a API não procurar os dados do produto na URL, mas sim abrir o
        // "envelope" (Corpo) da requisição, pegar o texto JSON que está lá dentro e convertê-lo
        // automaticamente para um objeto da nossa classe C# Produto.
        //Produto novoProduto: É o parâmetro do método. A API tentará preencher as propriedades
        //(Nome, Preco) com o que vier do [FromBody].
        public IActionResult PostProduto([FromBody] Produto novoProduto)
        {
            if (novoProduto == null)
            {
                // BadRequest(): É um método auxiliar que gera um erro HTTP 400 (Bad Request).
                // Se o cliente enviar um pacote quebrado ou nulo, a API recusa e avisa imediatamente,
                // protegendo o sistema.
                return BadRequest("O produto enviado está vazio ou inválido.");
            }
            //_produtos.Max(p => p.Id): Isso se chama expressão Lambda usando LINQ.
            //O código percorre toda a nossa lista _produtos, olha para a propriedade Id de
            //cada um (p => p.Id), acha o número máximo e soma 1 para criar o próximo.
            int novoId = _produtos.Count > 0 ? _produtos.Max(p => p.Id) + 1 : 1;
            novoProduto.Id = novoId;
            _produtos.Add(novoProduto);
            //StatusCode(201, novoProduto): Força a API a devolver exatamente o protocolo oficial
            //da internet para criação (201). Além do código de sucesso, ele devolve o novoProduto
            //para que quem chamou a API saiba qual Id foi gerado.
            return StatusCode(201, novoProduto);
        }
        //[HttpGet("{id}")]: A novidade aqui são as chavetas {id}. Elas definem um Parâmetro de Rota.
        // O .NET captura automaticamente o número digitado no final da URL e o prepara para ser
        // utilizado no código.
        [HttpGet("{id}")]
        
        public IActionResult GetProdutoPorId(int id)
        {
            //var: É uma palavra-chave de inferência de tipo. Em vez de escrevermos Produto produto = ...,
            //usamos var e o compilador do C# descobre automaticamente qual é o tipo da variável com base
            //no resultado que vem do lado direito do sinal de igual.
            //FirstOrDefault(p => p.Id == id): Este é o motor de busca do LINQ. O código percorre a
            //lista e tenta encontrar o "Primeiro" (First) elemento onde o Id do produto em memória
            //seja igual ao id passado na URL. Se não encontrar nenhuma correspondência, ele retorna o
            //valor "Padrão" (Default) aplicável a objetos no C#, que é null (nulo/ausente).
            var produto = _produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                //NotFound(): Um método fundamental para construir APIs corretas. Se a variável
                //estiver nula, disparamos a resposta HTTP 404 (Not Found). Uma API bem desenhada
                //nunca devolve sucesso com dados vazios quando algo não existe; ela deve devolver
                //explicitamente o erro 404.
                return NotFound("O produto solicitado não existe no catálogo.");
            }

            return Ok(produto);
        }

        [HttpPut("{id}")]
        public IActionResult PutProduto(int id, [FromBody] Produto produtoAtualizado)
        {
            var produtoExistente = _produtos.FirstOrDefault(p => p.Id == id);

            if (produtoExistente == null)
            {
                return NotFound("O produto informado não foi encontrado para atualização.");
            }
            // produtoExistente.Nome = produtoAtualizado.Nome;: É aqui que a mágica da Orientação a
            // Objetos acontece. O produtoExistente é a referência do item que está na nossa
            // lista em memória. Quando trocamos o valor da propriedade .Nome dele, a lista original é
            // atualizada imediatamente.
            produtoExistente.Nome = produtoAtualizado.Nome;
            produtoExistente.Preco = produtoAtualizado.Preco;
            // NoContent(): É o método que gera o código HTTP 204 (No Content - Sem Conteúdo).
            // Na teoria de APIs, quando você atualiza ou deleta algo com sucesso, você não precisa
            // devolver os dados de volta para o cliente, pois o cliente foi quem acabou de enviá-los.
            // O 204 é a forma da API dizer: "Recebi, atualizei com sucesso e não tenho mais nada a
            // acrescentar".
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound("O produto informado não foi encontrado para exclusão.");
            }

            _produtos.Remove(produto);

            return NoContent();
        }
    }
}
