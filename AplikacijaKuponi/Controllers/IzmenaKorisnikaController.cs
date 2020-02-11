using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class IzmenaKorisnikaController : Controller
    {
        public ActionResult IzmenaKorisnika(string id)
        {
            KuponiEntities baza = new KuponiEntities();
            List<Korisnik> korisnici = baza.Korisnik.ToList();
            Korisnik nadjen = null; 

            foreach (var k in korisnici)
            {
                if (k.IDKorisnik == Int32.Parse(id))
                {
                    nadjen = k;
                    break;

                }

            }
            Session["izmenaKorisnik"] = nadjen;

            return View();
        }

        public ActionResult IzmenaKorisnikaBaza(Korisnik k, string tip)
        {

            Korisnik kIzmena = Session["izmenaKorisnik"] as Korisnik;
            KuponiEntities baza = new KuponiEntities();
            List<Korisnik> korisnici = baza.Korisnik.ToList();

            Korisnik nadjen = korisnici.SingleOrDefault(a => a.IDKorisnik == kIzmena.IDKorisnik);
            if (tip == "Administrator")
            {
                nadjen.Uloga = true;
            }
            else
            {
                nadjen.Uloga = false;
            }
            nadjen.Ime = k.Ime;
            nadjen.Prezime = k.Prezime;
            nadjen.Email = k.Email;
            nadjen.Username = k.Username;
            nadjen.Password = k.Password;

            baza.SaveChanges();

            return RedirectToAction("Korisnici", "Korisnici");
        }
    }
}