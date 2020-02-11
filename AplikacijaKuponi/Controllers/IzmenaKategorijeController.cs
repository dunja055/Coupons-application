using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class IzmenaKategorijeController: Controller
    {
        public ActionResult IzmenaKategorije(string id)
        {
            KuponiEntities baza = new KuponiEntities();
            List<Kategorija> kategorije = baza.Kategorija.ToList();
            Kategorija nadjen = null;

            foreach (var k in kategorije)
            {
                if (k.IDKategorija == Int32.Parse(id))
                {
                    nadjen = k;
                    break;

                }

            }
            Session["izmenaKategorije"] = nadjen;

            return View();
        }

        public ActionResult IzmenaKategorijeBaza(Kategorija k)
        {

            Kategorija kIzmena = Session["izmenaKategorije"] as Kategorija;
            KuponiEntities baza = new KuponiEntities();
            List<Kategorija> kategorije = baza.Kategorija.ToList();

            Kategorija nadjen = kategorije.SingleOrDefault(a => a.IDKategorija == kIzmena.IDKategorija);
          
            nadjen.NazivKategorije = k.NazivKategorije;
            nadjen.Kolicina = k.Kolicina;

            baza.SaveChanges();

            return RedirectToAction("Kategorije", "Kategorije");
        }
    }
}