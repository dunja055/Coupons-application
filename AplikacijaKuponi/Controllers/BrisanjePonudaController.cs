using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class BrisanjePonudaController : Controller
    {
        public ActionResult BrisanjePonuda(string id)
        {
            KuponiEntities baza = new KuponiEntities();
            List<Ponuda> ponude = baza.Ponuda.ToList();
            Ponuda obrisanaP = null;
            List<PonudaiPoslovniObjekti> pip = baza.PonudaiPoslovniObjekti.ToList();
            List<SlikePonude> slikePonude = baza.SlikePonude.ToList();

            foreach (var slike in slikePonude)
            {
                if(slike.Ponuda.IDPonuda == Int32.Parse(id))
                {
                    baza.SlikePonude.Remove(slike);
                }
            }

            foreach (var p in ponude)
            {
                if (p.IDPonuda == Int32.Parse(id))
                {
                    obrisanaP = p;
                    break;
                }
            }

            List<PonudaiPoslovniObjekti> ponudePoslObj = obrisanaP.PonudaiPoslovniObjekti.ToList();
            foreach (var po in ponudePoslObj)
            {
                foreach(SlikePoslovnihObjekata sp in baza.SlikePoslovnihObjekata)
                {
                    if(sp.IDPoslovniObjekti == po.IDPoslovniObjekti)
                    {
                        baza.SlikePoslovnihObjekata.Remove(sp);
                    }
                }

                foreach(IstorijaKupovine ik in baza.IstorijaKupovine)
                {
                    if(ik.IDPonudaiPoslovniObjekti == po.IDPonudaiPoslovniObjekti)
                    {
                        baza.IstorijaKupovine.Remove(ik);
                    }
                }
                foreach(Recenzija rec in baza.Recenzija)
                {
                    if(rec.IDPonudaiPoslovniObjekti == po.IDPonudaiPoslovniObjekti)
                    {
                        baza.Recenzija.Remove(rec);
                    }
                }
                foreach(Rezervacija rez in baza.Rezervacija)
                {
                    if(rez.IDPonudaiPoslovniObjekti == po.IDPonudaiPoslovniObjekti)
                    {
                        baza.Rezervacija.Remove(rez);
                    }
                }
                baza.PonudaiPoslovniObjekti.Remove(po);
            }
           
         
            List<Kategorija> kategorije = baza.Kategorija.ToList();
            Kategorija nadjen = null;
            foreach (var k in kategorije)
            {
                if (k.IDKategorija == obrisanaP.IDKategorija)
                {
                    nadjen = k;
                    break;

                }
            }
            nadjen.Kolicina = nadjen.Kolicina - 1;
            baza.Ponuda.Remove(obrisanaP);
            baza.SaveChanges();
            
 

            return RedirectToAction("Index", "Home");
        }
    }
}