using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class BrisanjeKategorijeController : Controller
    {
        public ActionResult BrisanjeKategorije(string id)
        {
            KuponiEntities baza = new KuponiEntities();
            List<Kategorija> kategorije = baza.Kategorija.ToList();
            Kategorija obrisanaK = null;
            List<Ponuda> ponude = baza.Ponuda.ToList();


            foreach (var p in ponude)
            {
                if (p.Kategorija.IDKategorija == Int32.Parse(id))
                {
                    baza.Ponuda.Remove(p);
                }
            }


            foreach (var k in kategorije)
            {
                if (k.IDKategorija == Int32.Parse(id))
                {
                    obrisanaK = k;
                    break;

                }

            }

            baza.Kategorija.Remove(obrisanaK);
            baza.SaveChanges();

            return RedirectToAction("Kategorije", "Kategorije");
        }
    }
}