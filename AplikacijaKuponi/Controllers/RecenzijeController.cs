using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class RecenzijeController : Controller
    {
        public ActionResult Recenzije(string naslov, string komentar, string rating)
        {
            Ponuda ponuda = Session["novaPonuda"] as Ponuda;
            Korisnik kor = Session["ulogovani"] as Korisnik;
            using (var baza = new KuponiEntities())
            {
                PonudaiPoslovniObjekti ponudaiPoslovniObjekti = baza.PonudaiPoslovniObjekti.FirstOrDefault(x => x.Ponuda.IDPonuda == ponuda.IDPonuda);
                Korisnik korFinal = baza.Korisnik.FirstOrDefault(x => x.IDKorisnik == kor.IDKorisnik);
                Recenzija recenzija = new Recenzija();
                recenzija.Ocena = Int32.Parse(rating);
                recenzija.Naslov = naslov;
                recenzija.Komentar = komentar;
                recenzija.PonudaiPoslovniObjekti = ponudaiPoslovniObjekti;
                recenzija.IDPonudaiPoslovniObjekti = ponudaiPoslovniObjekti.IDPonudaiPoslovniObjekti;
                recenzija.Datum = DateTime.Now;
                recenzija.Korisnik = korFinal;
                recenzija.IDKorisnik = korFinal.IDKorisnik;
                baza.Recenzija.Add(recenzija);
                baza.SaveChanges();
            }

            return RedirectToAction("Ponuda","Ponuda",new { IDPonuda = (Session["novaPonuda"] as Ponuda).IDPonuda });
        }
    }
}