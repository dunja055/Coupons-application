using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class ObjekatController : Controller
    {
        public ActionResult Objekat(string IDPoslovniObjekti)
        {
            KuponiEntities baza = new KuponiEntities();
            List<PoslovniObjekti> objekat = baza.PoslovniObjekti.ToList();
            PoslovniObjekti noviObjekti = null;
            foreach (PoslovniObjekti po in objekat)
            {
                if (po.IDPoslovniObjekti == int.Parse(IDPoslovniObjekti))
                {
                    noviObjekti = po;
                    break;
                }
            }
            Session["noviObjekti"] = noviObjekti;

            return View();
        }
    }
}