using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace api.VOs.User
{
    public class UserVOEnter
    {
        [JsonPropertyName("name")]
        [BindProperty(Name = "name")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Name { get; set; }


        [JsonPropertyName("email")]
        [BindProperty(Name = "email")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [BindProperty(Name = "password")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MinLength(8, ErrorMessage = "{0} precisa ser maior que {1} caracter")]
        public string Password { get; set; }
    }
}
