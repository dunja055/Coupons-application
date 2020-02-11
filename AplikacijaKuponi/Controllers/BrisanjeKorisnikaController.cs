using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class BrisanjeKorisnikaController : Controller
    {
        public ActionResult BrisanjeKorisnika(string id)
        {
            KuponiEntities baza = new KuponiEntities();
            List<Korisnik> korisnici = baza.Korisnik.ToList();
            Korisnik obrisanK = null;
            List<IstorijaKupovine> istorijaKupovine = baza.IstorijaKupovine.ToList();
            List<Obavestenje> obavestenje = baza.Obavestenje.ToList();
            List<Recenzija> recenzija = baza.Recenzija.ToList();
            List<Rezervacija> rezervacija = baza.Rezervacija.ToList();

            foreach (var istorijaK in istorijaKupovine)
            {
                if (istorijaK.Korisnik.IDKorisnik == Int32.Parse(id))
                {
                    baza.IstorijaKupovine.Remove(istorijaK);
                }
            }

            foreach (var ob in obavestenje)
            {
                if (ob.Korisnik.IDKorisnik == Int32.Parse(id))
                {
                    baza.Obavestenje.Remove(ob);
                }
            }
            foreach (var rec in recenzija)
            {
                if (rec.Korisnik.IDKorisnik == Int32.Parse(id))
                {
                    baza.Recenzija.Remove(rec);
                }
            }
            foreach (var rez in rezervacija)
            {
                if (rez.Korisnik.IDKorisnik == Int32.Parse(id))
                {
                    baza.Rezervacija.Remove(rez);
                }
            }
            foreach (var k in korisnici)
            {
                if (k.IDKorisnik == Int32.Parse(id))
                {
                    obrisanK = k;
                    break;

                }

            }

            baza.Korisnik.Remove(obrisanK);
            baza.SaveChanges();

            return RedirectToAction("Korisnici", "Korisnici");
        }
    }
}