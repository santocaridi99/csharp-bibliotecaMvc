using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using csharp_bibliotecaMvc.Data;
using csharp_bibliotecaMvc.Models;

namespace csharp_bibliotecaMvc.Controllers
{
    public class LibroesController : Controller
    {
        private readonly BibliotecaContex _context;

        public LibroesController(BibliotecaContex context)
        {
            _context = context;
        }

        // GET: Libroes
        public async Task<IActionResult> Index()
        {
            return _context.Libri != null ?
                        View(await _context.Libri.ToListAsync()) :
                        Problem("Entity set 'BibliotecaContex.Libri'  is null.");
        }

        // GET: Libroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Libri == null)
            {
                return NotFound();
            }

            //var libro = await _context.Libri
            //    .FirstOrDefaultAsync(m => m.libroID == id);
            //if (libro == null)
            //{
            //    return NotFound();
            //}

            var libro = await _context.Libri
               .Include(a => a.Autori)
               .Include(p => p.Prestiti)
               .AsNoTracking()
               .FirstOrDefaultAsync(m => m.libroID == id);

            return View(libro);
        }

        // GET: Libroes/Create
        public IActionResult Create()
        {
            ViewData["listaAutoriAll"] = _context.Autori.ToList<Autore>();
            return View();
        }

        // POST: Libroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection form)
        {

            if (ModelState.IsValid)
            {
                Libro nuovoLibro = new Libro();
                //Autore nuovoAutore = new Autore();
                nuovoLibro.Titolo = form["Titolo"];
                nuovoLibro.Descrizione = form["Descrizione"];
                nuovoLibro.Scaffale = form["Scaffale"];
                nuovoLibro.Stato = (Stato)Enum.Parse(typeof(Stato), form["Stato"]);

                string str = form["AutoreName"];
                string[] words = str.Split(',');
                nuovoLibro.Autori = new List<Autore>();
                foreach (string word in words)
                {
                    Autore nuovoAutore = new Autore() { AutoreName = word };
                    nuovoLibro.Autori.Add(nuovoAutore);
                    _context.Autori.Add(nuovoAutore);
                }


                _context.Libri.Add(nuovoLibro);

                _context.SaveChanges();



                //nuovoAutore.AutoreName = form["AutoreName"];


                //Autori = new List<Autore> { Dante }





            }


            return RedirectToAction("Index");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Titolo,Descrizione,Scaffale,Stato")] Libro libro)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(libro);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(libro);
        //}

        // GET: Libroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Libri == null)
            {
                return NotFound();
            }

            var libro = await _context.Libri.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }


        // POST: Libroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Libro libro)
        {
            if (id != libro.libroID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("Edit", libro);


            }

            var aggiornaLibro = _context.Libri.Where(l => l.libroID == id).Include(a=>a.Autori).FirstOrDefault();
            aggiornaLibro.Titolo = libro.Titolo;
            aggiornaLibro.Scaffale = libro.Scaffale;
            aggiornaLibro.Stato = libro.Stato;
            aggiornaLibro.Descrizione = libro.Descrizione;
           
            if (libro.Autori.Count()==0)
            {
                aggiornaLibro.Autori = new List<Autore>();
                string str = Request.Form["AutoreName"];
                string[] words = str.Split(',');

                foreach (string word in words)
                {

                    Autore updateAutore = new Autore() { AutoreName = word };
                    aggiornaLibro.Autori.Add(updateAutore);

                    _context.Autori.Add(updateAutore);
                }
            }
            else
            {
                aggiornaLibro.Autori = libro.Autori;
            }


            //foreach(var item in libro.Autori)
            //{
            //    _context.Autori.RemoveRange(item);
            //}


           
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        //// POST: Libroes/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("libroID,Titolo,Descrizione,Scaffale,Stato,AutoreName")] Libro libro)
        //{
        //    if (id != libro.libroID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            string str = Request.Form["AutoreName"];
        //            string[] words = str.Split(',');

        //            //foreach(var item in libro.Autori)
        //            //{
        //            //    _context.Autori.RemoveRange(item);
        //            //}
        //            foreach (string word in words)
        //            {

        //                Autore updateAutore = new Autore() { AutoreName = word };
        //                libro.Autori.Add(updateAutore);

        //                _context.Autori.Update(updateAutore);
        //            }

        //            _context.Update(libro);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LibroExists(libro.libroID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(libro);
        //}

        // GET: Libroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Libri == null)
            {
                return NotFound();
            }

            var libro = await _context.Libri
                .FirstOrDefaultAsync(m => m.libroID == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Libri == null)
            {
                return Problem("Entity set 'BibliotecaContex.Libri'  is null.");
            }
            var libro = await _context.Libri.FindAsync(id);
            if (libro != null)
            {
                _context.Libri.Remove(libro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return (_context.Libri?.Any(e => e.libroID == id)).GetValueOrDefault();
        }
    }
}
