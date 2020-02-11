using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class KategorijeController : Controller
    {
        public ActionResult Kategorije()
        {
            KuponiEntities baza = new KuponiEntities();
            List<Kategorija> listaKategorija = baza.Kategorija.ToList();
            Session["kategorija"] = listaKategorija;
            return View();
        }
    }
}