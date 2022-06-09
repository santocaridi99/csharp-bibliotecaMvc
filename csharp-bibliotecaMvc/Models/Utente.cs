using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharp_bibliotecaMvc.Models
{
    public class Utente
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DataUltimoPrestito { get; set; }
        public ICollection<Prestito> Prestiti { get; set; }
    }
}
