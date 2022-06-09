using csharp_bibliotecaMvc.Models;
using System;
using System.Linq;

namespace csharp_bibliotecaMvc.Data
{
    public class Dbinizializer
    {
        public static void Initialize(BibliotecaContex context)
        {
            //E' il metodo di EF che crea il database SOLO se già non c'è
            context.Database.EnsureCreated();

            // Look for any Books.
            if (context.Libri.Any())
            {
                return;   // DB has been seeded
            }

            var utenti = new Utente[]
            {
             new Utente{Name="Mario", LastName="Rossi",DataUltimoPrestito=DateTime.Parse("2022-06-08")  },
             new Utente{Name="Luigi", LastName="Verdi",DataUltimoPrestito=DateTime.Parse("2022-05-08")  },
             new Utente{Name="Francesco", LastName="Bianchi",DataUltimoPrestito=DateTime.Parse("2022-04-08") },
            };
            foreach (Utente u in utenti)
            {
                context.Utenti.Add(u);
            }
            context.SaveChanges();


            var autori = new Autore[]
            {
              new Autore{AutoreName="Dante"},
              new Autore{AutoreName="jK Rowling"}
            };
            foreach (Autore a in autori)
            {
                context.Autori.Add(a);
            }
            context.SaveChanges();

            var Dante = context.Autori.Where(item => item.AutoreName == "Dante").First();
            var Rowling = context.Autori.Where(item => item.AutoreName == "jk Rowling").First();

            var libri = new Libro[]
            {
              new Libro{  Titolo="La divina Commedia" , Descrizione="grande classico" , Scaffale="SS2" ,Stato=Stato.Disponibile , Autori=new List<Autore>{Dante}},
               new Libro{  Titolo="Harry potter pietra filosofale" , Descrizione="Harry Potter" , Scaffale="SS3" ,  Stato=Stato.Disponibile ,  Autori=new List<Autore>{Rowling}},
                new Libro{ Titolo="Harry potter camera dei segreti" , Descrizione="Harry Potter" , Scaffale="SS3",  Stato=Stato.NonDisponibile ,  Autori=new List<Autore>{Rowling} },
                 new Libro{ Titolo="Harry potter principe mezzo sangue" , Descrizione="Harry Potter" , Scaffale="SS3", Stato=Stato.Prestito ,Autori=new List<Autore>{Rowling} },
            };
            foreach (Libro l in libri)
            {
                context.Libri.Add(l);
            }
            context.SaveChanges();


            var prestiti = new Prestito[]
            {
              new Prestito{  LibroID=4 , UtenteID=1  }
            };
            foreach (Prestito p in prestiti)
            {
                context.Prestiti.Add(p);
            }
            context.SaveChanges();






        }
    }
}
