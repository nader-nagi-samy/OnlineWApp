using ComicBookGalary.Data;
using ComicBookGalary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicBookGalary.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ComicBookRepository _comicBookRepository;

        public ComicBooksController()
        {
            _comicBookRepository = new ComicBookRepository();
        }

        public ActionResult Index()
        {
            var comicBooks = _comicBookRepository.GetComicBooks();
            return View(comicBooks);
        }

        public ActionResult Detail(int? id)
        {
            //if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            //{
            //    //return new RedirectResult("/");
            //    return Redirect("/");
            //}

            //return Content("Hello from ComicBooksController");

            //return new ContentResult()
            //{
            //    Content = "Hello from ComicBooksController"
            //};
            // ResponseContent `string`.
            //return "Hello from ComicBooksController";

            // ViewBag.
            //ViewBag.SeriesTitle = "The Amazing Spider-Man";
            //ViewBag.IssueNumber = 700;
            //ViewBag.Description = "<p>Final issue! Witness the final hours of Doctor Octopus' life and his one, last, great act of revenge! Even if Spider-Man survives...<strong>will Peter Parker?</strong></p>";
            //ViewBag.Artists = new string[] {
            //    "Script: Dan Slott" ,
            //    "Pencils: Humberto Ramos",
            //    "Inks: Victor Olazaba",
            //    "Colors: Edgar Delgado",
            //    "Letters: Chris Eliopoulos"
            //};

            //var comicBook = new ComicBook()
            //{
            //    SeriesTitle = "The Amazing Spider-Man",
            //    IssueNumber = 700,
            //    DescriptionHtml = "<p>Final issue! Witness the final hours of Doctor Octopus' life and his one, last, great act of revenge! Even if Spider-Man survives...<strong>will Peter Parker?</strong></p>",
            //    Artists = new Artist[] {
            //        new Artist { Role = "Script", Name="Dan Slott" },
            //        new Artist { Role = "Pencils", Name="Humberto Ramos" },
            //        new Artist { Role = "Inks", Name="Victor Olazaba" },
            //        new Artist { Role = "Colors", Name="Edgar Delgado" },
            //        new Artist { Role = "Letters", Name="Chris Eliopoulos" }
            //    }
            //};

            if (id == null)
            {
                return HttpNotFound();
            }
            var comicBook = _comicBookRepository.GetComicBook(id.Value);
            return View(comicBook);
        }

    }
}