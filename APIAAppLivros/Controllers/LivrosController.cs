using APIAAppLivros;
using APIAAppLivros.Models;
using LivrosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIAAppLivros.Controllers

{
    [Route("api/livros")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivrosRepository _livrosRepository;

        public LivrosController(LivrosRepository livrosRepository)
        {
            _livrosRepository = livrosRepository;
        }

        // GET: api/<LivrosController>
        [HttpGet]
        [Route("listar")]
        [SwaggerOperation(Summary = "Listar todos os Livros", Description = "Este endpoint retorna um listagem de livros cadastrados.")]
        public async Task<IEnumerable<Livros>> Listar([FromQuery] bool? ativo = null)
        {
            return await _livrosRepository.ListarTodosLivros(ativo);
        }

        // GET api/<LivrosController>/5
        [HttpGet("detalhes/{id}")]
        [SwaggerOperation(
            Summary = "Obtém dados de um Livro pelo ID",
            Description = "Este endpoint retorna todos os dados de um liro cadastrada filtrando pelo ID.")]

        public async Task<Livros> BuscarPorId(int id)
        {
            return await _livrosRepository.BuscarPorId(id);
        }

        // POST api/<PessoaController>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar um novo Livro",
            Description = "Este endpoint é responsavel por cadastrar um novo livro.")]
        public async void Post([FromBody] Livros dados)
        {
            await _livrosRepository.Salvar(dados);
        }

        // PUT api/<PessoaController>/5
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualizar os dados de uma Pessoa filtrado pelo ID",
            Description = "Este endpoint é resposavel por atualizar  os dados de uma pessoa no banco.")]
        public async Task<IActionResult> Put(int id, [FromBody] Livros dados)
        {
            dados.Id = id;
            await _livrosRepository.Atualizar(dados);
            return Ok();
        }

        // DELETE api/<PessoaController>/5
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover o Livro  filtrado pelo ID",
            Description = "Este endpoint é resposavel por remover  os dados de um livrp no banco.")]
        public async Task<IActionResult> Delete(int id)
        {
            await _livrosRepository.DeletarPorId(id);
            return Ok();
        }
    }

}



