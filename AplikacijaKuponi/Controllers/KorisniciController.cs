using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class KorisniciController : Controller
    {
        public ActionResult Korisnici()
        {
            KuponiEntities baza = new KuponiEntities();
            List<Korisnik> listaKorisnici = baza.Korisnik.ToList();
            Session["korisnici"] = listaKorisnici;
            return View();
        }
    }
}