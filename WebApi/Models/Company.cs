using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiddlewareNz.Evaluation.WebApi.Models
{
    public class Company
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; }
    }
}