using System;
using Newtonsoft.Json;

namespace CosmosDB.Models
{
    public class Pessoa
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public bool Ativo { get; set; }
        public DateTime DTCreated { get; set; } = DateTime.UtcNow;
        public DateTime DTUpdated { get; set; } = DateTime.UtcNow;
    }
}
