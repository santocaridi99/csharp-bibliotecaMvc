using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace csharp_bibliotecaMvc.Models
{
    public enum Stato
    {
        Disponibile , Prestito , NonDisponibile
    }
    public class Libro
    {
        public int libroID { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public string Scaffale { get; set; }
        
        public Stato Stato { get; set; }    

        public ICollection<Prestito>? Prestiti { get; set; }
        public ICollection<Autore>? Autori { get; set; }

    }
}
