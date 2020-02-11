using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class PonudaController : Controller
    {
        public ActionResult Ponuda(string IDPonuda)
        {
            KuponiEntities baza = new KuponiEntities();
            List<Ponuda> ponude = baza.Ponuda.ToList();
            Ponuda novaPonuda = null;
            foreach(Ponuda p in ponude)
            {
                if(p.IDPonuda== int.Parse(IDPonuda))
                {
                    novaPonuda = p;
                    break;
                }
            }
            PonudaiPoslovniObjekti ponudaiPoslovniObjekti = baza.PonudaiPoslovniObjekti.FirstOrDefault(x => x.Ponuda.IDPonuda == novaPonuda.IDPonuda);
            List<Recenzija> recenzijePonude = new List<Recenzija>();

            foreach(Recenzija rec in baza.Recenzija)
            {
                if(rec.IDPonudaiPoslovniObjekti == ponudaiPoslovniObjekti.IDPonudaiPoslovniObjekti)
                {
                    recenzijePonude.Add(rec);
                }
            }

            Session["novaPonuda"] = novaPonuda;
            Session["recenzije"] = recenzijePonude;

            return View();
        }
      
    }
}