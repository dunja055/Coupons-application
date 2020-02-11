using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class BrisanjePoslovnihObjekataController : Controller
    {
        public ActionResult BrisanjePoslovnihObjekata(string id)
        {
            KuponiEntities baza = new KuponiEntities();
            List<PoslovniObjekti> poslovniObjekti = baza.PoslovniObjekti.ToList();
            PoslovniObjekti obrisaniPO = null;
            List<PonudaiPoslovniObjekti> pip = baza.PonudaiPoslovniObjekti.ToList();
            List<SlikePoslovnihObjekata> slikePO = baza.SlikePoslovnihObjekata.ToList();

            foreach (var slike in slikePO)
            {
                if (slike.PoslovniObjekti.IDPoslovniObjekti == Int32.Parse(id))
                {
                    baza.SlikePoslovnihObjekata.Remove(slike);
                }
            }

            foreach (var po in poslovniObjekti)
            {
                if (po.IDPoslovniObjekti == Int32.Parse(id))
                {
                    obrisaniPO = po;
                    break;
                }
            }

            List<PonudaiPoslovniObjekti> ponudePoslObj = obrisaniPO.PonudaiPoslovniObjekti.ToList();
            foreach (var pObj in ponudePoslObj)
            {
                foreach (SlikePonude slike in baza.SlikePonude)
                {
                    if (slike.IDPonuda == pObj.IDPonuda)
                    {
                        baza.SlikePonude.Remove(slike);
                    }
                }
                
                foreach (IstorijaKupovine ik in baza.IstorijaKupovine)
                {
                    if (ik.IDPonudaiPoslovniObjekti == pObj.IDPonudaiPoslovniObjekti)
                    {
                        baza.IstorijaKupovine.Remove(ik);
                    }
                }
                foreach (Recenzija rec in baza.Recenzija)
                {
                    if (rec.IDPonudaiPoslovniObjekti == pObj.IDPonudaiPoslovniObjekti)
                    {
                        baza.Recenzija.Remove(rec);
                    }
                }
                foreach (Rezervacija rez in baza.Rezervacija)
                {
                    if (rez.IDPonudaiPoslovniObjekti == pObj.IDPonudaiPoslovniObjekti)
                    {
                        baza.Rezervacija.Remove(rez);
                    }
                }
                baza.PonudaiPoslovniObjekti.Remove(pObj);
            }

            baza.PoslovniObjekti.Remove(obrisaniPO);
            baza.SaveChanges();

            return RedirectToAction("PoslovniObjekti", "PoslovniObjekti");
        }
    }
}