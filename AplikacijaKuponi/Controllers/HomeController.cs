using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class HomeController : Controller
    { 
        
        public ActionResult Index()
        {
            KuponiEntities baza = new KuponiEntities();
            List<Ponuda> ponude = baza.Ponuda.ToList();
            List<Kategorija> kategorija = baza.Kategorija.ToList();   
            Session["poslovniObjekti"] = baza.PoslovniObjekti.ToList();
            Session["ponude"] = ponude;
            Session["kategorija"] = kategorija;

            return View();
        }
        public ActionResult Odjava()
        {
            Session.Remove("ulogovani");
            return View("Index");
        }

        public ActionResult Pretraga(string naziv,string cena1,string cena2,string ocena)
        {
            KuponiEntities baza = new KuponiEntities();
            List<Ponuda> ponude = baza.Ponuda.ToList();
            List<Ponuda> filterPonude = new List<Ponuda>();

            foreach (Ponuda p in ponude)
            {
                double cena11 = double.Parse(cena1);
                double cena22 = double.Parse(cena2);
                int ocenaNova = Int32.Parse(ocena);
                if( p.NazivPonude.Contains(naziv) && p.Cena > cena11 && p.Cena < cena22 && ProsecnaOcenaPonude(p) > ocenaNova)
                {
                    filterPonude.Add(p);
                }
            }
            Session["ponude"] = filterPonude;
            return View("Index");
        }

        private double? ProsecnaOcenaPonude(Ponuda p)
        {
            double? prosek = 0;
            double? suma = 0;
            foreach (PonudaiPoslovniObjekti pip in p.PonudaiPoslovniObjekti)
            {
                foreach (Recenzija rec in pip.Recenzija)
                {
                    suma += rec.Ocena;
                }
                prosek = suma / pip.Recenzija.Count;
            }
            return prosek;
        }
        [HttpGet]
        public ActionResult IzborKategorije(string IDKategorija)
        {
             KuponiEntities baza = new KuponiEntities();
             List<Ponuda> ponude = baza.Ponuda.ToList();
             List<Ponuda> novePonude = new List<Ponuda>();
             foreach (Ponuda p in ponude)
             {
                 if(p.IDKategorija == Int32.Parse(IDKategorija))
                 {
                     novePonude.Add(p);
                 }
             }


            Session["ponude"] = novePonude;

            return View("Index");
        }
        public ActionResult DodajPonudu()
        {
            return View("Index");
        }
        [HttpPost]
        public ActionResult IzborGrada(string naziv)
        {
            KuponiEntities baza = new KuponiEntities();
            List<PoslovniObjekti> pObjekti = baza.PoslovniObjekti.ToList();
            List<PoslovniObjekti> noviPO = new List<PoslovniObjekti>();
            foreach (PoslovniObjekti po in pObjekti  )
            {
                if ( po.Grad== naziv)
                {
                    noviPO.Add(po);
                }
            }
            
            Session["poslovniObjekti"] = noviPO;

            return RedirectToAction("PoslovniObjekti", "PoslovniObjekti", new { naziv = naziv });
        }

    }
}