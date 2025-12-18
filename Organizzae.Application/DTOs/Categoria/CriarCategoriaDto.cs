using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizzae.Application.DTOs.Categoria
{
    public class CriarCategoriaDto
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string? Icone { get; set; }
        public string CorHexadecimal { get; set; } = "#6c757d";
    }
}
