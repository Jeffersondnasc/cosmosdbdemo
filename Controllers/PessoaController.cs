using CosmosDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly Container _container;

        public PessoaController(CosmosClient cosmosClient)
        {
            _container = cosmosClient.GetContainer("cosmodbjefferson", "Pessoa");
        }

        [HttpGet]
        public async Task<IEnumerable<Pessoa>> Get()
        {
            var query = _container.GetItemQueryIterator<Pessoa>("SELECT * FROM c");
            var results = new List<Pessoa>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }
            return results;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetById(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Pessoa>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> Create(Pessoa pessoa)
        {
            pessoa.Id = Guid.NewGuid().ToString();
            pessoa.DTCreated = DateTime.UtcNow;
            pessoa.DTUpdated = DateTime.UtcNow;
            await _container.CreateItemAsync(pessoa, new PartitionKey(pessoa.Id));
            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Pessoa pessoa)
        {
            if (id != pessoa.Id) return BadRequest();
            pessoa.DTUpdated = DateTime.UtcNow;
            await _container.UpsertItemAsync(pessoa, new PartitionKey(id));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _container.DeleteItemAsync<Pessoa>(id, new PartitionKey(id));
            return NoContent();
        }
    }
}
