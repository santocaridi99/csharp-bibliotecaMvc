using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharp_bibliotecaMvc.Models
{
    public class Autore
    {
        public int AutoreID { get; set; }
        public string AutoreName { get; set; }
        public ICollection<Libro> Libri { get; set; }

    }
}
