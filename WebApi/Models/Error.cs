using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiddlewareNz.Evaluation.WebApi.Models
{
    public class Error
    {
        [JsonPropertyName("error")]
        [Required]
        public string Message { get; set; }
        [JsonPropertyName("error_description")]
        [Required]
        public string Description { get; set; }

        public Error(string error, string description)
        {
            Message = error;
            Description = description;
        }
    }
}
