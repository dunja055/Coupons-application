using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class PoslovniObjektiController :Controller
    {
        public ActionResult PoslovniObjekti(string naziv = "")
        {
            KuponiEntities baza = new KuponiEntities();
            List<PoslovniObjekti> objekti = baza.PoslovniObjekti.ToList();

            if(naziv == "")
            {
                Session["poslovniObjekti"] = objekti;
            }
            return View();
        }
        public ActionResult DodajPoslovniObjekat()
        {
            return View("PoslovniObjekti");
        }

        public ActionResult PoObjekti()
        {
            return View();
        }
    }
}