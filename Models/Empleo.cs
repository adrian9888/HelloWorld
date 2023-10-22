using System;
using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Models
{
    public class Empleo
    {
        public int Id { get; set; }

        public string? Logo { get; set; } // Esto será una URL a la imagen del logo.

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        public decimal Salario { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string Enlace { get; set; } // URL para aplicar al empleo.
    }
}