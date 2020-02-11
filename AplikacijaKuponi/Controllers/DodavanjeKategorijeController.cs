using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class DodavanjeKategorijeController: Controller
    {
        public ActionResult DodavanjeKategorije()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DodavanjeKategorije(Kategorija k)
        {
            KuponiEntities baza = new KuponiEntities();
            baza.Kategorija.Add(k);
            baza.SaveChanges();

            return RedirectToAction("Kategorije", "Kategorije");

        }
    }
}