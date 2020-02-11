using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class IzmenaPOController : Controller
    {
        public ActionResult IzmenaPO(string id)
        {
            KuponiEntities baza = new KuponiEntities();
            List<PoslovniObjekti> poObjekti = baza.PoslovniObjekti.ToList();
            PoslovniObjekti nadjen = null;

            foreach (var po in poObjekti)
            {
                if (po.IDPoslovniObjekti == Int32.Parse(id))
                {
                    nadjen = po;
                    break;

                }

            }
            Session["izmenaPO"] = nadjen;

            return View();
        }
        [HttpPost]
        public ActionResult IzmenaPOBaza(PoslovniObjekti po, HttpPostedFileBase file)
        {
            PoslovniObjekti poIzmena = Session["izmenaPO"] as PoslovniObjekti;
            KuponiEntities baza = new KuponiEntities();
            List<PoslovniObjekti> poObjekti = baza.PoslovniObjekti.ToList();

            PoslovniObjekti nadjen = poObjekti.SingleOrDefault(a => a.IDPoslovniObjekti == poIzmena.IDPoslovniObjekti);
            if (file != null)
            {

                string slika = System.IO.Path.GetFileName(file.FileName);
                string putanja = System.IO.Path.Combine(Server.MapPath("~/Content/assets/Slike poslovni objekti/"), slika);
                file.SaveAs(putanja);
                SlikePoslovnihObjekata spo = baza.SlikePoslovnihObjekata.FirstOrDefault(x => x.IDPoslovniObjekti == nadjen.IDPoslovniObjekti);
                baza.SlikePoslovnihObjekata.Remove(spo);
                baza.SaveChanges();

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] niz = ms.GetBuffer();
                    SlikePoslovnihObjekata slikePO = new SlikePoslovnihObjekata();
                    var novaPutanja = MakeRelative(putanja, @"C:\Users\Dunja\Desktop\Kuponi\AplikacijaKuponi\AplikacijaKuponi\");
                    novaPutanja = "/" + novaPutanja;
                    slikePO.Putanja = novaPutanja;
                    slikePO.PoslovniObjekti = nadjen;
                    slikePO.IDPoslovniObjekti = nadjen.IDPoslovniObjekti;
                    baza.SlikePoslovnihObjekata.Add(slikePO);
                    baza.SaveChanges();
                 
                }
            }
            nadjen.NazivObjekta = po.NazivObjekta;
            nadjen.Grad = po.Grad;
            nadjen.Adresa = po.Adresa;
            nadjen.Telefon = po.Telefon;
            nadjen.Email = po.Email;
            baza.SaveChanges();

            return RedirectToAction("PoslovniObjekti", "PoslovniObjekti");
        }

        public string MakeRelative(string filePath, string referencePath)
        {
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(referencePath);
            return referenceUri.MakeRelativeUri(fileUri).ToString();
        }
    }
}