using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class DodavanjeKorisnikaController :Controller
    {
        public ActionResult DodavanjeKorisnika()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DodavanjeKorisnika(Korisnik k, string tip)
        {
            if(tip == "Administrator")
            {
                k.Uloga = true;
            }
            else
            {
                k.Uloga = false;
            }
            
            KuponiEntities baza = new KuponiEntities();
            baza.Korisnik.Add(k);
            baza.SaveChanges();



            return RedirectToAction("Korisnici", "Korisnici");

        }
    }
}